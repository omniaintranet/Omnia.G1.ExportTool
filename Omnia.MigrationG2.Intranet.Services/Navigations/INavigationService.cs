using Omnia.MigrationG2.Intranet.Models.Navigations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Intranet.Services.Navigations
{
    public interface INavigationService
    {
        /// <summary>
        /// Gets the navigation node by identifier.
        /// </summary>
        /// <param name="navigationSourceUrl">The navigation source URL.</param>
        /// <param name="nodeId">The node identifier.</param>
        /// <param name="excludeParentAndChildren">if set to <c>true</c> [exclude parent and children].</param>
        /// <returns></returns>
        Models.Navigations.NavigationNode GetNavigationNodeById(string navigationSourceUrl, Guid nodeId, bool excludeParentAndChildren = false);

        /// <summary>
        /// Gets the navigation languages.
        /// </summary>
        /// <param name="navigationSourceUrl">The navigation source URL.</param>
        /// <returns></returns>
        IEnumerable<NavigationLanguage> GetNavigationLanguages(string navigationSourceUrl);
    }
}
