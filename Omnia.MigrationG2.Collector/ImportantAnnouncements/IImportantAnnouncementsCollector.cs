using Omnia.MigrationG2.Core.Logger;
using Omnia.MigrationG2.Intranet.Models.ImportantAnnouncements;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Omnia.MigrationG2.Collector.ImportantAnnouncements
{
    public interface IImportantAnnouncementsCollector
    {
        List<Announcements> GetAnnouncements();

        List<ReportItemMessage> GetReports();
    }
}
