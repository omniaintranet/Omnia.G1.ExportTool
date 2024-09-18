using Omnia.MigrationG2.Foundation.Models.Tenants;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Foundation.Repositories.Tenants
{
    /// <summary>
    /// ITenantCloudRepository
    /// </summary>
    public interface ITenantCloudRepository
    {
        /// <summary>
        /// Returns current tenant
        /// </summary>
        /// <returns></returns>
        Tenant GetCurrentTenant();
    }
}
