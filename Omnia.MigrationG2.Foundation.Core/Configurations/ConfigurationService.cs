using Omnia.MigrationG2.Foundation.Models.Configurations;
using Omnia.MigrationG2.Foundation.Repositories.Configurations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Foundation.Core.Configurations
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly IConfigurationsRepository _configurationsRepository;

        public ConfigurationService(IConfigurationsRepository configurationsRepository)
        {
            _configurationsRepository = configurationsRepository;
        }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="region">The region.</param>
        /// <returns></returns>
        public Configuration GetConfiguration(string name, string region, Guid? extensionPackageId = null)
        {
            //if (extensionPackageId == null || extensionPackageId == Guid.Empty)
            //    extensionPackageId = this.ExtensionPackageId;

            var data = _configurationsRepository.GetConfiguration(name, region, extensionPackageId);

            return data;
        }

        public IEnumerable<Configuration> GetConfigurationsInRegion(string region, Guid? extensionPackageId = null)
        {
            //if (extensionPackageId == null || extensionPackageId == Guid.Empty)
            //    extensionPackageId = this.ExtensionPackageId;

            var data = _configurationsRepository.GetConfigurationsInRegion(region, extensionPackageId);

            return data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fromSiteUrl"></param>
        /// <param name="region"></param>
        /// <returns></returns>
        public IEnumerable<Configuration> GetParentSiteConfigurations(string fromSiteUrl, string region)
        {
            var data = _configurationsRepository.GetParentSiteConfigurations(fromSiteUrl, region);

            return data;
        }
    }
}
