using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omnia.MigrationG2.Intranet.Services.Navigations
{
    public class NavigationMapper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hostUrl"></param>
        /// <param name="targetUrl"></param>
        /// <returns></returns>
        public static string GetNavigationNodeSiteUrl(string hostUrl, string targetUrl)
        {
            var urlParts = targetUrl.Split('/');
            if (urlParts.Length > 2)
            {
                string siteRelativeUrl = string.Join("/", urlParts.Take(urlParts.Length - 2));
                return hostUrl + siteRelativeUrl;
            }
            return hostUrl;
        }
    }
}
