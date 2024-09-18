using Omnia.MigrationG2.Intranet.Models.SearchProperties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Omnia.Intranet.Repositories.SearchProperties
{
    public interface ISearchPropertyRepository
    {
        List<SearchProperty> GetSearchProperties(SearchPropertyCategory category);
    }
}
