using Omnia.MigrationG2.Collector.Models.G2;
using Omnia.MigrationG2.Intranet.Models.Navigations;
using Omnia.MigrationG2.Intranet.Models.News;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Omnia.MigrationG2.Collector.MigrationMapper
{
    public interface IMigrationMapper
    {
        List<string> GetSpecialList();

        NavigationMigrationItem MapToNavigationMigrationItem(NavigationNode rootNode,string navigationSourceUrl,string sharePointUrl);

        NavigationMigrationItem MapToNavigationMigrationItem(List<NewsItem> newsItems, string newsCenterUrl, string newsCenterTitle = null);
    }
}
