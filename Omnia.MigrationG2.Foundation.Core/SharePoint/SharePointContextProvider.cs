using Microsoft.SharePoint.Client;
using Omnia.MigrationG2.Core;
using Omnia.MigrationG2.Foundation.Core.Caching;
using Omnia.MigrationG2.Foundation.Models.Extensibility;
using Omnia.MigrationG2.Foundation.Models.Tenants;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Text;

namespace Omnia.MigrationG2.Foundation.Core.SharePoint
{
    public class SharePointContextProvider
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
        /// Creates the application only client context for the provider spUrl.
        /// </summary>
        /// <param name="spUrl">The sp URL.</param>
        /// <param name="tenant">The tenant.</param>
        /// <returns></returns>
        public static AppClientContext CreateAppOnlyClientContext(string spUrl, Tenant tenant, string language = null)
        {
            try
            {
                var appAccessToken = CacheService.GetFromMemoryCache<string>($"{TokenHelper.AppTokenCacheKey}-{spUrl}");
                if (string.IsNullOrEmpty(appAccessToken))
                {
                    appAccessToken = TokenHelper.GetAppOnlyAccessToken(spUrl);

                    if (string.IsNullOrEmpty(appAccessToken))
                    {
                        throw new Exception("The App Access Token is Null. SPUrl:" + spUrl);
                    }
                }

                AppClientContext clientContext = new AppClientContext(spUrl);
                RegisterClientContextToken(appAccessToken, clientContext);

                clientContext.Tag = new SharePointContextInfo
                      (
                          url: spUrl,
                          loginName: Constants.App.LoginName,
                          tenant: tenant,
                          tokenCacheKey: string.Empty,
                          lcid: string.IsNullOrEmpty(language) ? Constants.Common.DefaultLanguageLCID : CultureInfo.GetCultureInfo(language).LCID
                      );

                return clientContext;
            }
            catch (Exception ex)
            {
                if (ex != null && ex.GetType() == typeof(WebException))
                {
                    var webEx = (WebException)ex;
                    if (webEx.Response != null && webEx.Response.GetType() == typeof(HttpWebResponse))
                    {
                        var response = (HttpWebResponse)webEx.Response;
                        if (response.StatusCode == HttpStatusCode.NotFound)
                        {
                            throw ex;
                        }
                    }
                }

                throw new SPTokenExpiredException(Constants.Api.errorCreatingContext);
            }
        }

        /// <summary>
        /// Creates the user client context for the provider spUrl.
        /// </summary>
        /// <param name="spUrl">The sp URL.</param>
        /// <param name="tenant">The tenant.</param>
        /// <returns></returns>
        public static UserClientContext CreateUserClientContext(string spUrl, Tenant tenant, string language = null)
        {
            try
            {

                UserClientContext clientContext = new UserClientContext(spUrl);
                clientContext.Tag = new SharePointContextInfo
                      (
                          url: spUrl,
                          loginName: Constants.App.LoginName,
                          tenant: tenant,
                          tokenCacheKey: string.Empty,
                          lcid: string.IsNullOrEmpty(language) ? Constants.Common.DefaultLanguageLCID : CultureInfo.GetCultureInfo(language).LCID
                      );

                return clientContext;
            }
            catch (Exception ex)
            {
                if (ex != null && ex.GetType() == typeof(WebException))
                {
                    var webEx = (WebException)ex;
                    if (webEx.Response != null && webEx.Response.GetType() == typeof(HttpWebResponse))
                    {
                        var response = (HttpWebResponse)webEx.Response;
                        if (response.StatusCode == HttpStatusCode.NotFound)
                        {
                            throw ex;
                        }
                    }
                }

                throw new SPTokenExpiredException(Constants.Api.errorCreatingContext);
            }
        }

        public static ExtendedClientContext CreateClientContext(string spUrl, Tenant tenant, string language = null)
        {
            if (AppUtils.MigrationSettings.UseUserClientContext)
            {
                return CreateUserClientContext(spUrl,tenant, language);
            }
            return CreateAppOnlyClientContext(spUrl, tenant, language);
        }


        /// <summary>
        /// Elevate permission to app context.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="spUrl">The sp URL.</param>
        /// <param name="tenant">The tenant.</param>
        /// <param name="language">The language.</param>
        /// <param name="func">The function.</param>
        /// <returns></returns>
        public static T Elevate<T>(string spUrl, Tenant tenant, string language, Func<ExtendedClientContext, T> func)
        {
            try
            {
                using (var appContext = CreateClientContext(spUrl, tenant, language))
                {
                    return func(appContext);
                }
            }
            catch (System.Net.WebException wex)
            {
                var response = wex.Response as HttpWebResponse;
                if (response != null && (response.StatusCode == HttpStatusCode.Unauthorized))
                {
                    using (var appContext = CreateClientContext(spUrl, tenant, language))
                    {
                        return func(appContext);
                    }
                }
                else
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Elevate permission to app context.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="spUrl">The sp URL.</param>
        /// <param name="tenant">The tenant.</param>
        /// <param name="func">The function.</param>
        /// <returns></returns>
        public static T Elevate<T>(string spUrl, Tenant tenant, Func<ExtendedClientContext, T> func)
        {
            return Elevate<T>(spUrl, tenant, null, func);
        }

        /// <summary>
        /// Elevate permission to app context.
        /// </summary>
        /// <param name="spUrl">The sp URL.</param>
        /// <param name="tenant">The tenant.</param>
        /// <param name="language">The language.</param>
        /// <param name="action">The action.</param>
        public static void Elevate(string spUrl, Tenant tenant, string language, Action<ExtendedClientContext> action)
        {
            Elevate<bool>(spUrl, tenant, language, appContext =>
            {
                action(appContext);
                return true;
            });
        }

        /// <summary>
        /// Elevate permission to app context.
        /// </summary>
        /// <param name="spUrl">The sp URL.</param>
        /// <param name="tenant">The tenant.</param>
        /// <param name="action">The action.</param>
        public static void Elevate(string spUrl, Tenant tenant, Action<ExtendedClientContext> action)
        {
            Elevate<bool>(spUrl, tenant, appContext =>
            {
                action(appContext);
                return true;
            });
        }

        private static void RegisterClientContextToken(string accessToken, AppClientContext clientContext)
        {
            clientContext.AuthenticationMode = ClientAuthenticationMode.Anonymous;
            clientContext.FormDigestHandlingEnabled = false;
            clientContext.ExecutingWebRequest +=
                delegate (object oSender, WebRequestEventArgs webRequestEventArgs)
                {
                    //webRequestEventArgs.WebRequestExecutor.RequestHeaders["Authorization"] =
                    //    "Bearer " + accessToken;
                    //webRequestEventArgs.WebRequestExecutor.WebRequest.UserAgent = Core.Constants.App.UserAgent;
                    webRequestEventArgs.WebRequest.Headers.Add("Authorization", "Bearer " + accessToken);
                    //webRequestEventArgs.WebRequest.Headers.Add("UserAgent", Core.Constants.App.UserAgent);
                };
        }
    }
}
