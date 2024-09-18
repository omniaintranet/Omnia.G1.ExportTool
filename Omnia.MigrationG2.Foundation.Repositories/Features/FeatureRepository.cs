using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper.QueryableExtensions;
using Omnia.MigrationG2.Foundation.Models.Features;

namespace Omnia.MigrationG2.Foundation.Repositories.Features
{
    public class FeatureRepository : DatabaseRepositoryBase, IFeatureRepository
    {
        public IEnumerable<FeatureInstance> GetFeatureInstances(Guid appFeatureId)
        {
            try
            {
                return OMFContext.FeatureInstances
                    .Where
                    (
                        q => q.FeatureId == appFeatureId
                        && (q.TenantId == this.TenantId || q.TenantId == new Guid("FF000000-0000-FFFF-0000-0000000000FF"))
                        && q.DeletedAt == null
                    )
                    .ProjectTo<Models.Features.FeatureInstance>()
                    .AsEnumerable();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
