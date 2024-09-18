using Microsoft.Extensions.Options;
using Omnia.MigrationG2.Models.AppSettings;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Omnia.MigrationG2.Core.Http
{
    public abstract class G1HttpClientService : BaseHttpClientService
    {
        public G1HttpClientService(
          IHttpClientFactory httpClientFactory)
          : base(httpClientFactory)
        {
        }

        protected override void EnsureDefaultHeaders(HttpRequestHeaders headers)
        {
            headers.Accept.Clear();
            headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (!string.IsNullOrEmpty(_httpClientSettings.TokenKey))
            {
                headers.Add("TokenKey", _httpClientSettings.TokenKey);
            }
            else
            {
                headers.Add("ExtensionId", _httpClientSettings.ToString());
                headers.Add("ApiSecret", _httpClientSettings.ApiSecret);
            }

        }
    }
}
