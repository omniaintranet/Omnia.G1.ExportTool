using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Omnia.MigrationG2.Intranet.Repositories;
using Omnia.MigrationG2.Intranet.Models.SearchProperties;

namespace Omnia.Intranet.Repositories.SearchProperties
{
    public class SearchPropertyRepository : DatabaseRepositoryBase, ISearchPropertyRepository
    {
        public List<SearchProperty> GetSearchProperties(SearchPropertyCategory category)
        {

            var allCategory = !Enum.IsDefined(typeof(SearchPropertyCategory), category);

            return OMIContext
                .SearchProperties
                .Where(p => p.TenantId == this.TenantId && (allCategory || p.Category == category))
                .Select(Mapper.Map<SearchProperty>)
                .ToList();
        }
    }
}
