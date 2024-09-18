using Omnia.MigrationG2.Intranet.Models.PersonalLinks;
using Omnia.MigrationG2.Intranet.Models.Likes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Intranet.Repositories.PersonalLinks
{
    public interface IMyLinksRepository
    {
        List<MyLink> GetAllPersonalLinks(); 
    }
}
