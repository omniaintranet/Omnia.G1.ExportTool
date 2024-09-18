using Omnia.MigrationG2.Core.Logger;
using Omnia.MigrationG2.Intranet.Models.Navigations;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Omnia.MigrationG2.Collector.Navigations
{
    public interface INavigationNodesCollector
    {
        IList<NavigationNode> GetHierarchyNavigationNodes(string navigationSourceUrl, out int numberOfNavigationNode, DateTime? minDate = null, string navigationSiteUrl = null, string parentId = null);

        void LoadHierarchyNavigationNodesWithQuickPageInfo(string navigationSourceUrl, NavigationNode rootNode, CultureInfo cultureInfo, int numberOfNavigationNodes, ref int nodeIterator);

        List<ReportItemMessage> GetReports();

        List<string> GetNodesWithNonStaticBlocks();

        List<string> GetNodesWithNoGlueData();
        List<string> GetNodesWithCheckedOut();
    }
}
