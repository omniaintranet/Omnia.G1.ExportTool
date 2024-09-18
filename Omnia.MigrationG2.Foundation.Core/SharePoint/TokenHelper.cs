using Microsoft.Extensions.Options;
using Microsoft.SharePoint.Client;
using Newtonsoft.Json;
using Omnia.MigrationG2.Core;
using Omnia.MigrationG2.Foundation.Core.Caching;
using Omnia.MigrationG2.Foundation.Core.Utilities;
using Omnia.MigrationG2.Foundation.Models.Shared;
using Omnia.MigrationG2.Models.AppSettings;
using SharePointPnP.IdentityModel.Extensions.S2S.Protocols.OAuth2;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Omnia.MigrationG2.Foundation.Core.SharePoint
{
    public static class TokenHelper
    {
        private static ICacheService _cacheService;

        private static ICacheService CacheService
        {
            get
            {
                if (_cacheService == null)
                {
                    _cacheService = AppUtils.GetService<ICacheService>();
                }

                return _cacheService;
            }
        }

        /// <summary>
        /// SharePoint principal.
        /// </summary>
        public const string SharePointPrincipal = "00000003-0000-0ff1-ce00-000000000000";

        public static string ClientId = AppUtils.OmniaSettings.ClientId;
        public static string ClientSecret = AppUtils.OmniaSettings.ClientSecret;
        public static string AppTokenCacheKey = $"appToken_{ClientId}";
        public static readonly TimeSpan AccessTokenLifetimeTolerance = TimeSpan.FromMinutes(5.0);


        private const string S2SProtocol = "OAuth2";
        private const string DelegationIssuance = "DelegationIssuance1.0";
        private const string AcsMetadataEndPointRelativeUrl = "metadata/json/1";
        private static string GlobalEndPointPrefix = "accounts";
        private static string AcsHostUrl = "accesscontrol.windows.net";

        /// <summary>
        /// Gets the application only access token.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        public static string GetAppOnlyAccessToken(string url)
        {
            var webToken = string.Empty;
            Uri webUri = new Uri(url);

            // Verify web uri
            if (webUri == null)
            {
                throw new ArgumentNullException("webUri");
            }

            // Get realm and token
            string webRealm = TokenHelper.GetRealmFromTargetUrl(webUri);
            OAuth2AccessTokenResponse token = GetAppOnlyAccessToken(SharePointPrincipal, webUri.Authority, webRealm);

            webToken = token.AccessToken;
            CacheService.AddOrUpdateMemoryCache(
                $"{AppTokenCacheKey}-{url}", 
                webToken,
                DateTimeOffset.Now.AddSeconds(Convert.ToInt32(token.ExpiresIn)).Subtract(AccessTokenLifetimeTolerance));

            return webToken;
        }

        /// This logic need to be changed in order to migrate from On-Prem

        /// <summary>
        /// Retrieves an app-only access token from ACS to call the specified principal 
        /// at the specified targetHost. The targetHost must be registered for target principal.  If specified realm is 
        /// null, the "Realm" setting in web.config will be used instead.
        /// </summary>
        /// <param name="targetPrincipalName">Name of the target principal to retrieve an access token for</param>
        /// <param name="targetHost">Url authority of the target principal</param>
        /// <param name="targetRealm">Realm to use for the access token's nameid and audience</param>
        /// <returns>An access token with an audience of the target principal</returns>
        public static OAuth2AccessTokenResponse GetAppOnlyAccessToken(
            string targetPrincipalName,
            string targetHost,
            string targetRealm)
        {
            string resource = GetFormattedPrincipal(targetPrincipalName, targetHost, targetRealm);
            string clientId = GetFormattedPrincipal(ClientId, null, targetRealm);

            OAuth2AccessTokenRequest oauth2Request = OAuth2MessageFactory.CreateAccessTokenRequestWithClientCredentials(clientId, ClientSecret, resource);
            oauth2Request.Resource = resource;

            // Get token
            OAuth2S2SClient client = new OAuth2S2SClient();
            OAuth2AccessTokenResponse oauth2Response;
            try
            {
                oauth2Response =
                    client.Issue(AcsMetadataParser.GetStsUrl(targetRealm), oauth2Request) as OAuth2AccessTokenResponse;
            }
            catch (WebException wex)
            {
                using (StreamReader sr = new StreamReader(wex.Response.GetResponseStream()))
                {
                    string responseText = sr.ReadToEnd();
                    throw new WebException(wex.Message + " - " + responseText, wex);
                }
            }

            return oauth2Response;
        }

        /// <summary>
        /// Get authentication realm from SharePoint
        /// </summary>
        /// <param name="targetApplicationUri">Url of the target SharePoint site</param>
        /// <returns>String representation of the realm GUID</returns>
        public static string GetRealmFromTargetUrl(Uri targetApplicationUri)
        {
            WebRequest request = WebRequest.Create(targetApplicationUri + "/_vti_bin/client.svc");
            request.Headers.Add("Authorization: Bearer ");

            try
            {
                using (request.GetResponse())
                {
                }
            }
            catch (WebException e)
            {
                if (e.Response == null)
                {
                    return null;
                }

                var response = (HttpWebResponse)e.Response;
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    throw e;
                }

                string bearerResponseHeader = e.Response.Headers["WWW-Authenticate"];
                if (string.IsNullOrEmpty(bearerResponseHeader))
                {
                    return null;
                }

                const string bearer = "Bearer realm=\"";
                int bearerIndex = bearerResponseHeader.IndexOf(bearer, StringComparison.Ordinal);
                if (bearerIndex < 0)
                {
                    return null;
                }

                int realmIndex = bearerIndex + bearer.Length;

                if (bearerResponseHeader.Length >= realmIndex + 36)
                {
                    string targetRealm = bearerResponseHeader.Substring(realmIndex, 36);

                    Guid realmGuid;

                    if (Guid.TryParse(targetRealm, out realmGuid))
                    {
                        return targetRealm;
                    }
                }
            }
            return null;
        }


        private static string GetFormattedPrincipal(string principalName, string hostName, string realm)
        {
            if (!String.IsNullOrEmpty(hostName))
            {
                return String.Format(CultureInfo.InvariantCulture, "{0}/{1}@{2}", principalName, hostName, realm);
            }

            return String.Format(CultureInfo.InvariantCulture, "{0}@{1}", principalName, realm);
        }

        #region AcsMetadataParser

        // This class is used to get MetaData document from the global STS endpoint. It contains
        // methods to parse the MetaData document and get endpoints and STS certificate.
        public static class AcsMetadataParser
        {
            public static X509Certificate2 GetAcsSigningCert(string realm)
            {
                JsonMetadataDocument document = GetMetadataDocument(realm);

                if (null != document.keys && document.keys.Count > 0)
                {
                    JsonKey signingKey = document.keys[0];

                    if (null != signingKey && null != signingKey.keyValue)
                    {
                        return new X509Certificate2(Encoding.UTF8.GetBytes(signingKey.keyValue.value));
                    }
                }

                throw new Exception("Metadata document does not contain ACS signing certificate.");
            }

            public static string GetDelegationServiceUrl(string realm)
            {
                JsonMetadataDocument document = GetMetadataDocument(realm);

                JsonEndpoint delegationEndpoint = document.endpoints.SingleOrDefault(e => e.protocol == DelegationIssuance);

                if (null != delegationEndpoint)
                {
                    return delegationEndpoint.location;
                }
                throw new Exception("Metadata document does not contain Delegation Service endpoint Url");
            }

            private static JsonMetadataDocument GetMetadataDocument(string realm)
            {
                string acsMetadataEndpointUrlWithRealm = String.Format(CultureInfo.InvariantCulture, "{0}?realm={1}",
                                                                       GetAcsMetadataEndpointUrl(),
                                                                       realm);
                byte[] acsMetadata = null;
                using (WebClient webClient = new WebClient())
                {
                    CommonUtils.TransientExceptionRetry(() =>
                    {
                        acsMetadata = webClient.DownloadData(acsMetadataEndpointUrlWithRealm);
                    },
                    new TransientExceptionRetryStrategy()
                    {
                        RetryCount = 3,
                        ExponentialDelayMilliseconds = 3000,
                        IncludeInnerExceptions = true,
                        TransientExceptionTypes = new List<Type>
                        {
                            typeof(WebException)
                        }
                    });
                }
                string jsonResponseString = Encoding.UTF8.GetString(acsMetadata);

                //JavaScriptSerializer serializer = new JavaScriptSerializer();
                //JsonMetadataDocument document = serializer.Deserialize<JsonMetadataDocument>(jsonResponseString);
                JsonMetadataDocument document = JsonConvert.DeserializeObject<JsonMetadataDocument>(jsonResponseString);

                if (null == document)
                {
                    throw new Exception("No metadata document found at the global endpoint " + acsMetadataEndpointUrlWithRealm);
                }

                return document;
            }

            private static string GetAcsMetadataEndpointUrl()
            {
                return Path.Combine(GetAcsGlobalEndpointUrl(), AcsMetadataEndPointRelativeUrl);
            }

            private static string GetAcsGlobalEndpointUrl()
            {
                return String.Format(CultureInfo.InvariantCulture, "https://{0}.{1}/", GlobalEndPointPrefix, AcsHostUrl);
            }

            public static string GetStsUrl(string realm)
            {
                JsonMetadataDocument document = GetMetadataDocument(realm);

                JsonEndpoint s2sEndpoint = document.endpoints.SingleOrDefault(e => e.protocol == S2SProtocol);

                if (null != s2sEndpoint)
                {
                    return s2sEndpoint.location;
                }

                throw new Exception("Metadata document does not contain STS endpoint url");
            }

            private class JsonMetadataDocument
            {
                public string serviceName { get; set; }
                public List<JsonEndpoint> endpoints { get; set; }
                public List<JsonKey> keys { get; set; }
            }

            private class JsonEndpoint
            {
                public string location { get; set; }
                public string protocol { get; set; }
                public string usage { get; set; }
            }

            private class JsonKeyValue
            {
                public string type { get; set; }
                public string value { get; set; }
            }

            private class JsonKey
            {
                public string usage { get; set; }
                public JsonKeyValue keyValue { get; set; }
            }
        }

        #endregion

        #region UserCredential
        public static NetworkCredential UserCredential
        {
            get
            {
                var settings = AppUtils.UserSettings;
                SecureString securestring = new SecureString();
                settings.Password.ToCharArray().ToList().ForEach(s => securestring.AppendChar(s));
                //Add domain name here for G1 onprem
                return new NetworkCredential(settings.Username, securestring,"swedishclub");
            }
        }
        #endregion
    }
}
