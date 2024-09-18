using Omnia.MigrationG2.Intranet.Models.CommonLinks;
using Omnia.MigrationG2.Intranet.Models.Likes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Intranet.Repositories.CommonLinks
{
    public interface ILinksRepository
    {
        List<LinksItem> GetCommonLinks(); 
    }
}
