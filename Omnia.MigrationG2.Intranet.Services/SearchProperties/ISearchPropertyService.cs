using Omnia.MigrationG2.Intranet.Models.SearchProperties;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Intranet.Services.SearchProperties
{
    public interface ISearchPropertyService
    {
        List<SearchProperty> GetSearchProperties(SearchPropertyCategory category);
    }
}
