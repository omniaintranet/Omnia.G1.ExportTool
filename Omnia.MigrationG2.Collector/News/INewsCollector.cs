using Omnia.MigrationG2.Core.Logger;
using Omnia.MigrationG2.Intranet.Models.News;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Omnia.MigrationG2.Collector.News
{
    public interface INewsCollector
    {
        List<NewsCenter> GetNewsCenter(string navigationSourceUrl);

        List<NewsItem> GetNews(string navigationSourceUrl, string newsCenterUrl, CultureInfo cultureInfo, ref int nodeIterator, DateTime? minDate = null);

        List<ReportItemMessage> GetReports();

        List<string> GetNewsWithNonStaticBlocks();

        List<string> GetNewsWithNoGlueData();
        
        List<NewsItem> GeCheckOutNews(string navigationSourceUrl, string newsCenterUrl, CultureInfo cultureInfo, ref int newsArticleIterator, DateTime? minDate = null);
        
    }
}
