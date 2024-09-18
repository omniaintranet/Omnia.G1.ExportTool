using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Omnia.MigrationG2.Foundation.Core.SharePoint
{
    /// <summary>
    /// AppClientContext
    /// </summary>
    public class AppClientContext : ExtendedClientContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppClientContext" /> class.
        /// </summary>
        /// <param name="webFullUrl">The web full URL.</param>
        public AppClientContext(string webFullUrl) : base(webFullUrl) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AppClientContext" /> class.
        /// </summary>
        /// <param name="webFullUrl">The web full URL.</param>
        public AppClientContext(Uri webFullUrl) : base(webFullUrl) { }

        /// <summary>
        /// Executes the query.
        /// </summary>
        public override void ExecuteQuery()
        {
            try
            {
                base.ExecuteQuery();
            }
            catch (System.Net.WebException wex)
            {
                var response = wex.Response as HttpWebResponse;
                if (response != null && (response.StatusCode == HttpStatusCode.Unauthorized))
                {
                    string accessToken = TokenHelper.GetAppOnlyAccessToken(this.Url);

                    this.ExecutingWebRequest +=
                        delegate (object oSender, WebRequestEventArgs webRequestEventArgs)
                        {
                            //webRequestEventArgs.WebRequestExecutor.RequestHeaders["Authorization"] =
                            //    "Bearer " + accessToken;
                            //webRequestEventArgs.WebRequestExecutor.WebRequest.UserAgent = Constants.App.UserAgent;
                            webRequestEventArgs.WebRequest.Headers.Add("Authorization", "Bearer " + accessToken);
                        };

                    base.ExecuteQuery();
                }
                else
                {
                    throw;
                }
            }
        }
    }
}
