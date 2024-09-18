using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Omnia.MigrationG2.Foundation.Core.Configurations;
using Omnia.MigrationG2.Foundation.Core.Services;
using Omnia.MigrationG2.Intranet.Models.PersonalLinks;
using Omnia.MigrationG2.Intranet.Repositories.PersonalLinks;

namespace Omnia.MigrationG2.Intranet.Services.PersonalLinks
{
    public class MyLinksService : ClientContextService, IMyLinksService
    {
        private readonly IMyLinksRepository _linksRepository;

        public MyLinksService(IMyLinksRepository linksRepository)
        {
            _linksRepository = linksRepository;
        }
        public List<MyLink> GetAllPersonalLinks()
        {
            try
            {
                var linksFromDb = _linksRepository.GetAllPersonalLinks();
                return linksFromDb;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
