using AutoMapper;
using Microsoft.Extensions.Options;
using Omnia.MigrationG2.Foundation.Models.OmniaProfiles;
using Omnia.MigrationG2.Models.AppSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omnia.MigrationG2.Foundation.Repositories.OmniaProfiles
{
    public class OmniaProfileRepository : DatabaseRepositoryBase, IOmniaProfileRepository
    {
        public IList<OmniaProfile> GetAll()
        {
            try
            {
                var items = OMFContext
                    .OmniaProfiles
                    .Where(item => item.TenantId == this.TenantId && item.DeletedAt == null)
                    .Select(Mapper.Map<OmniaProfile>)
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
