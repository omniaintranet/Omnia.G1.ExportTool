using Omnia.MigrationG2.Intranet.Models.Navigations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Intranet.Services.Navigations
{
    public interface INavigationCacheService
    {
        bool IsAllNavigationNodesCached(string navigationSourceUrl);
        IEnumerable<NavigationNode> GetAllNavigationNodes(string navigationSourceUrl, bool cloneNodes);
        Models.Navigations.NavigationNode GetNavigationNodeById(string navigationSourceUrl, Guid nodeId, bool excludeParentAndChildren);
        IEnumerable<NavigationTopNode> AddOrUpdateNavigationNodes(string navigationSourceUrl, IEnumerable<Models.Navigations.NavigationNode> navigationNodes);
        IEnumerable<NavigationLanguage> GetNavigationLanguages(string navigationSourceUrl);
        void AddOrUpdateNavigationLanguages(string navigationSourceUrl, IEnumerable<NavigationLanguage> languages);
    }
}
