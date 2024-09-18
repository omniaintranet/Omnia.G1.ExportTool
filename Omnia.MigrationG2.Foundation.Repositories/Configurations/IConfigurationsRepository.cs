using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Foundation.Repositories.Configurations
{
    public interface IConfigurationsRepository
    {
        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="region">The region.</param>
        /// <returns></returns>
        Models.Configurations.Configuration GetConfiguration(string name, string region, Guid? extensionPackageId = null);

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
        /// <param name="extensionPackageId"></param>
        /// <returns></returns>
        IEnumerable<Models.Configurations.Configuration> GetParentSiteConfigurations(string fromSiteUrl, string region, Guid? extensionPackageId = null);
    }
}
