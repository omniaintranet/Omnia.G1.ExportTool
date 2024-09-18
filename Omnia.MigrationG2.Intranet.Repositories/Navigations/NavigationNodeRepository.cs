using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Microsoft.Extensions.Options;
using Omnia.MigrationG2.Core;
using Omnia.MigrationG2.Intranet.Models.Navigations;

namespace Omnia.MigrationG2.Intranet.Repositories.Navigations
{
    public class NavigationNodeRepository : DatabaseRepositoryBase, INavigationNodeRepository
    {
        public IList<NavigationNode> GetByNavigationSourceUrl(string navigationSourceUrl)
        {
            try
            {
                var items = OMIContext
                    .NavigationNodes
                    .Where(item => item.TenantId == TenantId && !item.Deleted && item.NavigationSourceUrl == navigationSourceUrl)
                    .Select(Mapper.Map<NavigationNode>)
                    .ToList();

                return items;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public IList<NavigationNode> GetByNavigationSiteUrl(string navigationSourceUrl, string navigationSiteUrl, string parentId)
        {
            var tempid = new Guid(parentId);
            try
            {
                var items = OMIContext
                    .NavigationNodes
                    .Where(item => item.TenantId == TenantId && !item.Deleted && item.NavigationSourceUrl == navigationSourceUrl && (item.SiteUrl == navigationSiteUrl || item.ParentId == tempid))
                    .Select(Mapper.Map<NavigationNode>)
                    .ToList();

                return items;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
