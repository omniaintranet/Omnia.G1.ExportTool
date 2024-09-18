using Omnia.MigrationG2.Core.Logger;
using Omnia.MigrationG2.Intranet.Models.CommonLinks;
using Omnia.MigrationG2.Intranet.Models.News;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Omnia.MigrationG2.Collector.CommonLinks
{
    public interface ICommonLinksCollector
    {
        List<LinksItem> GetCommonLinks();

        List<ReportItemMessage> GetReports();
    }
}
