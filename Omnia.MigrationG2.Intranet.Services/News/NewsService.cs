using CamlexNET;
using Microsoft.Extensions.Logging;
using Microsoft.SharePoint.Client;
using Newtonsoft.Json;
using Omnia.MigrationG2.Foundation.Core.Features;
using Omnia.MigrationG2.Foundation.Core.Publishing;
using Omnia.MigrationG2.Foundation.Core.Services;
using Omnia.MigrationG2.Foundation.Core.SharePoint;
using Omnia.MigrationG2.Foundation.Core.Utilities;
using Omnia.MigrationG2.Foundation.Models.Features;
using Omnia.MigrationG2.Intranet.Core.ContentTypes.News;
using Omnia.MigrationG2.Intranet.Models.ContentManagement;
using Omnia.MigrationG2.Intranet.Models.News;
using Omnia.MigrationG2.Intranet.Services.ContentManagement;
using Omnia.MigrationG2.Intranet.Services.Navigations;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Omnia.MigrationG2.Intranet.Services.News
{
    public class NewsService : ClientContextService, INewsService
    {
        private const string CamlQueryTag = "Query";
        private const string CamlQueryWhereTag = "Where";
        private const string CamlViewTag = "View";

        private readonly IOmniaFeaturesService _omniaFeaturesService;
        private readonly IPublishingService _publishingService;
        private readonly INavigationService _navigationService;

        public NewsService(
            IOmniaFeaturesService omniaFeaturesService, 
            IPublishingService publishingService,
            INavigationService navigationService)
        {
            _omniaFeaturesService = omniaFeaturesService;
            _publishingService = publishingService;
            _navigationService = navigationService;
        }

        public IEnumerable<NewsItem> GetNews(string navigationSourceUrl, string newsCenterUrl, CultureInfo cultureInfo,DateTime? minDate)
        {
            IEnumerable<NewsItem> newsItemList = new List<NewsItem>();

            TimeZoneInfo webTimezoneInfo = null;
            IEnumerable<string> viewFields = new List<string>();

            // For migtation G2
            NewsCenterFilter query = new NewsCenterFilter()
            {
                NewsCenterUrl = newsCenterUrl
            };

            bool isDefault = false;
            var languages = _navigationService.GetNavigationLanguages(navigationSourceUrl);
            var defaultLanguage = languages.FirstOrDefault(language => language.IsDefault);
            if (defaultLanguage != null)
            {
                isDefault = defaultLanguage.LCID == cultureInfo.LCID ? true : false;
            }

            Elevate(newsCenterUrl, Tenant, (ctx) =>
            {
                try
                {
                    viewFields = BuildNewsViewFields();

                    Guid pageListId = RetrieveQueryInfo(ctx);
                    EnsureListQueryFilter(query, true, false);

                    ctx.Load(ctx.Web.RegionalSettings.TimeZone);
                    ctx.Load(ctx.Site, site => site.Url);
                    ctx.ExecuteQuery();

                    var listItems = GetNewsListItems(ctx, pageListId, query, viewFields);
                    if (minDate != null)
                    {
                        listItems = listItems.Where(newsItem =>
                        {
                            var date = (newsItem.FieldValues["Modified"] as DateTime?).Value;
                            if (date == null)
                            {
                                return false;
                            }
                            return date.CompareTo(minDate.Value) >= 0;
                        }).ToList();
                    }
                    //get check-out page - Diem
                    //var listcheckout = listItems.Where(newsItem => newsItem.File.CheckOutType != CheckOutType.None).ToList();
                    // get only check-in page -Diem
                    //listItems = listItems.Where(newsItem => newsItem.File.CheckOutType == CheckOutType.None).ToList();

                    listItems = listItems.Where(newsItem => newsItem.File.Level == FileLevel.Published).ToList();
                    

                    newsItemList = ToNewsItems(pageListId, listItems.ToList(), newsCenterUrl);

                    newsItemList = HandleTranslatedNews(ctx, pageListId, cultureInfo.Name, query, newsItemList, viewFields, newsCenterUrl);
                    newsItemList = newsItemList.Select(n => {
                        n.SiteCollectionRelativeUrl = ctx.Site.RootWeb.ServerRelativeUrl;
                        return n;
                    }).ToList();

                    newsItemList = newsItemList.Where(i =>
                    {
                        if (string.IsNullOrEmpty(i.PageLanguageLocale))
                        {
                            return isDefault;
                        }

                        return i.PageLanguageLocale.ToLower() == cultureInfo.Name.ToLower();
                    }).ToList();
                }
                catch (ServerUnauthorizedAccessException unauthorizedEx) // exclude events that user have not access
                {
                    throw;
                }
                catch (ServerException serverEx)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    throw;
                }
            });

            return newsItemList;
        }
        public IEnumerable<NewsItem> GetCheckOutNews(string navigationSourceUrl, string newsCenterUrl, CultureInfo cultureInfo, DateTime? minDate)
        {
            IEnumerable<NewsItem> newsItemList = new List<NewsItem>();

            TimeZoneInfo webTimezoneInfo = null;
            IEnumerable<string> viewFields = new List<string>();

            // For migtation G2
            NewsCenterFilter query = new NewsCenterFilter()
            {
                NewsCenterUrl = newsCenterUrl
            };

            bool isDefault = false;
            var languages = _navigationService.GetNavigationLanguages(navigationSourceUrl);
            var defaultLanguage = languages.FirstOrDefault(language => language.IsDefault);
            if (defaultLanguage != null)
            {
                isDefault = defaultLanguage.LCID == cultureInfo.LCID ? true : false;
            }

            Elevate(newsCenterUrl, Tenant, (ctx) =>
            {
                try
                {
                    viewFields = BuildNewsViewFields();

                    Guid pageListId = RetrieveQueryInfo(ctx);
                    EnsureListQueryFilter(query, true, false);

                    ctx.Load(ctx.Web.RegionalSettings.TimeZone);
                    ctx.Load(ctx.Site, site => site.Url);
                    ctx.ExecuteQuery();

                    var listItems = GetNewsListItems(ctx, pageListId, query, viewFields);
                    if (minDate != null)
                    {
                        listItems = listItems.Where(newsItem =>
                        {
                            var date = (newsItem.FieldValues["Modified"] as DateTime?).Value;
                            if (date == null)
                            {
                                return false;
                            }
                            return date.CompareTo(minDate.Value) >= 0;
                        }).ToList();
                    }
                    //get check-out page - Diem
                    listItems = listItems.Where(newsItem => newsItem.File.CheckOutType != CheckOutType.None).ToList();
                    // get only check-in page -Diem
                    //listItems = listItems.Where(newsItem => newsItem.File.CheckOutType == CheckOutType.None).ToList();

                    newsItemList = ToNewsItems(pageListId, listItems.ToList(), newsCenterUrl);

                    newsItemList = HandleTranslatedNews(ctx, pageListId, cultureInfo.Name, query, newsItemList, viewFields, newsCenterUrl);
                    newsItemList = newsItemList.Select(n => {
                        n.SiteCollectionRelativeUrl = ctx.Site.RootWeb.ServerRelativeUrl;
                        return n;
                    }).ToList();

                    newsItemList = newsItemList.Where(i =>
                    {
                        if (string.IsNullOrEmpty(i.PageLanguageLocale))
                        {
                            return isDefault;
                        }

                        return i.PageLanguageLocale.ToLower() == cultureInfo.Name.ToLower();
                    }).ToList();
                }
                catch (ServerUnauthorizedAccessException unauthorizedEx) // exclude events that user have not access
                {
                    throw;
                }
                catch (ServerException serverEx)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    throw;
                }
            });

            return newsItemList;
        }

        /// <summary>
        /// Gets the news center
        /// </summary>
        /// <param name="siteUrl"></param>
        /// <returns></returns>
        public NewsCenterResult GetNewsCenters(string siteUrl)
        {
            NewsCenterResult result = new NewsCenterResult()
            {
                NewsCenters = new List<NewsCenter>(),
                IsSiteNotFound = true
            };

            Elevate(siteUrl, Tenant, (ctx) =>
            {
                var newsCenters = new List<NewsCenter>();
                ctx.Load(ctx.Site, s => s.Url);
                ctx.Load(ctx.Web, w => w.ServerRelativeUrl, w => w.Title);
                ctx.ExecuteQuery();

                var newsCenterInstances = _omniaFeaturesService.GetFeatureInstances(new Guid("972159B0-5E20-4827-A9C5-EB6F85D4A46D"));

                if (newsCenterInstances != null)
                {
                    ctx.Load(ctx.Site.RootWeb, w => w.Url, w => w.Title);
                    ctx.ExecuteQuery();

                    var rootSiteUrl = ctx.Site.RootWeb.Url.ToLower();
                    var activeNewsCentersInCurrentSite = newsCenterInstances
                        .Where(newsCenter => newsCenter.Target.ToLower().Contains(rootSiteUrl) &&
                                             newsCenter.Status == FeatureInstanceStatus.Activated)
                        .ToList();

                    foreach (var newsCenter in activeNewsCentersInCurrentSite)
                    {
                        try
                        {
                            var newsCenterServerRelativeUrl = CommonUtils.GetServerRelativeUrl(newsCenter.Target);
                            if (!newsCenterServerRelativeUrl.StartsWith("/"))
                                newsCenterServerRelativeUrl = "/" + newsCenterServerRelativeUrl;

                            var newsCenterWeb = ctx.Site.OpenWeb(newsCenterServerRelativeUrl);
                            ctx.Load(newsCenterWeb, w => w.Url, w => w.Title, w => w.WebTemplate, w => w.ServerRelativeUrl);
                            ctx.ExecuteQuery();
                            
                            newsCenters.Add(new NewsCenter
                            {
                                Title = newsCenterWeb.Title,
                                Url = newsCenterWeb.Url,
                                ServerRelativeUrl = newsCenterWeb.ServerRelativeUrl,
                                WebTemplate = newsCenterWeb.WebTemplate,
                                SiteTitle = ctx.Site.RootWeb.Title,
                                SiteAbsoluteUrl = rootSiteUrl
                            });
                        }
                        catch (Exception ex)
                        {
                            Logger.Error($"Cannot check news center '{newsCenter.Target}'", ex.Message);
                        }
                    }
                }

                result = new NewsCenterResult
                {
                    NewsCenters = newsCenters,
                    IsSiteNotFound = false
                };
            });

            return result;
        }

        private IEnumerable<NewsItem> ToNewsItems(Guid pageListId, ICollection<ListItem> listItems, string newsCenterUrl)
        {
            var result = new List<NewsItem>();
            if (listItems != null && listItems.Count > 0)
            {
                string pageLanguage = Core.Constants.CustomFields.OmniaPageLanguage;
                string pageTargetLanguage = Core.Constants.CustomFields.OmniaPageTargetLanguage;
                string pageSourceLanguage = Core.Constants.CustomFields.OmniaPageSourceLanguage;
                foreach (var item in listItems)
                {
                    try
                    {
                        var pageSource = item[pageSourceLanguage] != null ? JsonConvert.DeserializeObject<PageLanguage>((string)item[pageSourceLanguage]) : null;
                        var pageTarget = item[pageTargetLanguage] != null ? JsonConvert.DeserializeObject<List<PageLanguage>>((string)item[pageTargetLanguage]) : null;
                        var pageLanguageLocale = item[pageLanguage] != null ? (string)item[pageLanguage] : "";
                        var newsItem = new NewsItem()
                        {
                            UniqueId = GetListItemUniqueId(pageListId, item),
                            Id = (int)item[Foundation.Core.Constants.SharePoint.Fields.ID],
                            Url = (string)item[Foundation.Core.Constants.SharePoint.Fields.FileRef],
                            Title = (string)item[Foundation.Core.Constants.SharePoint.Fields.Title],
                            PageLanguageLocale = pageLanguageLocale,
                            PageTargetLanguage = pageTarget,
                            PageSourceLanguage = pageSource,
                            ListId = pageListId
                        };

                        newsItem.NewsCenterUrl = newsCenterUrl;
                        result.Add(newsItem);
                    }
                    catch (Exception ex)
                    {
                        Logger.Error($"Cannot convert list item to news item with url '{item[Foundation.Core.Constants.SharePoint.Fields.FileRef]}'", ex);
                    }
                }
            }
            return result;
        }

        private IEnumerable<NewsItem> HandleTranslatedNews(ExtendedClientContext context, Guid listId, string pageLocale, NewsCenterFilter newsFilterQuery,
            IEnumerable<NewsItem> newsItems, IEnumerable<string> viewFields, string newsCenterUrl = "", bool includeSourcePageInfo = false)
        {
            if (pageLocale != null)
            {
                string requestPageLocale = pageLocale.ToLower();
                Dictionary<int, string> pageTargetDictionary = new Dictionary<int, string>();
                List<NewsItem> lstNewsItems = new List<NewsItem>();
                foreach (var news in newsItems)
                {
                    if (news.PageTargetLanguage != null && news.PageTargetLanguage.Any())
                    {
                        var targetPage = news.PageTargetLanguage.Where(n => n.Locale.ToLower() == requestPageLocale).FirstOrDefault();
                        if (targetPage != null)
                        {
                            pageTargetDictionary[news.Id] = targetPage.PageUrl.ToLower();
                        }
                    }
                }

                if (pageTargetDictionary.Any())
                {
                    EnsureTranslatedPageQueryFilter(newsFilterQuery, true);
                    var listItems = GetTranslatedNewsListItems(context, listId, newsFilterQuery, pageTargetDictionary.Select(item => item.Value).ToList(), viewFields);
                    var translatedNewsItems = ToNewsItems(listId, listItems.ToList(), newsCenterUrl);

                    if (translatedNewsItems != null && translatedNewsItems.Any())
                    {
                        foreach (var news in newsItems)
                        {
                            if (news.PageLanguageLocale.ToLower() == requestPageLocale)
                                lstNewsItems.Add(news);
                            else if (pageTargetDictionary.ContainsKey(news.Id))
                            {
                                var newsPage = translatedNewsItems.Where(t => t.Url.ToLower() == pageTargetDictionary[news.Id]).FirstOrDefault();
                                if (newsPage == null) lstNewsItems.Add(news);
                                else
                                {
                                    newsPage.Url = news.Url;
                                    if (includeSourcePageInfo)
                                    {
                                        newsPage.DefaultPageId = news.Id;
                                    }
                                    lstNewsItems.Add(newsPage);
                                }
                            }
                            else
                                lstNewsItems.Add(news);
                        }
                        return lstNewsItems;
                    }
                }
            }
            
            return newsItems;
        }

        private string GetListItemUniqueId(Guid pageListId, ListItem item)
        {
            return string.Format("{0}:{1}", pageListId.ToString(), item[Foundation.Core.Constants.SharePoint.Fields.ID]);
        }

        private DateTime GetDateTimeFieldValue(object fieldValue, TimeZoneInfo webTimezoneInfo = null)
        {
            if (webTimezoneInfo != null)
            {
                return SharePointUtils.GetDateFieldValue(webTimezoneInfo, fieldValue);
            }
            return (DateTime)fieldValue;
        }

        private IEnumerable<ListItem> GetNewsListItems(ExtendedClientContext context, Guid listId, NewsCenterFilter newsCenterFilters, IEnumerable<string> viewFields)
        {
            var list = context.Web.Lists.GetById(listId);
            var query = BuildListQuery(context, list, newsCenterFilters, viewFields);
            var items = list.GetItems(query);
            context.Load(context.Site.RootWeb, w => w.ServerRelativeUrl);

            //get checkouttype - Diem
            context.Load(items, a => a.IncludeWithDefaultProperties(item => item.File, item => item.File.CheckOutType));

            context.Load(context.Web.RegionalSettings.TimeZone);
            //context.Load(items);
            context.ExecuteQuery();

            return items.AsEnumerable();
        }

        private IEnumerable<ListItem> GetTranslatedNewsListItems(ExtendedClientContext context, Guid listId, NewsCenterFilter newsCenterFilters, List<string> translatedPageUrls, IEnumerable<string> viewFields)
        {
            var list = context.Web.Lists.GetById(listId);
            var query = BuildListQuery(list, newsCenterFilters, translatedPageUrls, viewFields);
            var items = list.GetItems(query);
            context.Load(items);
            context.ExecuteQuery();

            return items.AsEnumerable();
        }

        private CamlQuery BuildListQuery(List list, NewsCenterFilter newsCenterFilters, List<string> translatedPageUrls, IEnumerable<string> viewFields)
        {
            string customFilterCaml = string.Empty;
            var camlQuery = new CamlQuery();
            var query = Camlex.Query();

            query.ViewFields(viewFields);

            IList<Expression<Func<ListItem, bool>>> filterExpressions = new List<Expression<Func<ListItem, bool>>>();
            filterExpressions = GetListQueryFilters(this.Ctx, newsCenterFilters.Filters, out customFilterCaml);

            foreach (string pageUrl in translatedPageUrls)
            {
                filterExpressions.Add(GetTranslatedPageFilter(pageUrl));
            }

            if (filterExpressions.Count == 1)
                query.Where(filterExpressions[0]);
            else if (filterExpressions.Count > 1)
                query.WhereAny(filterExpressions.ToArray());

            camlQuery.ViewXml = JoinCamlQueriesWithCustomFilterCaml(query.ToString(true), customFilterCaml);
            return camlQuery;
        }

        private CamlQuery BuildListQuery(ClientContext context, List list, NewsCenterFilter newsCenterFilters, IEnumerable<string> viewFields)
        {
            string customFilterCaml = string.Empty;
            var camlQuery = new CamlQuery();
            var query = Camlex.Query();

            query.ViewFields(viewFields);

            IList<Expression<Func<ListItem, bool>>> filterExpressions = new List<Expression<Func<ListItem, bool>>>();
            filterExpressions = GetListQueryFilters(context, newsCenterFilters.Filters, out customFilterCaml);

            if (filterExpressions.Count == 1)
                query.Where(filterExpressions[0]);
            else if (filterExpressions.Count > 1)
                query.WhereAll(filterExpressions.ToArray());

            //if (newsQuery.SkipId > 0)
            //{
            //    ListItemCollectionPosition position = new ListItemCollectionPosition();
            //    position.PagingInfo = string.Format("Paged=TRUE&p_ID={0}", newsQuery.SkipId);
            //    camlQuery.ListItemCollectionPosition = position;
            //}

            camlQuery.ViewXml = JoinCamlQueriesWithCustomFilterCaml(query.ToString(true), customFilterCaml);

            return camlQuery;
        }

        private IList<Expression<Func<ListItem, bool>>> GetListQueryFilters(ClientContext context, ICollection<QueryFilter> filters, out string customFilterCaml)
        {
            var result = new List<Expression<Func<ListItem, bool>>>();
            customFilterCaml = string.Empty;
            var customFilterCamlQueries = new List<string>();
            if (filters != null)
            {
                foreach (var filter in filters)
                {
                    if (!string.IsNullOrEmpty(filter.Value) || !string.IsNullOrEmpty(filter.FromDateValue) || !string.IsNullOrEmpty(filter.ToDateValue))
                    {
                        switch (filter.TypeAsString)
                        {
                            case Foundation.Core.Fields.TextFieldAttribute.TypeAsStringValue:
                                customFilterCamlQueries.Add(GetCustomFilterCamlForStringField(filter));
                                break;
                        }
                    }
                    else
                    {
                        if (filter.Operator == QueryFilterOperator.IsNull)
                        {
                            result.Add(item => item[filter.FieldName] == null);
                        }
                    }
                }
                customFilterCaml = PatchAndCamlQueries(customFilterCamlQueries.ToArray());
            }
            return result;
        }

        private string JoinCamlQueriesWithCustomFilterCaml(string camlQuery, string customFilterCaml)
        {
            var result = camlQuery;
            if (!string.IsNullOrEmpty(customFilterCaml))
            {
                if (camlQuery.IndexOf(string.Format("<{0}>", CamlQueryWhereTag)) > -1)
                {
                    var camlQueryParts = SplitCamlQueryWithWhere(camlQuery);
                    var joinedCaml = PatchAndCamlQueries(camlQueryParts[1], customFilterCaml);
                    result = string.Format("{0}{1}{2}", camlQueryParts[0], joinedCaml, camlQueryParts[2]);
                }
                else if (camlQuery.IndexOf(string.Format("<{0}>", CamlQueryTag)) > -1)
                {
                    var queryStartIndex = camlQuery.IndexOf(string.Format("<{0}>", CamlQueryTag)) + CamlQueryTag.Length + 2;
                    result = string.Format("{0}{1}{2}",
                        camlQuery.Substring(0, queryStartIndex),
                        string.Format("<{0}>{1}</{0}>", CamlQueryWhereTag, customFilterCaml),
                        camlQuery.Substring(queryStartIndex));
                }
                else
                {
                    var viewStartIndex = camlQuery.IndexOf(string.Format("<{0}>", CamlViewTag)) + CamlViewTag.Length + 2;
                    result = string.Format("{0}{1}{2}",
                        camlQuery.Substring(0, viewStartIndex),
                        string.Format("<{0}><{1}>{2}</{1}></{0}>", CamlQueryTag, CamlQueryWhereTag, customFilterCaml),
                        camlQuery.Substring(viewStartIndex));

                }
            }
            return result;
        }

        private string PatchAndCamlQueries(params string[] camlQueries)
        {
            string result = string.Empty;
            for (int i = 0; i < camlQueries.Length; i++)
            {
                if (string.IsNullOrEmpty(camlQueries[i]))
                    continue;
                if (string.IsNullOrEmpty(result))
                    result = camlQueries[i];
                else
                    result = string.Format("<And>{0}{1}</And>", result, camlQueries[i]);
            }
            return result;
        }

        private string GetCustomFilterCamlForStringField(QueryFilter filter)
        {
            var result = string.Empty;

            Expression<Func<ListItem, bool>> expressions;

            if (filter.Operator != null)
            {
                switch (filter.Operator)
                {
                    case QueryFilterOperator.LessThan:
                        expressions = item => (int)item[filter.FieldName] < Convert.ToInt32(filter.Value);
                        break;
                    case QueryFilterOperator.GreaterThanOrEqual:
                        expressions = item => (int)item[filter.FieldName] >= Convert.ToInt32(filter.Value);
                        break;
                    case QueryFilterOperator.Contains:
                        expressions = item => ((string)item[filter.FieldName]).Contains(filter.Value);
                        break;
                    case QueryFilterOperator.StartsWith:
                        expressions = item => ((string)item[filter.FieldName]).StartsWith(filter.Value);
                        break;
                    case QueryFilterOperator.IsNull:
                        expressions = item => item[filter.FieldName] == null;
                        break;
                    default:
                        expressions = item => (string)item[filter.FieldName] == filter.Value;
                        break;
                }
            }
            else
            {
                expressions = item => (string)item[filter.FieldName] == filter.Value;
            }

            result = Camlex.Query().Where(expressions).ToString();

            if (!string.IsNullOrEmpty(result))
            {
                var camlQueryParts = SplitCamlQueryWithWhere(result);
                result = camlQueryParts[1];
                result = result.Replace("Type=\"Integer\"", "Type=\"" + filter.TypeAsString + "\"");
            }

            return result;
        }

        private string[] SplitCamlQueryWithWhere(string camlQuery)
        {
            var result = new string[3];
            if (!string.IsNullOrEmpty(camlQuery))
            {
                var whereStartIndex = camlQuery.IndexOf(string.Format("<{0}>", CamlQueryWhereTag)) + CamlQueryWhereTag.Length + 2;
                var whereEndIndex = camlQuery.IndexOf(string.Format("</{0}>", CamlQueryWhereTag));
                result[0] = camlQuery.Substring(0, whereStartIndex);
                result[1] = camlQuery.Substring(whereStartIndex, whereEndIndex - whereStartIndex);
                result[2] = camlQuery.Substring(whereEndIndex);
            }
            return result;
        }

        private IEnumerable<string> BuildNewsViewFields()
        {
            var result = new List<string>();

            result.Add(Foundation.Core.Constants.SharePoint.Fields.ID);
            result.Add(Foundation.Core.Constants.SharePoint.Fields.FileRef);
            result.Add(Foundation.Core.Constants.SharePoint.Fields.Title);
            //result.Add(Foundation.Core.Constants.SharePoint.Fields.Comments);
            //result.Add(Foundation.Core.Constants.SharePoint.Fields.PublishingPageImage);
            result.Add(Core.Constants.CustomFields.OmniaPageLanguage);
            result.Add(Core.Constants.CustomFields.OmniaPageSourceLanguage);
            result.Add(Core.Constants.CustomFields.OmniaPageTargetLanguage);
            //result.Add(Foundation.Core.Constants.SharePoint.Fields.ArticleStartDate);
            //result.Add(Core.Constants.CustomFields.OmniaGlueData);
            //result.Add(Core.Constants.CustomFields.OmniaCustomDataField);

            return result;
        }

        private Guid RetrieveQueryInfo(ExtendedClientContext context)
        {
            return _publishingService.GetPageListId(context, context.Url);
        }

        private Expression<Func<ListItem, bool>> GetTranslatedPageFilter(string pageUrl)
        {
            return item => (string)item[Foundation.Core.Constants.SharePoint.Fields.FileRef] == pageUrl;
        }

        private void EnsureListQueryFilter(NewsCenterFilter newsQuery, bool showPublishItemOnly, bool showDefaultPageOnly)
        {
            newsQuery.Filters = newsQuery.Filters != null ? newsQuery.Filters : new List<QueryFilter>();
            newsQuery.Filters.Add(CreateContentTypeFilter());
            //if (showDefaultPageOnly)
            //{
            //    newsQuery.Filters.Add(CreateTranslationFilter());
            //}

            if (showPublishItemOnly)
            {
                newsQuery.Filters.Add(CreateNewsVersionFilter());
            }
        }

        private void EnsureTranslatedPageQueryFilter(NewsCenterFilter newsQuery, bool showPublishItemOnly)
        {
            newsQuery.Filters = new List<QueryFilter>();
            newsQuery.Filters.Add(CreateContentTypeFilter());

            if (showPublishItemOnly)
            {
                newsQuery.Filters.Add(CreateNewsVersionFilter());
            }
        }

        private QueryFilter CreateContentTypeFilter()
        {
            var newsArticlePageContentType = new OmniaNewsArticlePage() as Foundation.Core.ContentTypes.ContentTypeBase;
            return new QueryFilter
            {
                FieldName = Foundation.Core.Constants.SharePoint.Fields.ContentTypeId,
                TypeAsString = Foundation.Core.Fields.TextFieldAttribute.TypeAsStringValue,
                Value = newsArticlePageContentType.IdAsString,
                Operator = QueryFilterOperator.StartsWith
            };
        }

        private QueryFilter CreateNewsVersionFilter()
        {
            return new QueryFilter
            {
                FieldName = Foundation.Core.Constants.SharePoint.Fields.UIVersionString,
                TypeAsString = Foundation.Core.Fields.TextFieldAttribute.TypeAsStringValue,
                Value = "1",
                Operator = QueryFilterOperator.GreaterThanOrEqual
            };
        }
    }
}
