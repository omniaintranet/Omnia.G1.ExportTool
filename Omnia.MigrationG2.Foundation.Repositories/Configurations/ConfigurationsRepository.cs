using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.Extensions.Options;
using Omnia.MigrationG2.Foundation.Models.Configurations;
using Omnia.MigrationG2.Models.AppSettings;

namespace Omnia.MigrationG2.Foundation.Repositories.Configurations
{
    public class ConfigurationsRepository : DatabaseRepositoryBase, IConfigurationsRepository
    {
        public Configuration GetConfiguration(string name, string region, Guid? extensionPackageId = null)
        {
            try
            {
                if (region == null)
                {
                    region = string.Empty;
                }

                return OMFContext
                         .Configurations
                         .Where
                         (
                             q => q.Name == name
                             && q.Region == region
                             && q.TenantId == this.TenantId
                             && (extensionPackageId == null || q.ExtensionPackageId == extensionPackageId.Value)
                             && q.DeletedAt == null
                         )
                         .Select(Mapper.Map<Configuration>)
                         .FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets the configurations in region.
        /// </summary>
        /// <param name="region">The region.</param>
        /// <returns></returns>
        public virtual IEnumerable<Models.Configurations.Configuration> GetConfigurationsInRegion(string region, Guid? extensionPackageId = null)
        {
            try
            {
                if (region == null)
                {
                    region = string.Empty;
                }

                var result = OMFContext
                    .Configurations
                    .Where
                    (
                        q => q.Region == region
                        && (extensionPackageId == null || q.ExtensionPackageId == extensionPackageId.Value)
                        && q.TenantId == this.TenantId
                        && q.DeletedAt == null
                    )
                    .Select(Mapper.Map<Configuration>);

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fromSiteUrl"></param>
        /// <param name="region"></param>
        /// <param name="extensionPackageId"></param>
        /// <returns></returns>
        public IEnumerable<Models.Configurations.Configuration> GetParentSiteConfigurations(string fromSiteUrl, string region, Guid? extensionPackageId = null)
        {
            try
            {
                IEnumerable<Models.Configurations.Configuration> configurations = OMFContext
                    .Configurations
                    .Where
                    (
                        q => q.TenantId == this.TenantId
                        && q.Region == region
                        && (extensionPackageId == null || q.ExtensionPackageId == extensionPackageId.Value)
                        && fromSiteUrl.StartsWith(q.Name)
                        && q.DeletedAt == null
                    )
                    .OrderByDescending(q => q.Name)
                    .Select(Mapper.Map<Configuration>);

                int urlPartsCount = fromSiteUrl.Split('/').Count();
                return configurations.Where(item => item.Name.Split('/').Count() < urlPartsCount);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
