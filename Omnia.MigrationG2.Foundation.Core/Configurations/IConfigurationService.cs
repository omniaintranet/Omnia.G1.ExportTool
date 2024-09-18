using Omnia.MigrationG2.Foundation.Models.Configurations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Foundation.Core.Configurations
{
    public interface IConfigurationService
    {

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="region">The region.</param>
        /// <param name="extensionPackageId">The extensionPackageId.</param>
        /// <returns></returns>
        Configuration GetConfiguration(string name, string region, Guid? extensionPackageId = null);

        /// <summary>
        /// Adds or Updates the list of configurations.
        /// Gets the configurations in region.
        /// </summary>
        /// <param name="region">The region.</param>
        /// <returns></returns>
        IEnumerable<Models.Configurations.Configuration> GetConfigurationsInRegion(string region, Guid? extensionPackageId = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fromSiteUrl"></param>
        /// <param name="region"></param>
        /// <returns></returns>
        IEnumerable<Configuration> GetParentSiteConfigurations(string fromSiteUrl, string region);
    }
}
