using AutoMapper;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Omnia.MigrationG2.Core;
using Omnia.MigrationG2.Foundation.Core.Caching;
using Omnia.MigrationG2.Intranet.Models.Navigations;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Omnia.MigrationG2.Intranet.Services.Navigations
{
    public class NavigationCacheService: INavigationCacheService
    {
        private readonly ICacheService _cacheService;

        public NavigationCacheService()
        {
            _cacheService = AppUtils.GetService<ICacheService>();
        }

        public bool IsAllNavigationNodesCached(string navigationSourceUrl)
        {
            var navigationNodes = GetAllNavigations(navigationSourceUrl);
            return navigationNodes != null;
        }

        public IEnumerable<Models.Navigations.NavigationNode> GetAllNavigationNodes(string navigationSourceUrl, bool shouldCloneNodes)
        {
            var navigationNodes = GetAllNavigations(navigationSourceUrl);
            if (navigationNodes != null && shouldCloneNodes)
            {
                return CloneNodes(navigationNodes);
            }
            return navigationNodes;
        }

        public Models.Navigations.NavigationNode GetNavigationNodeById(string navigationSourceUrl, Guid nodeId, bool excludeParentAndChildren)
        {
            //If we excludeParentAndChildren we will get not cloneNodes so we work with memorycache and then we only clone the requested node
            var navigationNodes = GetAllNavigationNodes(navigationSourceUrl, shouldCloneNodes: excludeParentAndChildren.Is(false));

            //2017-05-19 Andii removed this and is using a Publish/Subscribe mechanism to update in memory instead
            //we need to refactor this for performance.
            //if (excludeParentAndChildren.Is(false))
            //{
            //    NavigationHelper.UpdateNavigationUrl(navigationSourceUrl, ctx, configurationsClient);
            //}
            if (navigationNodes != null && navigationNodes.Count() > 0)
            {
                var node = FindNavigationNode(navigationNodes, nodeId);
                if (node != null && excludeParentAndChildren)
                {
                    return Mapper.Map<Models.Navigations.NavigationNode, Models.Navigations.NavigationNode>(node);
                }
                return node;
            }
            return null;
        }

        public void AddOrUpdateNavigationLanguages(string navigationSourceUrl, IEnumerable<NavigationLanguage> languages)
        {
            var cacheKey = NavigationHelper.GetNavigationLanguageKey(navigationSourceUrl);
            _cacheService.AddOrUpdateMemoryCache(cacheKey, new ConcurrentBag<NavigationLanguage>(languages),
                region: Core.Constants.Configurations.Regions.NavigationLanguageCache);
        }

        public IEnumerable<NavigationLanguage> GetNavigationLanguages(string navigationSourceUrl)
        {
            var cacheKey = NavigationHelper.GetNavigationLanguageKey(navigationSourceUrl);
            var cacheResult = _cacheService.GetFromMemoryCache<ConcurrentBag<NavigationLanguage>>(
                cacheKey, region: Core.Constants.Configurations.Regions.NavigationLanguageCache);
            if (cacheResult != null)
                return cacheResult.ToList();

            return null;
        }

        private Models.Navigations.NavigationNode FindNavigationNode(IEnumerable<Models.Navigations.NavigationNode> nodes, Guid nodeId)
        {
            Models.Navigations.NavigationNode foundNode = null;
            foreach (Models.Navigations.NavigationNode node in nodes)
            {
                if (node.Id == nodeId)
                    return node;
                else
                    foundNode = FindNavigationNode(node.Children, nodeId);
                if (foundNode != null)
                    break;
            }
            return foundNode;
        }

        private List<Models.Navigations.NavigationNode> CloneNodes(IEnumerable<Models.Navigations.NavigationNode> navigationNodes)
        {
            // We need to have retry mechanism when cloning navigation cache to avoid 
            // concurrent access and update to the cache at the same time.

            var newClonedNodes = navigationNodes.Select(
                    node => Mapper.Map<Models.Navigations.NavigationNode, Models.Navigations.NavigationNode>(node));

            var retryCount = 0;
            var maxRetry = 5;

            while (retryCount < maxRetry)
            {
                retryCount++;
                try
                {
                    return newClonedNodes.ToList();
                }
                catch (AutoMapperMappingException ex)
                {
                    //OmniaApi.WorkWith(Ctx.Omnia()).Logging().AddLog(
                    //    "Error when cloning navigation node",
                    //    $"Retry {retryCount} times. Error: {ex.Message}",
                    //    Foundation.Models.Logging.DefaultLogTypes.Warning);

                    Thread.Sleep(100);
                }
            }

            throw new Exception("Cannot get navigation nodes from cache");
        }

        public IEnumerable<NavigationTopNode> AddOrUpdateNavigationNodes(string navigationSourceUrl, IEnumerable<Models.Navigations.NavigationNode> navigationNodes)
        {
            var rootNode = navigationNodes.FirstOrDefault();
            var topNodes = GetTopNodeCaches(navigationSourceUrl, rootNode);
            if (rootNode != null)
                topNodes.Add(GetRootNodeCache(navigationSourceUrl, rootNode));

            AddOrUpdateNavigationHashCache(navigationSourceUrl, new ConcurrentBag<NavigationTopNode>(topNodes));
            _cacheService.AddOrUpdateMemoryCache(NavigationHelper.GetNavigationTreeCacheKey(navigationSourceUrl), navigationNodes,
                region: Core.Constants.Configurations.Regions.NavigationCache);
            return topNodes;
        }

        private void AddOrUpdateNavigationHashCache(string navigationSourceUrl, ConcurrentBag<NavigationTopNode> topNodes)
        {
            var hashNodes = new List<HashNavigationNode>();
            foreach (var item in topNodes)
            {
                hashNodes.Add(item.HashNode);
                _cacheService.AddOrUpdateMemoryCache(item.HashNode.Key, item.Node,
                    region: Core.Constants.Configurations.Regions.HashNavigationCache);
            }
            _cacheService.AddOrUpdateMemoryCache(NavigationHelper.GetAllNodeHashesCacheKey(navigationSourceUrl), hashNodes,
                region: Core.Constants.Configurations.Regions.HashNavigationCache);
        }

        private NavigationTopNode GetRootNodeCache(string navigationSourceUrl, Models.Navigations.NavigationNode rootNode)
        {
            if (rootNode == null)
                return null;

            var clientCachedRootNode = Mapper.Map<ClientCachedNavigationNode>(rootNode);
            // only set the term set info for the root node cache
            clientCachedRootNode.TermStoreId = rootNode.TermStoreId;
            clientCachedRootNode.TermSetId = rootNode.TermSetId;
            if (clientCachedRootNode.Children != null)
            {
                foreach (var topNode in clientCachedRootNode.Children)
                {
                    topNode.Children = null;
                }
            }

            var hashNode = new HashNavigationNode
            {
                Key = NavigationHelper.GetRootNavigationNodeCacheKey(navigationSourceUrl),
                Hash = Foundation.Core.Utilities.CommonUtils.CreateMd5Hash(JsonConvert.SerializeObject(clientCachedRootNode))
            };
            var retVal = new NavigationTopNode
            {
                HashNode = hashNode,
                Node = clientCachedRootNode
            };
            return retVal;
        }

        private IList<NavigationTopNode> GetTopNodeCaches(string navigationSourceUrl, Models.Navigations.NavigationNode rootNode)
        {
            var topNodeCaches = new List<NavigationTopNode>();
            if (rootNode != null && rootNode.Children != null)
            {
                foreach (var topNode in rootNode.Children)
                {
                    var clientCachedTopNode = Mapper.Map<ClientCachedNavigationNode>(topNode);
                    var hashNode = new HashNavigationNode
                    {
                        Key = NavigationHelper.GetTopNodeCacheKey(navigationSourceUrl, topNode),
                        Hash = Foundation.Core.Utilities.CommonUtils.CreateMd5Hash(JsonConvert.SerializeObject(clientCachedTopNode))
                    };
                    var navTopNode = new NavigationTopNode
                    {
                        HashNode = hashNode,
                        Node = clientCachedTopNode
                    };
                    topNodeCaches.Add(navTopNode);
                }
            }
            return topNodeCaches;
        }

        private IEnumerable<Models.Navigations.NavigationNode> GetAllNavigations(string navigationSourceUrl)
        {
            var navigationCacheKey = NavigationHelper.GetNavigationTreeCacheKey(navigationSourceUrl);
            return _cacheService.GetFromMemoryCache<IEnumerable<Models.Navigations.NavigationNode>>(
                navigationCacheKey, Core.Constants.Configurations.Regions.NavigationCache);
        }
    }
}
