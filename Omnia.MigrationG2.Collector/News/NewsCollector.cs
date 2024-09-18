using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Omnia.MigrationG2.Collector.ExtensionMethods;
using Omnia.MigrationG2.Core.Logger;
using Omnia.MigrationG2.Intranet.Models.News;
using Omnia.MigrationG2.Intranet.Services.Comments;
using Omnia.MigrationG2.Intranet.Services.ContentManagement;
using Omnia.MigrationG2.Intranet.Services.News;

namespace Omnia.MigrationG2.Collector.News
{
    public class NewsCollector : BaseCollectorService, INewsCollector
    {
        private readonly INewsService _newsService;
        private readonly IContentManagementService _contentManagementService;
        private readonly ICommentService _commentService;
        private List<string> NewsWithNonStaticBlocks = new List<string>();
        private List<string> NewsWithNoGlueData = new List<string>();

        public List<string> GetNewsWithNonStaticBlocks()
        {
            return NewsWithNonStaticBlocks;
        }

        public List<string> GetNewsWithNoGlueData()
        {
            return NewsWithNoGlueData;
        }

        public NewsCollector(
            INewsService newsService,
            IContentManagementService contentManagementService,
            ICommentService commentService)
        {
            _newsService = newsService;
            _contentManagementService = contentManagementService;
            _commentService = commentService;
        }

        public List<NewsItem> GetNews(string navigationSourceUrl, string newsCenterUrl, CultureInfo cultureInfo, ref int newsArticleIterator, DateTime? minDate = null)
        {
            Logger.Info($"Checking news center {newsCenterUrl}...");

            var newsItems = _newsService.GetNews(navigationSourceUrl, newsCenterUrl, cultureInfo, minDate).ToList();

            Logger.Info($"Found [{newsItems.Count}] news article(s).\n");

            foreach (var newsItem in newsItems)
            {
                try
                {

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Progress ({newsArticleIterator++}/{newsItems.Count})");
                    Console.ForegroundColor = ConsoleColor.White;
                    Logger.Info($"Checking news article '{newsItem.Title}' - {newsItem.Url}...");
                    newsItem.ListId = newsItem.ListId;
                    newsItem.PageInfo = _contentManagementService.GetQuickPageByPhysicalUrl(navigationSourceUrl, newsCenterUrl, newsItem.Url, cultureInfo.LCID, cultureInfo.Name);
                    newsItem.Likes = _commentService.GetLikeByPageId(newsItem.Id, newsItem.NewsCenterUrl);
                    newsItem.Comments = _commentService.GetComments(newsItem.Id, newsItem.NewsCenterUrl);
                    newsItem.TranslationPages = new List<NewsItem>();
                    if (newsItem.IsPageInfoAvailable())
                    {
                        Logger.Info($"Loaded page info.");
                        var glueData = newsItem.PageInfo.GetGlueDataField();
                        if (glueData == null)
                        {
                            NewsWithNoGlueData.Add(newsItem.Url);
                            Logger.Info($"Page has <No Glue Data Field>.");
                        }
                        else
                        {
                            if (glueData.Controls.Any(i => i.IsStatic == false))
                                NewsWithNoGlueData.Add(newsItem.Url);
                        }
                        newsItem.PageInfoPropertiesSettings = _contentManagementService.GetQuickPagePropertiesSettings(newsCenterUrl, newsItem.PageInfo.PageUrl, new Guid(newsItem.PageInfo.PageListId));
                        Logger.Success($"Loaded page properties. Done.\n", newsItem);

                        if (newsItem.PageInfo.TranslationPages != null)
                        {
                            foreach (var item in newsItem.PageInfo.TranslationPages)
                            {
                                var newsTranslationItem = new NewsItem()
                                {
                                    Title = item.Title,
                                    PageInfo = item,
                                    Url = item.SiteUrl,
                                    ListId = item.ListId,
                                    Likes = _commentService.GetLikeByPageId(item.PageItemId, newsItem.NewsCenterUrl),
                                    Comments = _commentService.GetComments(item.PageItemId, newsItem.NewsCenterUrl)
                                };
                                newsTranslationItem.PageInfoPropertiesSettings = _contentManagementService.GetQuickPagePropertiesSettings(newsCenterUrl, newsTranslationItem.PageInfo.PageUrl, new Guid(newsTranslationItem.PageInfo.PageListId));
                                newsItem.TranslationPages.Add(newsTranslationItem);
                            }
                        }
                    }
                    else
                    {
                        string errorMessage = "";
                        if (newsItem.PageInfo.IsAccessDenied)
                            errorMessage += "Access Denied; ";
                        if (newsItem.PageInfo.IsPageNotExist)
                            errorMessage += "Page Not Exist; ";
                        if (newsItem.PageInfo.IsSiteNotExist)
                            errorMessage += "Site Not Exist; ";
                        Logger.Error(errorMessage, newsItem);
                    }

                    //
                    //  }

                    //

                }
                catch (Exception ex)
                {
                    Logger.Error($"Cannot check page.\n", newsItem, ex);
                }
            }

            Logger.Info($"--------------------------------------");

            return newsItems;
        }
        public List<NewsItem> GeCheckOutNews(string navigationSourceUrl, string newsCenterUrl, CultureInfo cultureInfo, ref int newsArticleIterator, DateTime? minDate = null)
        {
            //Logger.Info($"Checking news center {newsCenterUrl}...");

            var newsItems = _newsService.GetCheckOutNews(navigationSourceUrl, newsCenterUrl, cultureInfo, minDate).ToList();

            Logger.Info($"Found [{newsItems.Count}] check-out news article(s).\n");

            foreach (var newsItem in newsItems)
            {
                try
                {

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Progress ({newsArticleIterator++}/{newsItems.Count})");
                    Console.ForegroundColor = ConsoleColor.White;
                    Logger.Info($"Checking news article '{newsItem.Title}' - {newsItem.Url}...");
                }
                catch (Exception ex)
                {
                    Logger.Error($"Cannot check page.\n", newsItem, ex);
                }
            }

            Logger.Info($"--------------------------------------");

            return newsItems;
        }

        public List<NewsCenter> GetNewsCenter(string navigationSourceUrl)
        {
            var newsCenterResult = _newsService.GetNewsCenters(navigationSourceUrl);
            return newsCenterResult.NewsCenters.ToList();

        }

        public List<ReportItemMessage> GetReports()
        {
            return Logger.GetReport();
        }
    }
}
