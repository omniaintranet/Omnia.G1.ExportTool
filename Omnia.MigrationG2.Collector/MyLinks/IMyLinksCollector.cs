using Omnia.MigrationG2.Core.Logger;
using Omnia.MigrationG2.Intranet.Models.PersonalLinks;
using Omnia.MigrationG2.Intranet.Models.News;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Omnia.MigrationG2.Collector.MyLinks
{
    public interface IMyLinksCollector
    {
        List<MyLink> GetAllPersonalLinks();

        List<ReportItemMessage> GetReports();
    }
}
