using AutoMapper;
using Microsoft.Extensions.Options;
using Omnia.MigrationG2.Foundation.Models.Tenants;
using Omnia.MigrationG2.Models.AppSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omnia.MigrationG2.Foundation.Repositories.Tenants
{
    /// <summary>
    /// The TenantCloud Repository
    /// </summary>
    public class TenantCloudRepository : DatabaseRepositoryBase, ITenantCloudRepository
    {
        public Tenant GetCurrentTenant()
        {
            try
            {
                return OMFContext
                    .Tenants
                    .Where
                    (
                        q => q.Id == this.TenantId
                        && q.DeletedAt == null
                    )
                    .Select(Mapper.Map<Tenant>)
                    .FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
