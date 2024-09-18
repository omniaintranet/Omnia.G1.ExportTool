using Microsoft.Extensions.Options;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Taxonomy;
using Newtonsoft.Json;
using Omnia.MigrationG2.Core;
using Omnia.MigrationG2.Foundation.Core.Configurations;
using Omnia.MigrationG2.Foundation.Core.Publishing;
using Omnia.MigrationG2.Foundation.Core.Services;
using Omnia.MigrationG2.Foundation.Core.SharePoint;
using Omnia.MigrationG2.Foundation.Core.Utilities;
using Omnia.MigrationG2.Intranet.Models.ContentManagement;
using Omnia.MigrationG2.Intranet.Services.Navigations;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using IntranetCoreUtils = Omnia.MigrationG2.Intranet.Core.Utils;

namespace Omnia.MigrationG2.Intranet.Services.ContentManagement
{
    public class ContentManagementService : ClientContextService, IContentManagementService
    {
        private readonly IConfigurationService _configurationService;
        private readonly INavigationService _navigationService;
        private readonly IPublishingService _publishingService;

        private Dictionary<string, ContentManagementSettings> contentManagementSettings = new Dictionary<string, ContentManagementSettings>();

        public ContentManagementService(IConfigurationService configurationService, INavigationService navigationService, IPublishingService publishingService)
        {
            _configurationService = configurationService;
            _navigationService = navigationService;
            _publishingService = publishingService;
        }

        public PageInfo GetQuickPage(string navigationSourceUrl, Models.Navigations.NavigationNode navigationNode, int lcid, string locale = null, bool isGlueEdit = false)
        {
            try
            {
                PageInfo result = null;
                bool forceGetSelectLocale = false;

                //Translation page
                if (!string.IsNullOrEmpty(locale))
                {
                    var pageCulture = GetLanguageCulture(locale);
                    if (pageCulture != null)
                    {
                        lcid = pageCulture.LCID;
                        forceGetSelectLocale = true;
                    }
                }

                if (navigationNode != null && !string.IsNullOrEmpty(navigationNode.TargetUrl))
                {
                    //Support old cached navigation node that does not have SiteUrl
                    if (string.IsNullOrEmpty(navigationNode.SiteUrl))
                    {
                        navigationNode.SiteUrl = NavigationMapper.GetNavigationNodeSiteUrl(IntranetCoreUtils.CommonUtils.GetHostUrl(Ctx.Url), navigationNode.TargetUrl);
                    }

                    Elevate(navigationNode.SiteUrl, Tenant, (appContext) =>
                    {
                        result = GetQuickPageInWeb(navigationSourceUrl, appContext, navigationNode.TargetUrl, lcid, false, forceGetSelectLocale, isGlueEdit);
                    });

                    /* Navigation properties */
                    int pageLCID = 0;
                    if (forceGetSelectLocale)
                        pageLCID = lcid;
                    else
                        pageLCID = GetLCID(lcid, result);

                    var termDescription = navigationNode.Descriptions.FirstOrDefault(item => item.LCID == pageLCID);
                    if (termDescription == null && !forceGetSelectLocale)
                    {
                        termDescription = navigationNode.Descriptions.FirstOrDefault();
                    }
                    if (termDescription != null)
                        result.NavigationDescription = IntranetCoreUtils.CommonUtils.EnsureDisplayTermString(termDescription.Value);

                    var termLabel = navigationNode.Labels.FirstOrDefault(l => l.LCID == pageLCID);
                    if (termLabel == null && !forceGetSelectLocale)
                    {
                        termLabel = navigationNode.Labels.FirstOrDefault();
                    }
                    if (termLabel != null)
                        result.NavigationTitle = IntranetCoreUtils.CommonUtils.EnsureDisplayTermString(termLabel.Value);

                    result.SiteUrl = navigationNode.SiteUrl;

                    result.TopNodeKey = NavigationHelper.GenerateTopNodeKey(navigationSourceUrl, navigationNode);
                    
                    //result.NavigationNode = navigationNode;

                    this.CheckIfMainPageNotMapDefaultLanguage(result, navigationSourceUrl);

                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PageInfo GetQuickPageByPhysicalUrl(string navigationSourceUrl, string siteUrl, string pageUrl, int lcid, string locale = null, bool isGlueEdit = false)
        {
            try
            {
                PageInfo result = null;
                bool forceGetSelectLocale = false;
                if (!string.IsNullOrEmpty(locale))
                {
                    var pageCulture = GetLanguageCulture(locale);
                    if (pageCulture != null)
                    {
                        lcid = pageCulture.LCID;
                        forceGetSelectLocale = true;
                    }
                }

                Elevate(siteUrl, Tenant, (appContext) =>
                {
                    result = GetQuickPageInWeb(siteUrl, appContext, pageUrl, lcid, true, forceGetSelectLocale, isGlueEdit);
                    result.SiteUrl = appContext.Url;
                });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        private void CheckIfMainPageNotMapDefaultLanguage(PageInfo pageInfo, string navigationSourceUrl)
        {
            if (!string.IsNullOrEmpty(navigationSourceUrl))
            {
                var languages = _navigationService.GetNavigationLanguages(navigationSourceUrl);
                var defaultLanguage = languages.FirstOrDefault(language => language.IsDefault);

                pageInfo.NeedToCorrectDefaultLanguage = defaultLanguage != null && !string.IsNullOrEmpty(pageInfo.PageLanguageLocale) &&
                    (pageInfo.PageSourceLanguage != null && pageInfo.PageSourceLanguage.Locale.ToLower() != defaultLanguage.Name.ToLower()
                    ||
                    pageInfo.PageSourceLanguage == null && pageInfo.PageLanguageLocale.ToLower() != defaultLanguage.Name.ToLower());
            }
        }

        private PageInfo GetQuickPageInWeb(string navigationSourceUrl, ExtendedClientContext context, string pageRelativeUrl, int lcid, bool isPhysicalPage = false, bool forceGetSelectLocale = false, bool isGlueEdit = false)
        {
            bool isPageNotExist;
            bool isAccessDenied;
            bool isSiteNotExist;
            File pageFile;
            File translationPageFile;
            var pageLocale = "";
            var pageCulture = GetLanguageCulture(lcid);
            if (pageCulture != null)
                pageLocale = pageCulture.Name.ToLower();

            RetrieveQuickPageInfoFromSP(context, pageRelativeUrl, out pageFile, out isPageNotExist, out isSiteNotExist, out isAccessDenied, isPhysicalPage, isGlueEdit);
            if (isPageNotExist || isSiteNotExist || isAccessDenied)
            {
                return new PageInfo { IsSiteNotExist = isSiteNotExist, IsPageNotExist = isPageNotExist, IsAccessDenied = isAccessDenied, Context = context };
            }

            //20170503 Duong - Begin : fix draft page open quick edit should not in 'create translate page', (page is not published anytime)
            if (!forceGetSelectLocale && pageFile.ListItemAllFields.FieldValues.ContainsKey(Core.Constants.CustomFields.OmniaPageLanguage))
            {
                var pageLanguageLocale = pageFile.ListItemAllFields[Core.Constants.CustomFields.OmniaPageLanguage] != null ? (string)pageFile.ListItemAllFields[Core.Constants.CustomFields.OmniaPageLanguage] : "";
                if (string.IsNullOrEmpty(pageLanguageLocale))
                {
                    var languages = _navigationService.GetNavigationLanguages(navigationSourceUrl);
                    var defaultLanguage = languages.FirstOrDefault(language => language.IsDefault);
                    lcid = defaultLanguage != null ? defaultLanguage.LCID : lcid;
                }
            }
            //20170503 Duong - End

            var pageInfo = BindQuickPageInfo(context, pageFile, pageRelativeUrl, lcid, isPhysicalPage, isGlueEdit);
            pageInfo.Context = context;
            pageInfo.TranslationPages = new List<PageInfo>();
            if (pageInfo.PageSourceLanguage != null)
            {
                var pageLanguage = GetLanguageCulture(pageInfo.PageSourceLanguage.Locale);
                RetrieveQuickPageInfoFromSP(context, pageInfo.PageSourceLanguage.PageUrl, out translationPageFile, out isPageNotExist, out isSiteNotExist, out isAccessDenied, isPhysicalPage, isGlueEdit);
                var translationPage = BindQuickPageInfo(context, translationPageFile, pageInfo.PageSourceLanguage.Locale, pageLanguage.LCID, isPhysicalPage, isGlueEdit);
                pageInfo.TranslationPages.Add(translationPage);
            }
            if (pageInfo.PageTargetLanguage != null)
            {
                foreach (var pageItem in pageInfo.PageTargetLanguage)
                {
                    var pageLanguage = GetLanguageCulture(pageItem.Locale);
                    RetrieveQuickPageInfoFromSP(context, pageItem.PageUrl, out translationPageFile, out isPageNotExist, out isSiteNotExist, out isAccessDenied, isPhysicalPage, isGlueEdit);
                    PageInfo translationPage = null;
                    if (!isPageNotExist)
                    {
                        translationPage = BindQuickPageInfo(context, translationPageFile, pageItem.PageUrl, pageLanguage.LCID, isPhysicalPage, isGlueEdit);
                        pageInfo.TranslationPages.Add(translationPage);
                    }
                }
            }
            if (!string.IsNullOrEmpty(pageLocale) && pageInfo.PageLanguageLocale.ToLower() == pageLocale && forceGetSelectLocale)
                return pageInfo;

            if ((!string.IsNullOrEmpty(pageLocale) && pageInfo.PageLanguageLocale.ToLower() != pageLocale) || pageInfo.PageSourceLanguage != null)
            {
                if (pageInfo.PageTargetLanguage != null && pageInfo.PageTargetLanguage.Any())
                {
                    var page = pageInfo.PageTargetLanguage.Where(p => p.Locale != null && p.Locale.ToLower() == pageLocale).FirstOrDefault();
                    if (page != null)
                    {
                        RetrieveQuickPageInfoFromSP(context, page.PageUrl, out pageFile, out isPageNotExist, out isSiteNotExist, out isAccessDenied, isPhysicalPage, isGlueEdit);
                        if (isPageNotExist || isSiteNotExist || isAccessDenied)
                        {
                            return new PageInfo { IsSiteNotExist = isSiteNotExist, IsPageNotExist = isPageNotExist, IsAccessDenied = isAccessDenied, Context = context };
                        }

                        var locales = pageInfo.PageLocales;
                        var pageTargets = pageInfo.PageTargetLanguage;
                        var defaultPageId = pageInfo.PageItemId;
                        pageInfo = BindQuickPageInfo(context, pageFile, page.PageUrl, lcid, isPhysicalPage, isGlueEdit);
                        pageInfo.PageLocales = locales;
                        pageInfo.PageTargetLanguage = pageTargets;
                        pageInfo.DefaultPageItemId = defaultPageId;
                    }
                }
                // physical page
                else if (pageInfo.PageSourceLanguage != null)
                {
                    var page = pageInfo.PageSourceLanguage;
                    if (page != null)
                    {
                        RetrieveQuickPageInfoFromSP(context, page.PageUrl, out pageFile, out isPageNotExist, out isSiteNotExist, out isAccessDenied, isPhysicalPage, isGlueEdit);
                        if (isPageNotExist || isSiteNotExist || isAccessDenied)
                        {
                            return new PageInfo { IsSiteNotExist = isSiteNotExist, IsPageNotExist = isPageNotExist, IsAccessDenied = isAccessDenied, Context = context };
                        }

                        var pageDefaultInfo = BindQuickPageInfo(context, pageFile, page.PageUrl, lcid, isPhysicalPage, isGlueEdit);

                        pageInfo.PageLocales = pageDefaultInfo.PageLocales;
                        pageInfo.PageTargetLanguage = pageDefaultInfo.PageTargetLanguage;
                        pageInfo.DefaultPageItemId = pageDefaultInfo.PageItemId;
                    }
                }
            }

            return pageInfo;
        }

        private PageInfo BindQuickPageInfo(ExtendedClientContext context, File pageFile, string pageUrl, int lcid, bool isPhysicalPage = false, bool isGlueEdit = false)
        {
            string pageLanguage = Core.Constants.CustomFields.OmniaPageLanguage;
            string pageSourceLanguage = Core.Constants.CustomFields.OmniaPageSourceLanguage;
            string pageTargetLanguage = Core.Constants.CustomFields.OmniaPageTargetLanguage;

            PageInfo pageInfo = new PageInfo();
            pageInfo.Title = (string)pageFile.ListItemAllFields[Foundation.Core.Constants.SharePoint.Fields.Title];
            pageInfo.CurrentPageItemId = (int)pageFile.ListItemAllFields[Foundation.Core.Constants.SharePoint.Fields.ID];
            pageInfo.Content = (string)pageFile.ListItemAllFields[Foundation.Core.Constants.SharePoint.Fields.PublishingPageContent];
            pageInfo.Summary = (string)pageFile.ListItemAllFields[Foundation.Core.Constants.SharePoint.Fields.Comments];
            pageInfo.Image = (string)pageFile.ListItemAllFields[Foundation.Core.Constants.SharePoint.Fields.PublishingPageImage];
            pageInfo.PageUrl = pageUrl;
            pageInfo.PhysicalPageUniqueId = (Guid)pageFile.ListItemAllFields[Foundation.Core.Constants.SharePoint.Fields.UniqueId];
            pageInfo.ContentTypeId = TryToGetContentTypeValue(pageFile);
            pageInfo.PublishingContact = (FieldUserValue)pageFile.ListItemAllFields[Foundation.Core.Constants.SharePoint.Fields.PublishingContact];
            pageInfo.PublishingContactName = TryToGetUserFieldEmail(context, pageFile, Foundation.Core.Constants.SharePoint.Fields.PublishingContact);
            pageInfo.ModifiedAt = pageFile.TimeLastModified;
            pageInfo.ModifiedBy = TryToGetUserFieldEmail(context, pageFile, "Editor");
            pageInfo.CreatedAt = pageFile.TimeCreated;
            pageInfo.CreatedBy = TryToGetUserFieldEmail(context, pageFile, "Author");
            pageInfo.ListId = pageFile.ListId;
            pageInfo.Context = context;
            SetPageVersionInformation(pageInfo, pageFile);
            if (pageFile.ListItemAllFields[Core.Constants.SharePoint.Field.ApprovalStatus] != null)
            {
                pageInfo.ApprovalStatus = (ApprovalStatus)Enum.ToObject(typeof(ApprovalStatus), (int)pageFile.ListItemAllFields[Core.Constants.SharePoint.Field.ApprovalStatus]);
            }

            TimeZoneInfo webTimezoneInfo = null;
            webTimezoneInfo = SharePointUtils.GetWebTimezoneInfo(context.Web);

            var currentVersion = pageFile.MajorVersion;
            if (pageInfo.FileMajorVersions == null)
                pageInfo.FileMajorVersions = new List<FileMajorVersion>();
            var version = pageInfo.FileMajorVersions.Where(v => v.VersionLabel == currentVersion).FirstOrDefault();
            if (version == null)
            {
                var dateValue = DateTime.Parse(CommonUtils.GetStringValue(pageFile.ListItemAllFields[Foundation.Core.Constants.SharePoint.Fields.Modified]));
                pageInfo.FileMajorVersions.Add(new FileMajorVersion
                {
                    VersionLabel = currentVersion,
                    CreatedAt = TimeZoneInfo.ConvertTime(dateValue, webTimezoneInfo, TimeZoneInfo.Utc)
                });
            }
            if (pageFile.ListItemAllFields.FieldValues.ContainsKey(Core.Constants.CustomFields.OmniaCustomDataField))
            {
                if (pageFile.ListItemAllFields[Core.Constants.CustomFields.OmniaCustomDataField] != null)
                    pageInfo.HasCustomDataField = true;
                else
                    pageInfo.HasCustomDataField = false;
            }
            pageInfo.CustomData = pageInfo.HasCustomDataField ? (string)pageFile.ListItemAllFields[Core.Constants.CustomFields.OmniaCustomDataField] : null;

            if (pageFile.ListItemAllFields.FieldValues.ContainsKey(Core.Constants.CustomFields.OmniaDraftNavigationTermField))
            {
                pageInfo.DraftNavigationTerm = (string)pageFile.ListItemAllFields[Core.Constants.CustomFields.OmniaDraftNavigationTermField];
            }

            if (pageFile.ListItemAllFields[Foundation.Core.Constants.SharePoint.Fields.PublishingPageLayout] != null)
                pageInfo.PageLayoutUrl = (pageFile.ListItemAllFields[Foundation.Core.Constants.SharePoint.Fields.PublishingPageLayout] as FieldUrlValue).Url;

            pageInfo.PageItemId = (int)pageFile.ListItemAllFields[Foundation.Core.Constants.SharePoint.Fields.ID];
            pageInfo.PageListId = pageFile.ListItemAllFields.ParentList.Id.ToString();

            if (pageFile.ListItemAllFields.FieldValues.ContainsKey(pageLanguage))
            {
                //string pageSource = (string)pageFile.ListItemAllFields[pageSourceLanguage] != null ? JsonConvert.SerializeObject(pageFile.ListItemAllFields[pageSourceLanguage]) : "";
                var pageSource = pageFile.ListItemAllFields[pageSourceLanguage] != null ? JsonConvert.DeserializeObject<PageLanguage>((string)pageFile.ListItemAllFields[pageSourceLanguage]) : null;
                var pageTarget = GetPageTarget(pageFile, pageTargetLanguage);
                var pageLanguageLocale = pageFile.ListItemAllFields[pageLanguage] != null ? (string)pageFile.ListItemAllFields[pageLanguage] : "";
                if (string.IsNullOrEmpty(pageLanguageLocale))
                {
                    var pageCulture = GetLanguageCulture(lcid);
                    if (pageCulture != null)
                        pageInfo.PageLanguageLocale = pageCulture.Name.ToLower();
                }
                else
                    pageInfo.PageLanguageLocale = pageLanguageLocale;
                pageInfo.PageSourceLanguage = pageSource;
                pageInfo.PageTargetLanguage = pageTarget;
                if (pageSource != null)
                    pageInfo.PageLocales = GetPageLocales(pageInfo.PageLanguageLocale, pageTarget, false);
                else
                    pageInfo.PageLocales = GetPageLocales(pageInfo.PageLanguageLocale, pageTarget, true);
            }

            if (pageFile.ListItemAllFields.ParentList != null)
                pageInfo.ListTitle = pageFile.ListItemAllFields.ParentList.Title;
            pageInfo.SiteTitle = context.Web.Title;
            pageInfo.SiteUrl = context.Web.Url;
            pageInfo.SiteRelativeUrl = context.Web.ServerRelativeUrl;

            pageInfo.SiteCollectionTitle = context.Site.RootWeb.Title;
            pageInfo.SiteCollectionUrl = context.Site.RootWeb.Url;
            pageInfo.SiteCollectionRelativeUrl = context.Site.RootWeb.ServerRelativeUrl;

            pageInfo.Properties = new Dictionary<string, object>();
            pageInfo.Properties.Add(Core.Constants.CustomFields.OmniaGlueData, pageFile.ListItemAllFields[Core.Constants.CustomFields.OmniaGlueData]);

            return pageInfo;
        }

        public PageInfoPropertiesSettings GetQuickPagePropertiesSettings(string navigationSourceUrl, string pageUrl, Guid pageListId)
        {
            try
            {
                PageInfoPropertiesSettings result = null;
                Elevate(navigationSourceUrl, Tenant, (appCtx) =>
                {
                    List pageList = appCtx.Web.Lists.GetById(pageListId);
                    LoadPageFields(appCtx, pageList.Fields, true, false);

                    File pageFile = appCtx.Web.GetFileByServerRelativeUrl(pageUrl);
                    appCtx.Load(pageFile.ListItemAllFields);

                    appCtx.ExecuteQuery();

                    PageInfo pageInfo = new PageInfo() { Context = appCtx };
                    ApplySettingsToPageInfo(navigationSourceUrl, appCtx, appCtx.Web, pageFile, pageInfo, pageList.Fields.ToList(), inViewMode: false);
                    //TargetingDefinition targetingDefinition = GetTargetingDefinitionIfNeed(pageInfo.PagePropertiesSetting);

                    result = new PageInfoPropertiesSettings
                    {
                        PagePropertiesSetting = pageInfo.PagePropertiesSetting,
                        Properties = pageInfo.Properties,
                        BooleanFields = pageInfo.BooleanFields,
                        //TargetingDefinition = targetingDefinition
                    };
                });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SettingsAdministration GetPagePropertiesSettings(string siteUrl)
        {
            SettingsAdministration settingsInformation = null;
            Elevate(siteUrl, Tenant, (ctx) =>
            {
                try
                {
                    settingsInformation = new SettingsAdministration();
                    var settings = GetContentManagementSettings(ctx);
                    if (settings == null || settings.IsInheritParentSetting)
                    {
                        string parentUrl;
                        settings = GetParentBasedConfiguration(ctx, out parentUrl);
                        if (settings != null)
                        {
                            settingsInformation.IsInheritParentSetting = true;
                            settingsInformation.ParentUrl = parentUrl;
                        }
                    }

                    List<PagePropertySettings> propertyConfigurations = settings != null ? settings.AvailablePageProperties : new List<PagePropertySettings>();

                    SetPropertiesSettingValues(ctx, settingsInformation, propertyConfigurations);
                }
                catch (Exception ex)
                {
                    throw;
                }
            });

            return settingsInformation;
        }

        private FieldCollection LoadPageFields(ExtendedClientContext ctx, FieldCollection pageFields, bool includeReadOnly, bool executeQuery, bool isLoadAllField = false)
        {
            if (isLoadAllField)
            {
                ctx.Load(pageFields);
                ctx.ExecuteQuery();
            }
            else
            {
                if (!includeReadOnly)
                {
                    ctx.Load(pageFields, fields => fields
                        .Include(
                            field => field.InternalName,
                            field => field.Title,
                                field => field.TypeAsString,
                                field => field.ReadOnlyField)
                        .Where(field =>
                            !field.Hidden && !field.ReadOnlyField
                            ));
                }
                else
                {
                    ctx.Load(pageFields, fields => fields
                       .Include(
                           field => field.InternalName,
                           field => field.Title,
                           field => field.TypeAsString,
                           field => field.ReadOnlyField)
                       .Where(field =>
                           !field.Hidden
                           ));
                }
                if (executeQuery)
                {
                    ctx.ExecuteQuery();
                }
            }
            return pageFields;
        }

        private void ApplySettingsToPageInfo(string navigationSourceUrl, ExtendedClientContext ctx, Web web, File pageFile, PageInfo pageInfo, List<Field> pageFields, bool inViewMode)
        {
            if (contentManagementSettings.ContainsKey(navigationSourceUrl) == false)
            {
                contentManagementSettings[navigationSourceUrl] = GetSettingsFromConfiguration(ctx);
            }

            ContentManagementSettings settings = contentManagementSettings[navigationSourceUrl];
            if (settings == null)
            {
                settings = new ContentManagementSettings();
            }

            // Append with properties user define in appsettings.json to query them as well
            if (AppUtils.MigrationSettings.CustomPropertiesMappings.Any())
            {


                foreach (var prop in AppUtils.MigrationSettings.CustomPropertiesMappings)
                {
                    if (settings.AvailablePageProperties.Any(i => i.InternalName == prop.Key) == false)
                    {
                        settings.AvailablePageProperties.Add(new PagePropertySettings
                        {
                            InternalName = prop.Key,
                            IsMigrationG2Property = true
                        });
                    }
                }
            }

            // Append with targeting properties
            var targetingProperties = pageFields.Where(x => x.InternalName.StartsWith("otp"));
            foreach (var targetingProp in targetingProperties)
            {
                if (settings.AvailablePageProperties.Any(i => i.InternalName == targetingProp.InternalName) == false)
                {
                    settings.AvailablePageProperties.Add(new PagePropertySettings
                    {
                        InternalName = targetingProp.InternalName,
                        IsMigrationG2Property = false
                    });
                }
            }

            pageInfo.PagePropertiesSetting = EnsurePagePropertiesSettings(ctx, settings.AvailablePageProperties, pageFields, inViewMode);
            foreach (var pageProperty in pageInfo.PagePropertiesSetting)
            {
                if (pageFile.ListItemAllFields.FieldValues.ContainsKey(pageProperty.InternalName))
                    pageInfo.Properties.Add(pageProperty.InternalName, pageFile.ListItemAllFields[pageProperty.InternalName]);

                //append boolean fields
                if (pageProperty.IsShowInViewMode && pageProperty.TypeAsString.ToLower().Equals("boolean"))
                {
                    if (pageFile.ListItemAllFields[pageProperty.InternalName] != null && (bool)pageFile.ListItemAllFields[pageProperty.InternalName])
                    {
                        pageInfo.BooleanFields.Add(pageProperty.DisplayName);
                    }
                }
            }


            if (!pageInfo.Properties.ContainsKey(Core.Constants.CustomFields.OmniaGlueData))
            {
                if (pageFile.ListItemAllFields.FieldValues.ContainsKey(Core.Constants.CustomFields.OmniaGlueData))
                {
                    pageInfo.Properties.Add(Core.Constants.CustomFields.OmniaGlueData, pageFile.ListItemAllFields[Core.Constants.CustomFields.OmniaGlueData]);
                }
            }
        }

        private List<PageProperty> EnsurePagePropertiesSettings(ExtendedClientContext ctx, IEnumerable<PagePropertySettings> pagePropertySettingsList, List<Field> pageFields, bool inViewMode)
        {
            var filteredPageFields = pageFields.Where(field => pagePropertySettingsList.Any(p => p.InternalName == field.InternalName)).ToList();

            var propertyDictionary = GetPagePropertyFieldsDictionary(ctx, filteredPageFields, inViewMode ? false : true,
                Foundation.Core.Fields.BooleanFieldAttribute.TypeAsStringValue,
                Foundation.Core.Fields.ManagedMetadataFieldAttribute.TypeMultiAsStringValue,
                Foundation.Core.Fields.ManagedMetadataFieldAttribute.TypeSingleAsStringValue,
                Foundation.Core.Fields.DateTimeFieldAttribute.TypeAsStringValue,
                Foundation.Core.Fields.UserFieldAttribute.TypeMultiAsStringValue,
                Foundation.Core.Fields.UserFieldAttribute.TypeSingleAsStringValue,
                Foundation.Core.Fields.TextFieldAttribute.TypeAsStringValue,
                Foundation.Core.Fields.NumberFieldAttribute.TypeAsStringValue,
                Foundation.Core.Fields.NoteFieldAttribute.TypeAsStringValue);

            List<PageProperty> pageProperties = new List<PageProperty>();
            foreach (var prop in pagePropertySettingsList)
            {
                if (propertyDictionary.ContainsKey(prop.InternalName))
                {
                    var pageProperty = propertyDictionary[prop.InternalName];
                    pageProperty.IsSelected = true;
                    pageProperty.IsShowInEditMode = prop.IsShowInEditMode;
                    pageProperty.IsShowInViewMode = prop.IsShowInViewMode;
                    pageProperty.IsShowInShowMore = prop.IsShowInShowMore;
                    pageProperty.IsShared = prop.IsShared;
                    pageProperty.IsShowLabel = prop.IsShowLabel;
                    pageProperty.Required = prop.Required;
                    pageProperty.GroupPermission = prop.GroupPermission;
                    pageProperty.DisplayType = prop.DisplayType;
                    if (pageProperty.InternalName.StartsWith(Core.Constants.Targeting.PageTargetingFieldPrefix))
                    {
                        pageProperty.IsTargetingField = true;
                    }

                    pageProperty.IsMigrationG2Property = prop.IsMigrationG2Property;

                    pageProperties.Add(pageProperty);
                }
            }
            return pageProperties;
        }

        private Dictionary<string, PageProperty> GetPagePropertyFieldsDictionary(ExtendedClientContext ctx, IList<Field> pageFields, bool loadAdditionalInfo, params string[] fieldTypes)
        {
            var propertyDictionary = new Dictionary<string, PageProperty>();
            var taxonomyFieldList = new List<TaxonomyField>();
            var datetimeFieldList = new List<FieldDateTime>();
            var userFieldList = new List<FieldUser>();

            foreach (var field in pageFields)
            {
                if (fieldTypes.Contains(field.TypeAsString) || fieldTypes.Length == 0)
                {
                    if (!propertyDictionary.ContainsKey(field.InternalName))
                    {
                        PageProperty pageProperty = new PageProperty()
                        {
                            DisplayName = field.Title,
                            InternalName = field.InternalName,
                            TypeAsString = field.TypeAsString,
                            ReadOnlyField = field.ReadOnlyField
                        };

                        if (field.TypeAsString == Intranet.Core.Constants.SharePoint.SharepointType.DateTime)
                        {
                            Microsoft.SharePoint.Client.FieldDateTime datetimeField = (Microsoft.SharePoint.Client.FieldDateTime)field.TypedObject;

                            ctx.Load(datetimeField, t => t.DisplayFormat);
                            datetimeFieldList.Add(datetimeField);
                        }

                        if (loadAdditionalInfo)
                        {
                            if (pageProperty.TypeAsString == Foundation.Core.Fields.ManagedMetadataFieldAttribute.TypeMultiAsStringValue ||
                                pageProperty.TypeAsString == Foundation.Core.Fields.ManagedMetadataFieldAttribute.TypeSingleAsStringValue)
                            {
                                var taxonomyField = (TaxonomyField)field;
                                ctx.Load(taxonomyField, f => f.TermSetId, f => f.AllowMultipleValues, f => f.CreateValuesInEditForm, f => f.Open);
                                taxonomyFieldList.Add(taxonomyField);
                            }

                            if (pageProperty.TypeAsString == Foundation.Core.Fields.UserFieldAttribute.TypeMultiAsStringValue ||
                                pageProperty.TypeAsString == Foundation.Core.Fields.UserFieldAttribute.TypeSingleAsStringValue)
                            {
                                var userField = (FieldUser)field;
                                ctx.Load(userField, f => f.AllowMultipleValues);
                                userFieldList.Add(userField);
                            }
                        }

                        propertyDictionary.Add(pageProperty.InternalName, pageProperty);
                    }
                }
            }

            if (taxonomyFieldList.Count > 0 || userFieldList.Count > 0 || datetimeFieldList.Count > 0)
            {
                ctx.ExecuteQuery();
                foreach (var taxonomyField in taxonomyFieldList)
                {
                    if (propertyDictionary.ContainsKey(taxonomyField.InternalName))
                    {
                        var pageProperty = propertyDictionary[taxonomyField.InternalName];
                        pageProperty.AdditionalInfo.Add(Foundation.Core.Constants.SharePoint.PagePropertyInfoKey.TermSetId, taxonomyField.TermSetId.ToString());
                        pageProperty.AdditionalInfo.Add(Foundation.Core.Constants.SharePoint.PagePropertyInfoKey.AllowMultipleValues, taxonomyField.AllowMultipleValues.ToString());
                        pageProperty.AdditionalInfo.Add(Foundation.Core.Constants.SharePoint.PagePropertyInfoKey.CreateValuesInEditForm, taxonomyField.CreateValuesInEditForm.ToString());
                        pageProperty.AdditionalInfo.Add(Foundation.Core.Constants.SharePoint.PagePropertyInfoKey.Open, taxonomyField.Open.ToString());
                    }
                }

                foreach (var userField in userFieldList)
                {
                    if (propertyDictionary.ContainsKey(userField.InternalName))
                    {
                        var pageProperty = propertyDictionary[userField.InternalName];
                        pageProperty.AdditionalInfo.Add(Foundation.Core.Constants.SharePoint.PagePropertyInfoKey.AllowMultipleValues, userField.AllowMultipleValues.ToString());
                    }
                }

                foreach (var datetimeField in datetimeFieldList)
                {
                    if (propertyDictionary.ContainsKey(datetimeField.InternalName))
                    {
                        var pageProperty = propertyDictionary[datetimeField.InternalName];
                        pageProperty.DisplayFormat = datetimeField.DisplayFormat.ToString();
                    }
                }
            }
            return propertyDictionary;
        }

        private Dictionary<string, PageProperty> GetPagePropertyFieldsDictionary(IList<Field> pageFields, params string[] fieldTypes)
        {
            var propertyDictionary = new Dictionary<string, PageProperty>();
            foreach (var field in pageFields)
            {
                if (fieldTypes.Contains(field.TypeAsString) || fieldTypes.Length == 0)
                {
                    if (!propertyDictionary.ContainsKey(field.InternalName))
                    {
                        PageProperty pageProperty = new PageProperty()
                        {
                            DisplayName = field.Title,
                            InternalName = field.InternalName,
                            TypeAsString = field.TypeAsString,
                            ReadOnlyField = field.ReadOnlyField
                        };
                        propertyDictionary.Add(pageProperty.InternalName, pageProperty);
                    }
                }
            }
            return propertyDictionary;
        }

        private ContentManagementSettings GetSettingsFromConfiguration(ExtendedClientContext ctx)
        {
            ContentManagementSettings settings = GetContentManagementSettings(ctx);
            if (settings == null || settings.IsInheritParentSetting)
            {
                string parentUrl;
                settings = GetParentBasedConfiguration(ctx, out parentUrl);
                if (settings != null)
                {
                    settings.IsInheritParentSetting = true;
                }
            }
            return settings;
        }

        private ContentManagementSettings GetParentBasedConfiguration(ExtendedClientContext ctx, out string parentUrl)
        {
            ContentManagementSettings settings = null;
            ctx.Load(ctx.Site, s => s.Url);
            ctx.ExecuteQuery();
            parentUrl = string.Empty;

            var configuration = InitConfiguration(ctx);
            var parentWebConfigurations = _configurationService.GetParentSiteConfigurations(configuration.Name, configuration.Region);
            try
            {
                ContentManagementSettings parentSettings;
                foreach (Foundation.Models.Configurations.Configuration item in parentWebConfigurations)
                {
                    try
                    {
                        parentSettings = JsonConvert.DeserializeObject<ContentManagementSettings>(item.Value);
                        if (!parentSettings.IsInheritParentSetting && item.Name.StartsWith(ctx.Site.Url.ToLower())) //StartsWith here to exclude inherit from root site (prevent inherit cross site collection)
                        {
                            settings = parentSettings;
                            parentUrl = item.Name;
                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }

            }
            catch (Exception ex)
            {
                throw;
            }
            return settings;
        }

        private ContentManagementSettings GetContentManagementSettings(ClientContext ctx)
        {
            ContentManagementSettings settings = null;
            var configuration = InitConfiguration(ctx);
            var configurationData = _configurationService.GetConfiguration(configuration.Name, configuration.Region);
            try
            {
                if (configurationData != null && configurationData.Value != null)
                {
                    settings = JsonConvert.DeserializeObject<ContentManagementSettings>(configurationData.Value);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return settings;
        }

        private void SetPropertiesSettingValues(ExtendedClientContext ctx, SettingsAdministration settings, IEnumerable<PagePropertySettings> configurableProperties)
        {
            var propertyDictionary = GetPropertyDictionaryForPageList(ctx, true);
            foreach (var selectedProp in configurableProperties)
            {
                if (propertyDictionary.ContainsKey(selectedProp.InternalName))
                {
                    var property = propertyDictionary[selectedProp.InternalName];
                    property.IsSelected = true;
                    property.IsShowInEditMode = selectedProp.IsShowInEditMode;
                    property.IsShowInViewMode = selectedProp.IsShowInViewMode;
                    property.IsShowInShowMore = selectedProp.IsShowInShowMore;
                    property.IsShared = selectedProp.IsShared;
                    property.IsShowLabel = selectedProp.IsShowLabel;
                    property.Required = selectedProp.Required;
                    property.GroupPermission = selectedProp.GroupPermission;
                    settings.SelectedPageProperties.Add(property);
                }
            }
        }

        private Dictionary<string, PageProperty> GetPropertyDictionaryForPageList(ExtendedClientContext ctx, bool includeReadOnly)
        {
            //Get page list id from cache
            Guid pageListId = _publishingService.GetPageListId(ctx, ctx.Url);
            var pageList = ctx.Web.Lists.GetById(pageListId);

            IList<Field> pageFieldsList = null;

            var pageFields = LoadPageFields(ctx, pageList.Fields, includeReadOnly, true);
            pageFieldsList = pageFields.ToList();
            return GetPagePropertyFieldsDictionary(pageFieldsList,
                Foundation.Core.Fields.BooleanFieldAttribute.TypeAsStringValue,
                Foundation.Core.Fields.ManagedMetadataFieldAttribute.TypeMultiAsStringValue,
                Foundation.Core.Fields.ManagedMetadataFieldAttribute.TypeSingleAsStringValue,
                Foundation.Core.Fields.DateTimeFieldAttribute.TypeAsStringValue,
                Foundation.Core.Fields.UserFieldAttribute.TypeMultiAsStringValue,
                Foundation.Core.Fields.UserFieldAttribute.TypeSingleAsStringValue,
                Foundation.Core.Fields.TextFieldAttribute.TypeAsStringValue,
                Foundation.Core.Fields.NoteFieldAttribute.TypeAsStringValue,
                Foundation.Core.Fields.NumberFieldAttribute.TypeAsStringValue);
        }

        private Foundation.Models.Configurations.Configuration InitConfiguration(ClientContext ctx)
        {
            return new Foundation.Models.Configurations.Configuration()
            {
                Name = CommonUtils.RemoveTrailingSlash(ctx.Url),
                Region = new Guid("DB5A14E4-5552-45E6-B64A-09B2BD42CD3F").ToString(),
                Value = string.Empty
            };
        }

        private void RetrieveQuickPageInfoFromSP(ExtendedClientContext context, string pageUrl, out File pageFile, out bool isPageNotExist, out bool isSiteNotExist, out bool isAccessDenied, bool isPhysicalPage = false, bool isGlueEdit = false)
        {
            try
            {
                isSiteNotExist = false;
                isPageNotExist = false;
                isAccessDenied = false;
                
                pageFile = context.Web.GetFileByServerRelativeUrl(pageUrl);
                //without loading the while object, load only Exists property will ensure no error thrown and to check if page exist
                context.Load(pageFile, file => file.Exists);
                context.ExecuteQuery();
                if (!pageFile.Exists)
                {
                    isSiteNotExist = false;
                    isPageNotExist = true;
                    isAccessDenied = false;
                    pageFile = null;
                    return;
                }

                context.Load(pageFile.ListItemAllFields);
                context.Load(pageFile.ListItemAllFields.ParentList, list => list.Id, list => list.Title, list => list.Created, list => list.LastItemModifiedDate);

                context.Load(pageFile, f => f.MajorVersion, f => f.MinorVersion, f => f.UIVersionLabel);

                context.Load(pageFile, f => f.TimeCreated, f => f.TimeLastModified);

                if (isGlueEdit)
                {
                    //context.Load(pageFile, f => f.MajorVersion, f => f.MinorVersion, f => f.UIVersionLabel);
                    context.Load(pageFile.Versions);
                    context.Load(pageFile.CheckedOutByUser);
                    context.Load(pageFile, p => p.CheckOutType);
                }
                context.Load(pageFile, f => f.ListId);
                context.Load(context.Web, web => web.Title, web => web.Url, web => web.ServerRelativeUrl, web => web.IsMultilingual,
                            web => web.SupportedUILanguageIds, web => web.Language, web => web.RegionalSettings.TimeZone);
                context.Load(context.Site.RootWeb, rootWeb => rootWeb.Title, rootWeb => rootWeb.Url, rootWeb => rootWeb.ServerRelativeUrl);
                //20211222 - Diem : get only checkout page
                context.Load(pageFile, p => p.CheckOutType == CheckOutType.None);
                context.ExecuteQuery();
            }
            catch (Exception ex)
            {


                // handle site not exist
                if (ApiUtils.IsWebNotFoundException(ex))
                {
                    isSiteNotExist = true;
                    isPageNotExist = false;
                    isAccessDenied = false;
                    pageFile = null;
                }
                else // check page is not exist and authorize
                {
                    try
                    {
                        isSiteNotExist = false;
                        isPageNotExist = false;
                        isAccessDenied = false;
                        pageFile = context.Web.GetFileByServerRelativeUrl(pageUrl);
                        context.Load(pageFile);
                        context.ExecuteQuery();
                    }
                    catch (Microsoft.SharePoint.Client.ServerException ex2)
                    {
                        pageFile = null;
                        isPageNotExist = false;
                        isSiteNotExist = false;
                        isAccessDenied = false;
                        //this one does not get hit because SP doesn't return the same error type despite having the same problem
                        if (ex2.ServerErrorTypeName == "System.IO.FileNotFoundException")
                        {
                            isPageNotExist = true;
                        }
                        else if (ex2.ServerErrorTypeName == "System.UnauthorizedAccessException")
                        {
                            isAccessDenied = true;
                        }
                        else
                        {
                            throw ex;
                        }
                    }
                    //catch exception if the upper one failed
                    catch (Exception ex3)
                    {
                        pageFile = null;
                        isPageNotExist = true;
                        isSiteNotExist = false;
                        isAccessDenied = false;
                    }
                }
            }
        }

        private CultureInfo GetLanguageCulture(string locale)
        {
            try
            {
                return CultureInfo.GetCultureInfo(locale);
            }
            catch (Exception) { return null; }
        }

        private CultureInfo GetLanguageCulture(int lcid)
        {
            try
            {
                return CultureInfo.GetCultureInfo(lcid);
            }
            catch (Exception) { return null; }
        }

        private string TryToGetContentTypeValue(File pageFile)
        {
            if (pageFile.ListItemAllFields.FieldValues.ContainsKey(Foundation.Core.Constants.SharePoint.Fields.ContentTypeId))
            {
                var fieldValue = (pageFile.ListItemAllFields[Foundation.Core.Constants.SharePoint.Fields.ContentTypeId] as ContentTypeId);
                if (fieldValue != null)
                {
                    return fieldValue.StringValue;
                }
            }
            return string.Empty;
        }

        private string TryToGetUserFieldLookupValue(File pageFile, string fieldName)
        {
            if (pageFile.ListItemAllFields.FieldValues.ContainsKey(fieldName))
            {
                var userValue = (pageFile.ListItemAllFields[fieldName] as FieldUserValue);
                if (userValue != null)
                {
                    return userValue.LookupValue;
                }
            }
            return string.Empty;
        }

        private string TryToGetUserFieldEmail(ExtendedClientContext clientContext, File pageFile, string fieldName)
        {
            FieldUserValue userValue = new FieldUserValue();
            try
            {
                if (pageFile.ListItemAllFields.FieldValues.ContainsKey(fieldName))
                {
                    userValue = (pageFile.ListItemAllFields[fieldName] as FieldUserValue);

                    if (userValue != null)
                    {
                        var user = clientContext.Web.GetUserById(userValue.LookupId);
                        clientContext.Load(user, x => x.LoginName);
                        clientContext.ExecuteQuery();
                        return user.LoginName.Substring(user.LoginName.LastIndexOf('|') + 1);
                        //return userValue.Email;
                    }
                }
                return string.Empty;
            }
            catch(Exception)
            {
                return userValue.Email;
            }
        }

        private static List<PageLanguage> GetPageTarget(File pageFile, string pageTargetLanguage)
        {
            try
            {
                return pageFile.ListItemAllFields[pageTargetLanguage] != null ? JsonConvert.DeserializeObject<List<PageLanguage>>((string)pageFile.ListItemAllFields[pageTargetLanguage]) : null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private List<LanguageLocale> GetPageLocales(string pageLanguageLocale, List<PageLanguage> pageTarget, bool isSourcePage)
        {
            List<LanguageLocale> lstLocales = new List<LanguageLocale>();
            var sourceCultureInfo = GetLanguageCulture(pageLanguageLocale);
            var regionInfo = GetRegionInfo(pageLanguageLocale);
            var locale = new LanguageLocale
            {
                Locale = pageLanguageLocale,
                IsSourceLang = isSourcePage,
                LanguageName = sourceCultureInfo != null ? sourceCultureInfo.EnglishName : "",
                LCID = sourceCultureInfo != null ? sourceCultureInfo.LCID : 0,
                FlagName = regionInfo != null ? regionInfo.TwoLetterISORegionName.ToLower() : "",
            };
            lstLocales.Add(locale);

            if (pageTarget != null)
            {
                foreach (var item in pageTarget)
                {
                    var targetCultureInfo = GetLanguageCulture(item.Locale);
                    var targetRegionInfo = GetRegionInfo(item.Locale);
                    var targetLocale = new LanguageLocale
                    {
                        Locale = item.Locale,
                        IsSourceLang = false,
                        LanguageName = targetCultureInfo != null ? targetCultureInfo.EnglishName : "",
                        LCID = targetCultureInfo != null ? targetCultureInfo.LCID : 0,
                        FlagName = targetRegionInfo != null ? targetRegionInfo.TwoLetterISORegionName.ToLower() : ""
                    };
                    lstLocales.Add(targetLocale);
                }
            }
            return lstLocales;
        }

        private RegionInfo GetRegionInfo(string locale)
        {
            try
            {
                return new RegionInfo(locale);
            }
            catch (Exception) { return null; }
        }

        private int GetLCID(int lcid, PageInfo result)
        {
            var pageLocale = "";
            var pageCulture = GetLanguageCulture(lcid);
            if (pageCulture != null)
                pageLocale = pageCulture.Name.ToLower();

            if ((result.PageLanguageLocale != null && result.PageLanguageLocale != "0" && result.PageLanguageLocale.ToLower() != pageLocale))
            {
                PageLanguage page = null;
                if (result.PageTargetLanguage != null && result.PageTargetLanguage.Any())
                    page = result.PageTargetLanguage.Where(p => p.Locale != null && p.Locale.ToLower() == pageLocale).FirstOrDefault();

                if (result.PageTargetLanguage == null || page == null)
                {
                    var culture = GetLanguageCulture(result.PageLanguageLocale);
                    if (culture != null)
                        return culture.LCID;
                }
            }
            return lcid;
        }

        private void SetPageVersionInformation(PageInfo pageInfo, File pageFile)
        {
            if (pageFile.IsPropertyAvailable("MajorVersion"))
                pageInfo.MajorVersion = pageFile.MajorVersion;

            if (pageFile.IsPropertyAvailable("MinorVersion"))
                pageInfo.MinorVersion = pageFile.MinorVersion;

            if (pageFile.IsPropertyAvailable("UIVersionLabel"))
                pageInfo.UIVersionLabel = pageFile.UIVersionLabel;
        }

        private Foundation.Models.Configurations.Configuration InitQuickPageSettingsConfiguration(ClientContext ctx, string navigationSourceUrl = null)
        {
            //Quick page settings has Site collection scope
            return new Foundation.Models.Configurations.Configuration()
            {
                Name = "quickpagesettings",
                Region = String.IsNullOrEmpty(navigationSourceUrl) ? CommonUtils.RemoveTrailingSlash(ctx.Url) : navigationSourceUrl,
                Value = string.Empty
            };
        }
    }
}
