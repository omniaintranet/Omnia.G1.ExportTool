using Omnia.MigrationG2.Intranet.Models.Navigations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Intranet.Services.Navigations
{
    public interface INavigationDBService
    {
        /// <summary>
        /// Gets the navigation tree from database.
        /// </summary>
        /// <param name="navigationSourceUrl">The navigation source URL.</param>
        /// <returns></returns>
        IList<NavigationNode> GetAllNavigationNodes(string navigationSourceUrl, out int numberOfNavigationNodes, DateTime? minDate,string navigationSiteUrl =null, string parentId = null);
    }
}
