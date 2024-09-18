using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Omnia.MigrationG2.Foundation.Core.Configurations;
using Omnia.MigrationG2.Foundation.Core.Services;
using Omnia.MigrationG2.Intranet.Models.CommonLinks;
using Omnia.MigrationG2.Intranet.Repositories.CommonLinks;

namespace Omnia.MigrationG2.Intranet.Services.CommonLinks
{
    public class CommonLinksService : ClientContextService, ICommonLinksService
    {
        private readonly ILinksRepository _linksRepository;

        public CommonLinksService(ILinksRepository linksRepository)
        {
            _linksRepository = linksRepository;
        }
        public List<LinksItem> GetCommonLinks ()
        {
            try
            {
                var linksFromDb = _linksRepository.GetCommonLinks();
                return linksFromDb;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
