using Omnia.MigrationG2.Intranet.Models.Navigations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omnia.MigrationG2.Intranet.Services.Navigations
{
    public class NavigationService: INavigationService
    {
        private INavigationTermService _navigationTermService;
        private readonly INavigationCacheService _navigationCacheService;
        private readonly INavigationDBService _navigationDBService;

        public NavigationService(
            INavigationTermService navigationTermService,
            INavigationCacheService navigationCacheService,
            INavigationDBService navigationDBService)
        {
            _navigationTermService = navigationTermService;
            _navigationCacheService = navigationCacheService;
            _navigationDBService = navigationDBService;
        }

        /// <summary>
        /// Gets the navigation node by identifier.
        /// </summary>
        /// <param name="navigationSourceUrl">The navigation source URL.</param>
        /// <param name="nodeId">The node identifier.</param>
        /// <param name="excludeParentAndChildren">if set to <c>true</c> [exclude parent and children].</param>
        /// <returns></returns>
        public Models.Navigations.NavigationNode GetNavigationNodeById(string navigationSourceUrl, Guid nodeId, bool excludeParentAndChildren = false)
        {
            try
            {
                if (!_navigationCacheService.IsAllNavigationNodesCached(navigationSourceUrl))
                {
                    //Ensure navigation caching
                    GetAndEnsureAllNavigationNodesInCache(navigationSourceUrl);
                }
                return _navigationCacheService.GetNavigationNodeById(navigationSourceUrl, nodeId, excludeParentAndChildren);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Gets the term languages.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<NavigationLanguage> GetNavigationLanguages(string navigationSourceUrl)
        {
            try
            {
                var languages = GetAndEnsureNavigationLanguages(navigationSourceUrl);
                return languages;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Gets the and ensure all navigation nodes in cache.
        /// </summary>
        /// <param name="navigationSourceUrl">The navigation source URL.</param>
        /// <returns></returns>
        public IEnumerable<Models.Navigations.NavigationNode> GetAndEnsureAllNavigationNodesInCache(string navigationSourceUrl, bool shouldCloneNodes = true)
        {
            var navigationNodes = _navigationCacheService.GetAllNavigationNodes(navigationSourceUrl, shouldCloneNodes);
            if (navigationNodes == null)
            {
                AddOrUpdateNavigationNodesCache(navigationSourceUrl);
                navigationNodes = _navigationCacheService.GetAllNavigationNodes(navigationSourceUrl, shouldCloneNodes);
            }
            return navigationNodes;
        }

        /// <summary>
        /// Adds the or update navigation nodes cache.
        /// </summary>
        /// <param name="navigationSourceUrl">The navigation source URL.</param>
        /// <returns></returns>
        public IEnumerable<NavigationTopNode> AddOrUpdateNavigationNodesCache(string navigationSourceUrl)
        {
            try
            {
                var temp = 0;
                var navigationNodes = _navigationDBService.GetAllNavigationNodes(navigationSourceUrl, out temp,null);
                return _navigationCacheService.AddOrUpdateNavigationNodes(navigationSourceUrl, navigationNodes);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private IEnumerable<NavigationLanguage> GetAndEnsureNavigationLanguages(string navigationSourceUrl)
        {
            var languages = _navigationCacheService.GetNavigationLanguages(navigationSourceUrl);
            if (languages == null || languages.Count() == 0)
            {
                languages = _navigationTermService.GetNavigationLanguages();
                _navigationCacheService.AddOrUpdateNavigationLanguages(navigationSourceUrl, languages);
            }
            return languages.OrderBy(l => l.DisplayName);
        }
    }
}
