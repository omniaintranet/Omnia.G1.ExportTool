using Omnia.MigrationG2.Intranet.Models.News;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Omnia.MigrationG2.Intranet.Services.News
{
    public interface INewsService
    {
        /// <summary>
        /// get the news centers
        /// </summary>
        /// <param name="siteUrl"></param>
        /// <returns></returns>
        NewsCenterResult GetNewsCenters(string siteUrl);

        /// <summary>
        /// get news from news center
        /// </summary>
        /// <param name="newsCenterUrl"></param>
        /// <returns></returns>
        IEnumerable<NewsItem> GetNews(string navigationSourceUrl, string newsCenterUrl, CultureInfo cultureInfo, DateTime? minDate);
        IEnumerable<NewsItem> GetCheckOutNews(string navigationSourceUrl, string newsCenterUrl, CultureInfo cultureInfo, DateTime? minDate);
    }
}
