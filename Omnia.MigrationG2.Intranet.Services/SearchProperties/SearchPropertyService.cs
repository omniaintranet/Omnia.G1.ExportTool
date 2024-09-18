using System;
using System.Collections.Generic;
using System.Text;
using Omnia.Intranet.Repositories.SearchProperties;
using Omnia.MigrationG2.Foundation.Core.Services;
using Omnia.MigrationG2.Intranet.Models.SearchProperties;

namespace Omnia.MigrationG2.Intranet.Services.SearchProperties
{
    public class SearchPropertyService : ClientContextService, ISearchPropertyService
    {
        private readonly ISearchPropertyRepository _searchPropsRepository;

        public SearchPropertyService(ISearchPropertyRepository searchPropsRepository)
        {
            _searchPropsRepository = searchPropsRepository;
        }
        public List<SearchProperty> GetSearchProperties(SearchPropertyCategory category)
        {
            return _searchPropsRepository.GetSearchProperties(category);
        }
    }
}
