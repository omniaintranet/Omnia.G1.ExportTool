using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Microsoft.Extensions.Options;
using Omnia.MigrationG2.Core;
using Omnia.MigrationG2.Intranet.Models.CommonLinks;

namespace Omnia.MigrationG2.Intranet.Repositories.CommonLinks
{
    public class LinksRepository : DatabaseRepositoryBase, ILinksRepository
    {
        public List<LinksItem> GetCommonLinks()
        {
            try
            {
                var items = OMIContext
                    .Links
                    .Where(item => item.TenantId == TenantId && item.DeletedAt == null)
                    .Select(Mapper.Map<LinksItem>)
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
