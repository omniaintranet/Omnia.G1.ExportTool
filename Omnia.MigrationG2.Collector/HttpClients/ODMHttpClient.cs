using Microsoft.Extensions.Options;
using Omnia.MigrationG2.Collector.Models.G1;
using Omnia.MigrationG2.Core.Http;
using Omnia.MigrationG2.Core.Utils;
using Omnia.MigrationG2.Intranet.Models.SearchProperties;
using Omnia.MigrationG2.Models.AppSettings;
using Omnia.MigrationG2.Models.HttpClient;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Omnia.MigrationG2.Collector.Http
{
    public class ODMHttpClient : G1HttpClientService
    {
        protected override string BaseUrl
        {
            get
            {
                return $"{_httpClientSettings.ODMUrl}";
            }
        }

        public ODMHttpClient(
            IHttpClientFactory httpClientFactory)
            : base(httpClientFactory)
        {
        }

        public bool isODMSettingsEnsured
        {
            get
            {
                try
                {
                    if(_httpClientSettings == null)
                    {
                        return false;
                    }
                    else
                    {
                        return !String.IsNullOrEmpty(_httpClientSettings.ODMUrl);
                    }
                }catch(Exception ex)
                {
                    return false;
                }
            }
        }

        public async ValueTask<ApiOperationResult<List<G1SearchProperty>>> GetSearchPropertiesAsync()
        {
            var parameters = new NameValueCollection()
                {
                    { "SPUrl", _httpClientSettings.SharePointUrl },
                    { "Lang", "sv-se" },
                    { "excludeSystemColumns", "false" },
                    { "requireRelatedDocumentPropety", "false" },
                };

            var httpResponse = await GetAsync("/SearchProperty/GetSearchProperties", parameters: parameters);
            var apiResponse = httpResponse.Content.ReadAsJsonAsync<ApiOperationResult<List<G1SearchProperty>>>();

            return await apiResponse;
        }
    }
}
