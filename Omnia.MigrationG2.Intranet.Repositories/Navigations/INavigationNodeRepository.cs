using Omnia.MigrationG2.Intranet.Models.Navigations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Intranet.Repositories.Navigations
{
    public interface INavigationNodeRepository
    {
        IList<NavigationNode> GetByNavigationSourceUrl(string navigationSourceUrl);
        IList<NavigationNode> GetByNavigationSiteUrl(string navigationSourceUrl, string siteURL, string parentId);
    }
}
