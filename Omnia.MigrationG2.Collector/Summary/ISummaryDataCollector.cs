using System.Collections.Generic;
using Omnia.MigrationG2.Collector.Models.G2;
using Omnia.MigrationG2.Models.AppSettings;

namespace Omnia.MigrationG2.Collector.Summary
{
    public interface ISummaryDataCollector
    {
        void getData(List<NavigationMigrationItem> input, string siteName, MigrationSettings migrationSettings);

        void getData(NavigationMigrationItem input, string siteName, MigrationSettings migrationSettings);
    }
}