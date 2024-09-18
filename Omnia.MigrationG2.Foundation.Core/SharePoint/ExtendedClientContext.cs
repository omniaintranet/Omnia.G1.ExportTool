using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Threading;

namespace Omnia.MigrationG2.Foundation.Core.SharePoint
{
    /// <summary>
    /// Extends the ClientContext
    /// </summary>
    public class ExtendedClientContext : ClientContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExtendedClientContext" /> class.
        /// </summary>
        /// <param name="webFullUrl">The web full URL.</param>
        public ExtendedClientContext(string webFullUrl) : base(webFullUrl) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExtendedClientContext" /> class.
        /// </summary>
        /// <param name="webFullUrl">The web full URL.</param>
        public ExtendedClientContext(Uri webFullUrl) : base(webFullUrl) { }

        /// <summary>
        /// Executes the query.
        /// </summary>
        public virtual void ExecuteQuery()
        {
            ExecuteQueryRetry();
        }

        /// <summary>
        /// Executes the query and performs retry if throttled.
        /// </summary>
        /// <param name="retryCount">The retry count.</param>
        /// <param name="delay">The delay.</param>
        /// <exception cref="System.ArgumentException">Provide a retry count greater than zero.
        /// or
        /// Provide a delay greater than zero.</exception>
        /// <exception cref="Omnia.Foundation.Core.SharePoint.MaximumRetryAttemptedException"></exception>
        private void ExecuteQueryRetry(int retryCount = 10, int delay = 500)
        {
            int retryAttempts = 0;
            int backoffInterval = delay;
            if (retryCount <= 0)
                throw new ArgumentException("Provide a retry count greater than zero.");

            if (delay <= 0)
                throw new ArgumentException("Provide a delay greater than zero.");

            // Do while retry attempt is less than retry count
            while (retryAttempts < retryCount)
            {
                try
                {
                    base.ExecuteQueryAsync().Wait();
                    return;
                }
                catch (WebException wex)
                {
                    var response = wex.Response as HttpWebResponse;
                    // Check if request was throttled - http status code 429
                    // Check is request failed due to server unavailable - http status code 503
                    if (response != null && (response.StatusCode == (HttpStatusCode)429 || response.StatusCode == (HttpStatusCode)503))
                    {
                        Debug.WriteLine("CSOM request frequency exceeded usage limits. Sleeping for {0} seconds before retrying.", backoffInterval);

                        //Add delay for retry
                        Thread.Sleep(backoffInterval);

                        //Add to retry count and increase delay.
                        retryAttempts++;
                        backoffInterval = backoffInterval * 2;
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            throw new MaximumRetryAttemptedException(string.Format("Maximum retry attempts {0}, has be attempted.", retryCount));
        }

    }

    /// <summary>
    /// Defines a Maximum Retry Attemped Exception
    /// </summary>
    [Serializable]
    public class MaximumRetryAttemptedException : Exception
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message"></param>
        public MaximumRetryAttemptedException(string message)
            : base(message)
        {

        }
    }
}
