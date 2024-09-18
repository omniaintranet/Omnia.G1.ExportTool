using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Foundation.Core.SharePoint
{
    public class UserClientContext : ExtendedClientContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppClientContext" /> class.
        /// </summary>
        /// <param name="webFullUrl">The web full URL.</param>
        public UserClientContext(string webFullUrl) : base(webFullUrl) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AppClientContext" /> class.
        /// </summary>
        /// <param name="webFullUrl">The web full URL.</param>
        public UserClientContext(Uri webFullUrl) : base(webFullUrl) { }

        /// <summary>
        /// Executes the query.
        /// </summary>
        public override void ExecuteQuery()
        {
            if(base.Credentials == null)
            {
                base.Credentials = TokenHelper.UserCredential;
            }
            try
            {
                base.ExecuteQuery();
            }catch(System.Net.WebException wex)
            {
                throw;
            }
        }
    }
}
