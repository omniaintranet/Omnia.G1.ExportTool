using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Omnia.MigrationG2.Foundation.Core.Utilities
{
    /// <summary>
    /// ApiUtils
    /// </summary>
    public class ApiUtils
    {
        /// <summary>
        /// Determines whether [is web not found exception] [the specified ex].
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <returns></returns>
        public static bool IsWebNotFoundException(Exception ex)
        {
            WebException webException = null;
            if (ex.GetType().IsAssignableFrom(typeof(WebException)))
            {
                webException = ex as WebException;
            }

            if (webException != null && webException.Status == WebExceptionStatus.ProtocolError && ((HttpWebResponse)webException.Response).StatusCode == HttpStatusCode.NotFound)
            {
                return true;
            }

            return false;
        }
    }
}
