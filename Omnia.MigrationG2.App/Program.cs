using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Omnia.MigrationG2.Collector.CommonLinks;
using Omnia.MigrationG2.Collector.Helpers;
using Omnia.MigrationG2.Collector.ImportantAnnouncements;
using Omnia.MigrationG2.Collector.MigrationMapper;
using Omnia.MigrationG2.Collector.Models.Reports;
using Omnia.MigrationG2.Collector.MyLinks;
using Omnia.MigrationG2.Collector.Navigations;
using Omnia.MigrationG2.Collector.News;
using Omnia.MigrationG2.Collector.Summary;
using Omnia.MigrationG2.Core;
using Omnia.MigrationG2.Core.Logger;
using Omnia.MigrationG2.Intranet.Models.ContentManagement;
using Omnia.MigrationG2.Intranet.Models.Navigations;
using Omnia.MigrationG2.Intranet.Models.News;
using Omnia.MigrationG2.Models.AppSettings;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Omnia.MigrationG2.Collector.Models.G2;
using static Omnia.MigrationG2.Collector.Models.G2.Enums;

namespace Omnia.MigrationG2.App
{
    class Program
    {
        private static int nodeIterator = 1;
        private static int newsArticleIterator = 1;
        static void Main(string[] args)
        {
            Console.WriteLine("Export G1 Tool - version 2.0.0 - 29 Mar 2022");

            Console.WriteLine("Initializing...");
            AppUtils.RegisterServiceProvider(Bootstrapper.ServiceProvider);
            AppUtils.RegisterConfiguration(Bootstrapper.Configuration);

            var settings = AppUtils.MigrationSettings;

            CultureInfo cultureInfo = GetLanguageCulture(settings.Language);

            var outputSettings = Bootstrapper
                .Configuration
                .GetSection(Constants.AppSettings.OutputSettingsSection).Get<OutputSettings>();


            CheckingNavigationNode(settings, cultureInfo, outputSettings);
            CheckingNewsCenter(settings, cultureInfo, outputSettings);
            CheckingCommonLinks(settings);
            CheckingMyLinks(settings);
            CheckingImportantAnnouncements(settings);
                               
            Console.WriteLine("Done. Press any key to end.");
            Console.ReadLine();
        }

        private static void CheckingNavigationNode(MigrationSettings settings, CultureInfo cultureInfo, OutputSettings outputSettings)
        {
            try
            {
                if (settings.CheckNavigationNodes)
               {
                    Console.WriteLine($"Collecting navigation nodes from '{settings.NavigationSourceUrl}'...");

                    var navigationNodesCollector = AppUtils.GetService<INavigationNodesCollector>();

                    var numberOfNavigationNodes = 0;
                    var nodes = navigationNodesCollector.GetHierarchyNavigationNodes(settings.NavigationSourceUrl, out numberOfNavigationNodes, settings.PageMinDate,settings.NavigationSiteUrl,settings.ParentId);

                    var rootNode = nodes.First();
                    Console.WriteLine("-------Navigation nodes-------");
                    DisplayNavigationNode(rootNode);
                    Console.WriteLine("------------------------------");
                    Console.WriteLine($"Found [{numberOfNavigationNodes}] nodes.\n\n");

                    Console.WriteLine("Collecting page info and page properties from SP...");
                    navigationNodesCollector.LoadHierarchyNavigationNodesWithQuickPageInfo(settings.NavigationSourceUrl, rootNode, cultureInfo, numberOfNavigationNodes, ref nodeIterator);
                    var reportItems = navigationNodesCollector.GetReports();
                    SummaryReport summaryReport = new SummaryReport();
                    summaryReport.SiteUrl = settings.NavigationSourceUrl;
                    summaryReport.NumberOfPages = numberOfNavigationNodes;
                    summaryReport.SuccessPages = reportItems.Where(i => i.Status == ReportStatus.Success).Select(i => (i as ReportItem<NavigationNode>).Data.Url).ToList();
                    summaryReport.FailedPages = reportItems.Where(i => i.Status == ReportStatus.Failed).Select(i => new FailedPagesModel { ErrorMessage = i.ErrorMessage, Exception = i.Exception?.Message }).ToList();
                    summaryReport.NumberOfSuccessCollectedPage = summaryReport.SuccessPages.Count;
                    summaryReport.NumberOfFailedCollectedPage = summaryReport.FailedPages.Count;
                    summaryReport.PagesWithNonStaticBlocks = navigationNodesCollector.GetNodesWithNonStaticBlocks();
                    summaryReport.PagesWithNoGlueDataField = navigationNodesCollector.GetNodesWithNoGlueData();
                    //20211222 - Diem : Get checked-out nodes
                    summaryReport.CheckOutPages = navigationNodesCollector.GetNodesWithCheckedOut();
                    summaryReport.NumberOfCheckOutPage = summaryReport.CheckOutPages.Count;

                    summaryReport.ContentManagementProperties = ContentManagementPropertiesHelper.GetContentManagementProperties(rootNode);

                    Console.WriteLine("Converting data to migration items...");
                    var migrationMapper = AppUtils.GetService<IMigrationMapper>();

                    var rootMigrationItem = migrationMapper.MapToNavigationMigrationItem(rootNode,settings.NavigationSourceUrl,settings.SharePointUrl);
                    summaryReport.SitesWithEmptyUserFieldEmail = migrationMapper.GetSpecialList();


                    Console.WriteLine("Creating files...");
                    var summaryDataCollector = AppUtils.GetService<ISummaryDataCollector>();
                    summaryDataCollector.getData(rootMigrationItem, GetFileName(settings.NavigationSourceUrl, outputSettings, true, false), settings);
                    DirectoryInfo info = Directory.CreateDirectory($"{Directory.GetCurrentDirectory()}\\ExportData");
                    File.WriteAllText(
                        info.ToString() + "\\" + GetFileName(settings.NavigationSourceUrl, outputSettings, false, false),
                        JsonConvert.SerializeObject(rootMigrationItem));
                    File.WriteAllText(
                        info.ToString() + "\\" + GetFileName(settings.NavigationSourceUrl, outputSettings, true, false),
                        JsonConvert.SerializeObject(summaryReport));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error at processing navigation nodes:"+ e.Message);
                return;
            }
        }

        private static void CheckingNewsCenter(MigrationSettings settings, CultureInfo cultureInfo, OutputSettings outputSettings)
        {
            try
            {
                if (settings.CheckNewsCenters)
                {
                    if (string.IsNullOrEmpty(settings.NewsCenterUrl))
                    {
                        Console.WriteLine($"Collecting news centers from '{settings.NavigationSourceUrl}'...");

                        var newsCollector = AppUtils.GetService<INewsCollector>();

                        var newsCenters = newsCollector.GetNewsCenter(settings.NavigationSourceUrl);

                        Console.WriteLine("----------News Center---------");
                        foreach (var newsCenter in newsCenters)
                        {
                            Console.WriteLine(newsCenter.Title + " - " + newsCenter.Url);
                        }
                        Console.WriteLine("------------------------------");
                        Console.WriteLine($"Found [{newsCenters.Count}] news center(s).\n");

                        Console.WriteLine("Collecting news articles from SP...");
                        foreach (var newsCenter in newsCenters)
                        {
                            newsArticleIterator = 1;
                            var newsItems = newsCollector.GetNews(settings.NavigationSourceUrl, newsCenter.Url, cultureInfo, ref newsArticleIterator,settings.PageMinDate);
                            //get check-out page - Diem
                            var newsCheckOutItems = newsCollector.GeCheckOutNews(settings.NavigationSourceUrl, newsCenter.Url, cultureInfo, ref newsArticleIterator, settings.PageMinDate);
                            
                            var reportItems = newsCollector.GetReports();

                            SummaryReport summaryReport = new SummaryReport();
                            summaryReport.SiteUrl = newsCenter.Url;
                            summaryReport.NumberOfPages = newsItems.Count;
                            //number of checkout page - Diem
                            summaryReport.NumberOfCheckOutPage = newsCheckOutItems.Count;
                            summaryReport.CheckOutPages = newsCheckOutItems.Select(i => i.Url).ToList();

                            summaryReport.SuccessPages = reportItems.Where(i => i.Status == ReportStatus.Success).Select(i => (i as ReportItem<NewsItem>).Data.Url).ToList();
                            summaryReport.FailedPages = reportItems.Where(i => i.Status == ReportStatus.Failed).Select(i => new FailedPagesModel { ErrorMessage = i.ErrorMessage, Exception = i.Exception?.Message }).ToList();
                            summaryReport.NumberOfSuccessCollectedPage = summaryReport.SuccessPages.Count;
                            summaryReport.NumberOfFailedCollectedPage = summaryReport.FailedPages.Count;
                            summaryReport.PagesWithNonStaticBlocks = newsCollector.GetNewsWithNonStaticBlocks();
                            summaryReport.PagesWithNoGlueDataField = newsCollector.GetNewsWithNoGlueData();
                            summaryReport.ContentManagementProperties = ContentManagementPropertiesHelper.GetContentManagementProperties(newsItems);

                            Console.WriteLine("Converting data to migration items...");
                            var migrationMapper = AppUtils.GetService<IMigrationMapper>();

                            List<string> nonStaticBlockUrls = new List<string>();
                            var rootMigrationItem = migrationMapper.MapToNavigationMigrationItem(newsItems, newsCenter.Url, newsCenter.Title);
                            summaryReport.SitesWithEmptyUserFieldEmail = migrationMapper.GetSpecialList();

                            Console.WriteLine("Creating files...");
                            var summaryDataCollector = AppUtils.GetService<ISummaryDataCollector>();
                            summaryDataCollector.getData(rootMigrationItem, GetFileName(settings.NavigationSourceUrl, outputSettings, true, true), settings);
                            DirectoryInfo info = Directory.CreateDirectory($"{Directory.GetCurrentDirectory()}\\ExportData");
                            File.WriteAllText(
                                info.ToString() + "\\" + GetFileName(newsCenter.Url, outputSettings, false, true),
                                JsonConvert.SerializeObject(rootMigrationItem), Encoding.Unicode);
                            File.WriteAllText(
                                info.ToString() + "\\" + GetFileName(newsCenter.Url, outputSettings, true, true),
                                JsonConvert.SerializeObject(summaryReport), Encoding.Unicode);
                        }
                    }
                    else
                    {
                        var newsCollector = AppUtils.GetService<INewsCollector>();
                        Console.WriteLine("Collecting news articles from SP...");
                        var newsItems = newsCollector.GetNews(settings.NavigationSourceUrl, settings.NewsCenterUrl, cultureInfo, ref newsArticleIterator, settings.PageMinDate);
                        //get check-out page - Diem
                        var newsCheckOutItems = newsCollector.GeCheckOutNews(settings.NavigationSourceUrl, settings.NewsCenterUrl, cultureInfo, ref newsArticleIterator, settings.PageMinDate);

                        var reportItems = newsCollector.GetReports();

                        SummaryReport summaryReport = new SummaryReport();
                        summaryReport.SiteUrl = settings.NewsCenterUrl;
                        summaryReport.NumberOfPages = newsItems.Count;
                        //number of checkout page - Diem
                        summaryReport.NumberOfCheckOutPage = newsCheckOutItems.Count;
                        summaryReport.CheckOutPages = newsCheckOutItems.Select(i => i.Url).ToList();

                        summaryReport.SuccessPages = reportItems.Where(i => i.Status == ReportStatus.Success).Select(i => i != null ? (i as ReportItem<NewsItem>).Data.Url : "").ToList();
                        summaryReport.FailedPages = reportItems.Where(i => i.Status == ReportStatus.Failed).Select(i => new FailedPagesModel { ErrorMessage = i.ErrorMessage, Exception = i.Exception?.Message }).ToList();
                        summaryReport.NumberOfSuccessCollectedPage = summaryReport.SuccessPages.Count;
                        summaryReport.NumberOfFailedCollectedPage = summaryReport.FailedPages.Count;
                        summaryReport.PagesWithNonStaticBlocks = newsCollector.GetNewsWithNonStaticBlocks();
                        summaryReport.PagesWithNoGlueDataField = newsCollector.GetNewsWithNoGlueData();
                        summaryReport.ContentManagementProperties = ContentManagementPropertiesHelper.GetContentManagementProperties(newsItems);

                        Console.WriteLine("Converting data to migration items...");
                        var migrationMapper = AppUtils.GetService<IMigrationMapper>();

                        List<string> nonStaticBlockUrls = new List<string>();
                        var rootMigrationItem = migrationMapper.MapToNavigationMigrationItem(newsItems, settings.NewsCenterUrl);
                        summaryReport.SitesWithEmptyUserFieldEmail = migrationMapper.GetSpecialList();

                        var data = JsonConvert.SerializeObject(rootMigrationItem);

                        Console.WriteLine("Creating files...");
                        var summaryDataCollector = AppUtils.GetService<ISummaryDataCollector>();
                        summaryDataCollector.getData(rootMigrationItem, GetFileName(settings.NavigationSourceUrl, outputSettings, true, true), settings);
                        DirectoryInfo info = Directory.CreateDirectory($"{Directory.GetCurrentDirectory()}\\ExportData");
                        File.WriteAllText(
                            info.ToString() + "\\" + GetFileName(settings.NewsCenterUrl, outputSettings, false, true), data, Encoding.Unicode);
                        File.WriteAllText(
                            info.ToString() + "\\" + GetFileName(settings.NewsCenterUrl, outputSettings, true, true),
                            JsonConvert.SerializeObject(summaryReport));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at checking news center" + ":" + ex);
                return;
            }
        }

        private static void CheckingCommonLinks(MigrationSettings settings)
        {
            try
            {
                if (settings.CheckCommonLinks)
                {
                    Console.WriteLine($"Collecting commonlinks ...");

                    var commonLinksCollector = AppUtils.GetService<ICommonLinksCollector>();
                    var commonlinks = commonLinksCollector.GetCommonLinks();

                    Console.WriteLine("Creating files...");
                    DirectoryInfo info = Directory.CreateDirectory($"{Directory.GetCurrentDirectory()}\\ExportData");
                    File.WriteAllText(
                        info.ToString() + $"\\Data.CommonLinks.{Regex.Matches(settings.NavigationSourceUrl, "(?<=https:\\/\\/).*(?=\\.sharepoint)")[0].ToString()}.json",
                        JsonConvert.SerializeObject(commonlinks));
                }
            }
            catch(Exception)
            {
                Console.WriteLine("Error at checking common links");
                return;
            }
        }

        private static void CheckingMyLinks(MigrationSettings settings)
        {
            try
            {
                if (settings.CheckMyLinks)
                {
                    Console.WriteLine($"Collecting my links ...");

                    var myLinksCollector = AppUtils.GetService<IMyLinksCollector>();
                    var mylinks = myLinksCollector.GetAllPersonalLinks();
                    Console.WriteLine("Creating files...");
                    DirectoryInfo info = Directory.CreateDirectory($"{Directory.GetCurrentDirectory()}\\ExportData");
                    File.WriteAllText(
                        info.ToString() + $"\\Data.MyLinks.{Regex.Matches(settings.NavigationSourceUrl, "(?<=https:\\/\\/).*(?=\\.sharepoint)")[0].ToString()}.json",
                        JsonConvert.SerializeObject(mylinks));
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error at checking my links" ,ex);
                return;
            }
        }

        private static void CheckingImportantAnnouncements(MigrationSettings settings)
        {
            try
            {
                if (settings.CheckImportantAnnouncements)
                {
                    Console.WriteLine($"Collecting important announcements ...");
                    var announcementsCollector = AppUtils.GetService<IImportantAnnouncementsCollector>();
                    var announcements = announcementsCollector.GetAnnouncements();

                    Console.WriteLine("Creating files...");
                    DirectoryInfo info = Directory.CreateDirectory($"{Directory.GetCurrentDirectory()}\\ExportData");
                    File.WriteAllText(
                        info.ToString() + $"\\Data.ImportantAnnouncements.{Regex.Matches(settings.NavigationSourceUrl, "(?<=https:\\/\\/).*(?=\\.sharepoint)")[0].ToString()}.json",
                        JsonConvert.SerializeObject(announcements));
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Error at checking important announcements");
                return;
            }
        }

        private static CultureInfo GetLanguageCulture(string locale)
        {
            try
            {
                return CultureInfo.GetCultureInfo(locale);
            }
            catch (Exception) { return null; }
        }

        private static void DisplayNavigationNode(NavigationNode node, int padding = 0)
        {
            for (int i = 0; i < padding; i++)
            {
                Console.Write("  ");
            }

            Console.Write($"{node.Labels.First().Value}\n");
            foreach (var childNode in node.Children)
            {
                DisplayNavigationNode(childNode, padding + 1);
            }
        }

        private static string GetFileName(string url, OutputSettings outputSettings, bool isReport, bool isNews)
        {
            var parts = url.Split("/");
            var urlSegment = parts[parts.Length-2] + "." + parts.Last();

            //Diem
            var fDate = DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss");

            if (isReport)
            {
                return outputSettings
                    .ReportFileName
                    .Replace("[type]", isNews ? "NewsArticles" : "Navigation")
                    .Replace("[site]", urlSegment)
                    .Replace("[url]", urlSegment)
                    .Replace("[time]", fDate);
            }

            return outputSettings
                .DataFileName
                .Replace("[type]", isNews ? "NewsArticles" : "Navigation")
                .Replace("[site]", urlSegment)
                .Replace("[url]", urlSegment)
                .Replace("[time]", fDate);
                
        }
               
    }
}
