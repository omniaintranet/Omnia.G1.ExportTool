using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Omnia.MigrationG2.Collector.ExtensionMethods;
using Omnia.MigrationG2.Core.Logger;
using Omnia.MigrationG2.Intranet.Models.CommonLinks;
using Omnia.MigrationG2.Intranet.Services.CommonLinks;
namespace Omnia.MigrationG2.Collector.CommonLinks
{
    public class CommonLinksCollector : BaseCollectorService, ICommonLinksCollector
    {
        private readonly ICommonLinksService _commonlinksService;
        public CommonLinksCollector(ICommonLinksService commonlinksService)
        {
            _commonlinksService = commonlinksService;
        }

        public List<LinksItem> GetCommonLinks()
        {
            Logger.Info($"Checking commonlinks...");
            var result = _commonlinksService.GetCommonLinks();
            Logger.Info($"--------------------------------------");
            return result;
        }

        public List<ReportItemMessage> GetReports()
        {
            return Logger.GetReport();
        }
    }
}
