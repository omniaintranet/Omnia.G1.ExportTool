using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.Http;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Omnia.MigrationG2.Core.Utils
{
    public static class HttpClientExtensions
    {
        public static async Task<T> ReadAsJsonAsync<T>(this HttpContent content)
        {
            var dataAsString = await content.ReadAsStringAsync();
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(dataAsString);
        }

        public static string toQueryStringParameters(this NameValueCollection parameters)
        {
            var array = (
                from key in parameters.AllKeys
                from value in parameters.GetValues(key)
                select string.Format(
            "{0}={1}",
            HttpUtility.UrlEncode(key),
            HttpUtility.UrlEncode(value))
                ).ToArray();
            return "?" + string.Join("&", array);
        }
    }
}
