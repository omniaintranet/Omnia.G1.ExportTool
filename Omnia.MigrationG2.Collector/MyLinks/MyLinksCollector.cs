using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Omnia.MigrationG2.Collector.ExtensionMethods;
using Omnia.MigrationG2.Core.Logger;
using Omnia.MigrationG2.Intranet.Models.PersonalLinks;
using Omnia.MigrationG2.Intranet.Services.PersonalLinks;
namespace Omnia.MigrationG2.Collector.MyLinks
{
    public class MyLinksCollector : BaseCollectorService, IMyLinksCollector
    {
        private readonly IMyLinksService _commonlinksService;
        public MyLinksCollector(IMyLinksService commonlinksService)
        {
            _commonlinksService = commonlinksService;
        }

        public List<MyLink> GetAllPersonalLinks()
        {
            Logger.Info($"Checking commonlinks...");
            var result = _commonlinksService.GetAllPersonalLinks();
            Logger.Info($"--------------------------------------");
            return result;
        }

        public List<ReportItemMessage> GetReports()
        {
            return Logger.GetReport();
        }
    }
}
