using Omnia.MigrationG2.Foundation.Models.Tenants;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Foundation.Core.SharePoint
{
    /// <summary>
    /// Extends the SharePoint Context with extra information
    /// </summary>
    public class SharePointContextInfo
    {
        /// <summary>
        /// The _relative URL
        /// </summary>
        string _relativeUrl = string.Empty;

        /// <summary>
        /// Gets the tenant for the request
        /// </summary>
        /// <value>
        /// The tenant.
        /// </value>
        public Tenant Tenant { get; set; }

        /// <summary>
        /// Gets the loginname of the user making the request
        /// </summary>
        /// <value>
        /// The name of the login.
        /// </value>
        public string LoginName { get; set; }

        /// <summary>
        /// Gets the Url of the Site targeted by the ClientContext.
        /// </summary>
        /// <value>
        /// The sp URL.
        /// </value>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the token cache key.
        /// </summary>
        /// <value>
        /// The token cache key.
        /// </value>
        public string TokenCacheKey { get; set; }

        /// <summary>
        /// Gets or sets the Azure AD token.
        /// </summary>
        public string AADToken { get; set; }

        /// <summary>
        /// Gets or sets the lcid.
        /// </summary>
        /// <value>
        /// The lcid.
        /// </value>
        public int LCID { get; set; }

        /// <summary>
        /// Gets the relative Url of the ClientContext.
        /// </summary>
        /// <value>
        /// The relative URL.
        /// </value>
        public string RelativeUrl
        {
            get
            {
                _relativeUrl.IsNull(() =>
                {
                    _relativeUrl = new Uri(this.Url).PathAndQuery;
                });

                return _relativeUrl;
            }
        }

        /// <summary>
        /// Gets the relative Url of the ClientContext and returns string.Empty if the url only contains /
        /// </summary>
        /// <value>
        /// The relative URL empty if root.
        /// </value>
        public string RelativeUrlEmptyIfRoot
        {
            get
            {
                return RelativeUrl.Is("/") ? string.Empty : RelativeUrl;
            }
        }

        /// <summary>
        /// Get whether the client context is using Azure AD Token
        /// </summary>
        public bool UseAADToken
        {
            get
            {
                return !string.IsNullOrWhiteSpace(AADToken);
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is application only context.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is application only context; otherwise, <c>false</c>.
        /// </value>
        public bool IsAppOnlyContext
        {
            get
            {
                return LoginName == Constants.App.LoginName;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SharePointContextInfo"/> class.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="loginName">Name of the login.</param>
        /// <param name="tenant">The tenant.</param>
        public SharePointContextInfo(string url, string loginName, Tenant tenant, string tokenCacheKey, int lcid)
        {
            this.Url = url;
            this.LoginName = loginName;
            this.Tenant = tenant;
            this.TokenCacheKey = tokenCacheKey;
            this.LCID = lcid;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SharePointContextInfo"/> class.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="loginName"></param>
        /// <param name="aadToken"></param>
        /// <param name="tenant"></param>
        /// <param name="lcid"></param>
        public SharePointContextInfo(string url, string loginName, string aadToken, Tenant tenant, int lcid)
        {
            this.Url = url;
            this.LoginName = loginName;
            this.Tenant = tenant;
            this.AADToken = aadToken;
            this.LCID = lcid;
        }

        protected SharePointContextInfo(SharePointContextInfo spContext)
        {
            this.Url = spContext.Url;
            this.LoginName = spContext.LoginName;
            this.Tenant = spContext.Tenant;
            this.TokenCacheKey = spContext.TokenCacheKey;
            this.AADToken = spContext.AADToken;
            this.LCID = spContext.LCID;
        }

        /// <summary>
        /// Combines the string with the ClientContext relative url.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="emptyIfRoot">if set to <c>true</c> [empty if root].</param>
        /// <returns></returns>
        public string CombineWithRelativeUrl(string url, bool emptyIfRoot = true)
        {
            return this.RelativeUrlEmptyIfRoot.Is(true)
                ? RelativeUrlEmptyIfRoot + url
                : RelativeUrl + url;
        }
    }
}
