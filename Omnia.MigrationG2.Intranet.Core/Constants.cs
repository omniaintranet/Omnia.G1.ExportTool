using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Intranet.Core
{
    public class Constants
    {
        /// <summary>
        /// ConnectionStrings
        /// </summary>
        public class ConnectionStrings
        {
            /// <summary>
            /// The Omnia Intranet connectionstring
            /// </summary>
            public const string Intranet = "Omnia.ConnectionStrings.Intranet";

            /// <summary>
            /// The Omnia Intranet dev connectionstring
            /// </summary>
            public const string PortalOnlineDev = "Data Source=.;Database=Intranet;Integrated Security=True;";

        }

        /// <summary>
        /// 
        /// </summary>
        public class SharePoint
        {
            /// <summary>
            /// The suffix calendar item URL
            /// </summary>
            public const string SuffixCalendarItemUrl = "/DispForm.aspx?ID=";

            public class UserProfileProperties
            {
                public const string Language = "SPS-MUILanguages";
                public const string TimeZone = "SPS-TimeZone";
                public const string WorkEmail = "WorkEmail";
            }

            public class Search
            {
                public const string SearchApiUrlForCheckManagedProperty = "{0}/_api/search/query?querytext='{1}'&selectproperties='{2}'&trimduplicates=false";
                public const string SearchAcceptHeader = "application/json;odata=verbose;charset=utf-8";
                public const string SearchApiWithRefinerUrl = "{0}/_api/search/query?querytext='{1}'&rowlimit={2}&startrow={3}&selectproperties='{4}'&sortlist='{5}'&trimduplicates=false&refiners='{6}'&refinementfilters='{7}'&uilanguage={8}&culture={9}";

                public const string SearchPropertyTokenStartString = "{Property.";
                public const string SearchPropertyTokenEndString = "}";

                public const string ManagedProperties_Title = "Title";
                public const string ManagedProperties_ListItemID = "ListItemID";
                public const string IsDocument = "IsDocument";
                public const string FileType = "FileType";
                public const string ContentType = "ContentType";
                public const string PreferredName = "PreferredName";
                public const string ManagedProperties_DefaultEncodingURL = "DefaultEncodingURL";
                public const string SPWebUrl = "SPWebUrl";
            }

            public static class SharepointType
            {
                public const string Text = "Text";
                public const string Boolean = "Boolean";
                public const string Integer = "Integer";
                public const string DateTime = "DateTime";
                public const string File = "File";
                public const string Note = "Note";
                public const string User = "User";
                public const string UserId = "UserId";
                public const string Number = "Number";
                public const string UserMulti = "UserMulti";
                public const string String = "String";
                public const string TaxonomyFieldType = "TaxonomyFieldType";
                public const string TaxonomyFieldTypeMulti = "TaxonomyFieldTypeMulti";

            }

            public static class Field
            {
                public const string HidePhysicalURLsFromSearch = "PublishingIsFurlPage";
                public const string ApprovalStatus = "_ModerationStatus";
            }

            public static class HiddenAppCatalogList
            {
                public static readonly Guid CommentLike = new Guid("4EF9BF95-3610-488F-911F-697245591C13");
                public static readonly Guid WebSummaryStatistics = new Guid("A40D9DD8-D827-420B-8AC0-4011B5C6BD65");
            }

            public static class HiddenNavigationSyncList
            {
                public static readonly Guid URL_ID = new Guid("7973E560-5AF5-4199-A49F-9C8BC54299E9");
                public static readonly string DESCRIPTION = "Contains navigation nodes in current site synced with Omnia Database";
            }
        }

        /// <summary>
        /// PageLayouts
        /// </summary>
        public class PageLayouts
        {
            public const string NewsStartPage = "OmniaNewsStartPage";
        }

        public class SearchConfiguration
        {
            public const string SearchResultTypeStatusCacheKey = "intranetsearchresulttypestatus_{0}";
            public const string SearchResultTypeForWebPageDisplayTemplate = "<SearchConfigurationSettings xmlns:i=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns=\"http://schemas.datacontract.org/2004/07/Microsoft.Office.Server.Search.Portability\"><SearchQueryConfigurationSettings><SearchQueryConfigurationSettings><BestBets xmlns:d4p1=\"http://www.microsoft.com/sharepoint/search/KnownTypes/2008/08\" /><DefaultSourceId>00000000-0000-0000-0000-000000000000</DefaultSourceId><DefaultSourceIdSet>true</DefaultSourceIdSet><DeployToParent>false</DeployToParent><DisableInheritanceOnImport>false</DisableInheritanceOnImport><QueryRuleGroups xmlns:d4p1=\"http://www.microsoft.com/sharepoint/search/KnownTypes/2008/08\" /><QueryRules xmlns:d4p1=\"http://www.microsoft.com/sharepoint/search/KnownTypes/2008/08\" /><ResultTypes xmlns:d4p1=\"http://schemas.datacontract.org/2004/07/Microsoft.Office.Server.Search.Administration\"><d4p1:ResultItemType><d4p1:BuiltIn>false</d4p1:BuiltIn><d4p1:DisplayProperties>Title,Path,Description,EditorOWSUSER,LastModifiedTime,CollapsingStatus,DocId,HitHighlightedSummary,HitHighlightedProperties,FileExtension,ViewsLifeTime,ParentLink,FileType,IsContainer,SecondaryFileExtension,DisplayAuthor</d4p1:DisplayProperties><d4p1:DisplayTemplateUrl>~sitecollection/_catalogs/masterpage/Display Templates/Search/Item_Default_WebPage.js</d4p1:DisplayTemplateUrl><d4p1:ID>88</d4p1:ID><d4p1:InternalID>c9cbb65a-2e2b-4e72-9af4-8c3f1d43b006</d4p1:InternalID><d4p1:IsDeleted>false</d4p1:IsDeleted><d4p1:LastModifiedDate>2017-08-02T05:54:41.73</d4p1:LastModifiedDate><d4p1:Name>OMI Web Page Result Type</d4p1:Name><d4p1:OptimizeForFrequentUse>false</d4p1:OptimizeForFrequentUse><d4p1:Owner><d4p1:DatabaseId>00000000-0000-0000-0000-000000000000</d4p1:DatabaseId><d4p1:DisableInheritance>false</d4p1:DisableInheritance><d4p1:SPFarmId>baa1b08f-cbca-4af8-95fe-67ba9526f9b6</d4p1:SPFarmId><d4p1:SPSiteId>2dc5d29c-ab0f-427e-bbb7-4ceee0f1d754</d4p1:SPSiteId><d4p1:SPSiteSubscriptionId>ffc28f99-7a35-450e-8a25-9a8b6a29c057</d4p1:SPSiteSubscriptionId><d4p1:SPWebId>00000000-0000-0000-0000-000000000000</d4p1:SPWebId></d4p1:Owner><d4p1:RulePriority>4</d4p1:RulePriority><d4p1:Rules><d4p1:PropertyRules><d4p1:PropertyRule><d4p1:PropertyName>ContentTypeId</d4p1:PropertyName><d4p1:PropertyOperator><d4p1:DescriptionLSID>ManageResultTypes_Conditions_StartsWith</d4p1:DescriptionLSID><d4p1:IsFunction>true</d4p1:IsFunction><d4p1:IsQuoted>true</d4p1:IsQuoted><d4p1:JoinedByOr>true</d4p1:JoinedByOr><d4p1:Name i:nil=\"true\" /><d4p1:NameLSID>ResultType_RuleOperator_StartsWith</d4p1:NameLSID><d4p1:Representation>StartsWith</d4p1:Representation></d4p1:PropertyOperator><d4p1:PropertyValues xmlns:d9p1=\"http://schemas.microsoft.com/2003/10/Serialization/Arrays\"><d9p1:string>0x010100C568DB52D9D0A14D9B2FDCC96666E9F2007948130EC3DB064584E219954237AF3900242457EFB8B24247815D688C526CD44D00FE768BAF03484F5F8C733C43EC34A438</d9p1:string></d4p1:PropertyValues><d4p1:RuleNameLSID i:nil=\"true\" /></d4p1:PropertyRule></d4p1:PropertyRules></d4p1:Rules><d4p1:SourceID>00000000-0000-0000-0000-000000000000</d4p1:SourceID></d4p1:ResultItemType></ResultTypes><Sources xmlns:d4p1=\"http://schemas.datacontract.org/2004/07/Microsoft.Office.Server.Search.Administration.Query\" /><UserSegments xmlns:d4p1=\"http://www.microsoft.com/sharepoint/search/KnownTypes/2008/08\" /></SearchQueryConfigurationSettings></SearchQueryConfigurationSettings><SearchRankingModelConfigurationSettings><RankingModels xmlns:d3p1=\"http://schemas.microsoft.com/2003/10/Serialization/Arrays\" /></SearchRankingModelConfigurationSettings><SearchSchemaConfigurationSettings><Aliases xmlns:d3p1=\"http://schemas.datacontract.org/2004/07/Microsoft.Office.Server.Search.Administration\"><d3p1:LastItemName i:nil=\"true\" /><d3p1:dictionary xmlns:d4p1=\"http://schemas.microsoft.com/2003/10/Serialization/Arrays\" /></Aliases><CategoriesAndCrawledProperties xmlns:d3p1=\"http://schemas.microsoft.com/2003/10/Serialization/Arrays\" /><CrawledProperties xmlns:d3p1=\"http://schemas.datacontract.org/2004/07/Microsoft.Office.Server.Search.Administration\" i:nil=\"true\" /><ManagedProperties xmlns:d3p1=\"http://schemas.datacontract.org/2004/07/Microsoft.Office.Server.Search.Administration\"><d3p1:LastItemName i:nil=\"true\" /><d3p1:dictionary xmlns:d4p1=\"http://schemas.microsoft.com/2003/10/Serialization/Arrays\" /><d3p1:TotalCount>0</d3p1:TotalCount></ManagedProperties><Mappings xmlns:d3p1=\"http://schemas.datacontract.org/2004/07/Microsoft.Office.Server.Search.Administration\"><d3p1:LastItemName i:nil=\"true\" /><d3p1:dictionary xmlns:d4p1=\"http://schemas.microsoft.com/2003/10/Serialization/Arrays\" /></Mappings><Overrides xmlns:d3p1=\"http://schemas.datacontract.org/2004/07/Microsoft.Office.Server.Search.Administration\"><d3p1:LastItemName i:nil=\"true\" /><d3p1:dictionary xmlns:d4p1=\"http://schemas.microsoft.com/2003/10/Serialization/Arrays\" /></Overrides></SearchSchemaConfigurationSettings><SearchSubscriptionSettingsConfigurationSettings i:nil=\"true\" /></SearchConfigurationSettings>";
        }

        public class WebPartGroups
        {
            public const string Omnia = "Omnia";
        }

        public class CustomFields
        {
            public const string OmniaCustomDataField = "PFPCustomDataField";
            public const string OmniaDraftNavigationTermField = "PFPDraftNavTermField";
            public const string OmniaPageLanguage = "OMILang";
            public const string OmniaPageSourceLanguage = "OMISourceLang";
            public const string OmniaPageTargetLanguage = "OMITargetLangs";
            public const string OmniaGlueData = "OMIGlueDataField";
            public const string OmniaPublishingEndDate = "OMIPublishingEndDate";
            public const string OmniaPublishingStartDate = "OMIPublishingStartDate";
        }

        public class Configurations
        {
            public class ControlSettings
            {
                public const string OmniaControlId = "8a9dcdbf-89a9-4622-9f32-008becff82cc";
                public const string Region = "svcsettings";
                public const string PageScope = "page";
                public const string ShowLike = "\"showLikes\":false";
                public const string ShowComment = "\"showComments\":false";
            }
            /// <summary>
            /// Regions
            /// </summary>
            public class Regions
            {
                public const string NavigationSourceUrl = "navigationsourceurl";

                /// <summary>
                /// The intranet core settings
                /// </summary>
                public const string IntranetCoreSettings = "IntranetCoreSettings";

                /// <summary>
                /// The notification settings
                /// </summary>
                public const string NotificationSettings = "NotificationSettings";

                public const string NavigationSettings = "NavigationSettings";

                public const string NavigationCache = "intranetnavigationcache";
                public const string NavigationLanguageCache = "intranetnavigationlanguagecache";
                public const string NavigationCacheBackup = "intranetnavigationcacheBackup";
                public const string HashNavigationCache = "intranethashnavigationcache";


                public const string ContentManagementSettings = "contentmanagementsettings";

                public const string ScheduledPublishingSettings = "scheduledpublishingsettings";

                public const string OmniaIntranetSettings = "Omnia.Intranet";

                public const string WebReportTimeLimit = "webreportsettings";

                public const string SetASDefaultPage = "SetAsDefaultPage";
            }

            /// <summary>
            /// Names
            /// </summary>
            public class Names
            {
                /// <summary>
                /// The navigation source delay time
                /// </summary>
                public const string NavigationSourceDelayTime = "Navigation_Source_Delay_Time_Minutes";

                public const string NavigationMaximumRetry = "Navigation_Maximum_Retry_Times";

                /// <summary>
                /// The disable notification comment
                /// </summary>
                public const string DisableNotificationComment = "Disable_e-mail_notifications_on_comments";

                /// <summary>
                /// The localization language in intranet
                /// </summary>
                public const string LocalizationLanguage = "Localization_language";

                public const string PeopleTermGroupNameSettings = "People_Term_Group_Name";

                public const string UseMediaPickerSettings = "Use_Media_Picker";
                public const string MediaPickerMaxFileSizeSettings = "Media_Picker_Max_File_Size";

                public const string ManagedPropertiesForTitle = "Managed_Properties_For_Title";
                public const string ManagedPropertiesForReviewDate = "Managed_Properties_For_Review_Date";

                public const string BaseApiUrl = "Omnia.Intranet.apiurl";

                public const string WebReportTimeLimit = "webreport_timelimit";
            }
        }

        /// <summary>
        /// Public Api Service
        /// </summary>
        public static class Api
        {
            public const string DefaultContentType = "application/x-www-form-urlencoded";

            public static class Parameters
            {
                public const string SPUrl = "SPUrl";
                public const string SPHostUrl = "SPHostUrl";
                public const string IsFileUrl = "IsFileUrl";
                public const string IsAppContext = "IsAppContext";
                public const string Lang = "Lang";
                public const string PfpAction = "PFPAction";
                public const string TokenKey = "TokenKey";
                public const string ContentType = "ContentType";
                public const string TenantId = "TenantId";
                public const string ApiSecret = "ApiSecret";
            }
        }

        public class Security
        {
            /// <summary>
            /// The token key
            /// </summary>
            public const string TokenKey = "TokenKey";

            public class Roles
            {
                public const string IntranetCoreAdmin = "IntranetCore.Admin";
                public const string ImportantAnnouncementAdmin = "ImportantAnnouncement.Admin";
                public const string LinksManagementAdmin = "LinksManagement.Admin";
                public const string BannerAdmin = "Banner.Admin";
                public const string PublishingAdmin = "Publishing.Admin";
            }
        }

        /// <summary>
        /// AssemblyNames
        /// </summary>
        public static class AssemblyNames
        {
            /// <summary>
            /// 
            /// </summary>
            public const string IntranetRepositories = "Omnia.Intranet.Repositories.dll";
        }

        /// <summary>
        /// WebTemplates
        /// </summary>
        public class WebTemplates
        {
            /// <summary>
            /// The publishing site web template
            /// </summary>
            public const string PublishingSite = "CMSPUBLISHING#0";

            private static string[] _publishingSiteDisplays = { "BLANKINTERNET", "CMSPUBLISHING" };

            /// <summary>
            /// Gets the publishing site displays.
            /// </summary>
            /// <value>
            /// The publishing site displays.
            /// </value>
            public static string[] PublishingSiteDisplays
            {
                get
                {
                    return _publishingSiteDisplays;
                }
            }
        }

        /// <summary>
        /// NavigationTermProperties
        /// </summary>
        public class NavigationTermProperties
        {
            public static readonly string OmniaIsDraftNavigationTerm = "PFPIsDraftNavigationTerm";
            public static readonly string OmniaOwnerNavigationTerm = "PFPOwnerNavigationTerm";
            public static readonly string OmniaSupportLegacyWebpart = "PFPSupportLegacyWebpart";
        }

        /// <summary>
        /// Targeting
        /// </summary>
        public class Targeting
        {
            public static readonly string PageTargetingFieldPrefix = "otp";
            public static readonly string HasTargetingFieldInternalName = "otpHasTargeting";
        }

        public static class Caching
        {
            public const string ParentTerms = "Omnia.Intranet.Core.Caching.ParentTerms";
            public const string ParentTermsRequest = "Omnia.Intranet.Core.Caching.ParentTermsRequest";

            public const string UserProfileProperties = "Omnia.Intranet.Core.Caching.UserProfileProperties";
            public const string UserProfilePropertiesRequest = "Omnia.Intranet.Core.Caching.UserProfilePropertiesRequest";

            public const string NavigationNodes = "Omnia.Intranet.Extensibility.Core.Caching.Navigation";

            public const string TermStrPrefix = "cacheTermStrId_";
            public const string UserValueCachePrefix = "cacheUserValueStr_";

            public const string NavigationTopNodeList = "Omnia.Intranet.Extensibility.Core.Caching.NavigationTopNodeList_{0}";
            public const string NavigationTopNode = "Omnia.Intranet.Extensibility.Core.Caching.NavigationTopNode_{0}_{1}_{2}";
            public const string AllNavigationTopNode = "Omnia.Intranet.Extensibility.Core.Caching.AllNavigationTopNode_{0}";
            public const string NavigationLanguages = "Omnia.Intranet.Extensibility.Core.Caching.NavigationLanguages_{0}";
            public const string TopNodes = "TopNodes";
            public const string UpdateNavigation = "OMIUpdateNavigation";
            public const string EnsuredFeedbackEmail = "Omnia.Intranet.Core.Caching.EnsuredFeedbackEmail";
            public const string MediaPickerMaxFileSize = "Omnia.Intranet.Core.Caching.MediaPickerMaxFileSize";
            public const string SocialOneFlowSettings = "Omnia.Intranet.Core.Caching.SocialOneFlowSettings";
        }

        public static class Search
        {
            /// <summary>
            /// Properties to query in Search
            /// </summary>
            public static class Properties
            {
                /// <summary>
                /// The title
                /// </summary>
                public const string Title = "Title";
                /// <summary>
                /// The path
                /// </summary>
                public const string Path = "Path";
                /// <summary>
                /// The content class
                /// </summary>
                public const string ContentClass = "contentclass";
                /// <summary>
                /// The Description
                /// </summary>
                public const string Description = "Description";
                /// <summary>
                /// The file extension from url
                /// </summary>
                public const string FileExtension = "FileExtension";
                /// <summary>
                /// The actual file extension
                /// </summary>
                public const string FileType = "FileType";
                /// <summary>
                /// The server redirected preview URL
                /// </summary>
                public const string ServerRedirectedPreviewURL = "ServerRedirectedPreviewURL";
                /// <summary>
                /// The server redirected URL
                /// </summary>
                public const string ServerRedirectedURL = "ServerRedirectedURL";
                /// <summary>
                /// The hit highlighted summary
                /// </summary>
                public const string HitHighlightedSummary = "HitHighlightedSummary";
                /// <summary>
                /// The hit highlighted properties
                /// </summary>
                public const string HitHighlightedProperties = "HitHighlightedProperties";
                /// <summary>
                /// The picture URL
                /// </summary>
                public const string PictureURL = "PictureURL";
                /// <summary>
                /// The account name
                /// </summary>
                public const string AccountName = "AccountName";
                /// <summary>
                /// The about me
                /// </summary>
                public const string AboutMe = "AboutMe";
                /// <summary>
                /// The sp web URL
                /// </summary>
                public const string SPWebUrl = "SPWebUrl";

                /// <summary>
                /// The site title
                /// </summary>
                public const string SiteTitle = "SiteTitle";

                /// <summary>
                /// The site name
                /// </summary>
                public const string SiteName = "SiteName";

                /// <summary>
                /// The site logo
                /// </summary>
                public const string SiteLogo = "SiteLogo";

                /// <summary>
                /// The parent link
                /// </summary>
                public const string ParentLink = "ParentLink";

                /// <summary>
                /// The last modified time
                /// </summary>
                public const string LastModifiedTime = "LastModifiedTime";

                /// <summary>
                /// The created owsdate
                /// </summary>
                public const string Created = "Created";

                /// <summary>
                /// The last modified user
                /// </summary>
                public const string ModifiedBy = "ModifiedBy";

                /// <summary>
                /// The author
                /// </summary>
                public const string Author = "Author";

                /// <summary>
                /// The site
                /// </summary>
                public const string Site = "Site";

                /// <summary>
                /// The ExpiresOWSDATE
                /// </summary>
                public const string ExpiresOWSDATE = "ExpiresOWSDATE";

                /// <summary>
                /// The ExpirationTime
                /// </summary>
                public const string ExpirationTime = "ExpirationTime";

                /// <summary>
                /// The BodyOWSMTXT
                /// </summary>
                public const string BodyOWSMTXT = "BodyOWSMTXT";

                /// <summary>
                /// The due date
                /// </summary>
                public const string DueDateOWSDATE = "DueDateOWSDATE";

                /// <summary>
                /// The status owschcs
                /// </summary>
                public const string StatusOWSCHCS = "StatusOWSCHCS";

                /// <summary>
                /// The web template
                /// </summary>
                public const string WebTemplate = "WebTemplate";

                /// <summary>
                /// The content type identifier
                /// </summary>
                public const string ContentTypeId = "ContentTypeId";

                /// <summary>
                /// 
                /// </summary>
                public const string FileName = "FileName";

                public const string ViewsRecent = "ViewsRecent";

                public const string ViewsLifeTime = "ViewsLifeTime ";

                public const string ListItemID = "ListItemID";

                public const string PublishingContact = "PublishingContactOWSUSER";

                public const string OMIReviewDate = "OMIReviewDateOWSDATE";

                public const string TitleRefinableString = "RefinableString00";

                public const string OMIReviewDateRefinableDate = "RefinableDate00";

                public const string CheckoutUserOWSUSER = "CheckoutUserOWSUSER";
            }

            /// <summary>
            /// ContentClass
            /// </summary>
            public static class ContentClass
            {
                /// <summary>
                /// The list item
                /// </summary>
                public const string ListItem = "STS_ListItem";

            }

            public static class FileExtension
            {
                public const string ASPX = "aspx";
                public const string HTML = "html";
            }
        }

        public static class ContentType
        {
            public const string ExcludedContentTypesConfigKey = "excludedcontenttypes";
            public const string ConfigRegion = "contenttypes";
        }

        public static class LocalizeKeys
        {
            public static string MessageSearchPropertyNameExisted = "$Localize:OMI.Core.SearchProperty.Messages.Message_SearchProperty_Name_Existed;";
            public static string LocalizePreferredName = "$Localize:OMI.Core.SearchProperty.Default.PreferredName;";
            public static string LocalizeFirstName = "$Localize:OMI.Core.SearchProperty.Default.FirstName;";
            public static string LocalizeLastName = "$Localize:OMI.Core.SearchProperty.Default.LastName;";
            public static string LocalizeDepartment = "$Localize:OMI.Core.SearchProperty.Default.Department;";
            public static string LocalizeOffice = "$Localize:OMI.Core.SearchProperty.Default.Office;";
            public static string LocalizeMobilePhone = "$Localize:OMI.Core.SearchProperty.Default.MobilePhone;";
            public static string LocalizeWorkPhone = "$Localize:OMI.Core.SearchProperty.Default.WorkPhone;";
            public static string LocalizeWorkEmail = "$Localize:OMI.Core.SearchProperty.Default.WorkEmail;";
            public static string RequireCorrectDefaultLanguage = "$Localize:OMI.ContentManagement.MessageCorrectDefaultLanguage;";
            public static string CheckedOutByOtherUser = "$Localize:OMI.ContentManagement.MessageCheckedOutByOtherUser;";
            public static string CanNotEnableLegacyMode = "$Localize:OMI.ContentManagement.CanNotEnableLegacyMode;";
        }

        public static class PagingSort
        {
            public static string OrderBy = "OrderBy";
            public static string ThenBy = "ThenBy";
            public static string Descending = "Descending";
            public static string Ascending = "Ascending";
        }

        public static class Queues
        {
            public const string SyncCommentLike = "SyncCommentLike";
            public const string SyncNodesToSPLists = "SyncNodesToSPLists";
            public const string RecoverNavigationNodes = "RecoverNavigationNodes";
            public const string SyncAllNodesFromDBToSPLists = "SyncAllNodesFromDBToSPLists";
            public const string SyncWebSummaryStatistics = "SyncWebSummaryStatistics";
            public const string SocialOneFlowMigration = "SocialOneFlowMigration";
        }

        public static class QueueSynchronousTransactionIds
        {
            public static readonly Guid SyncCommentLike = new Guid("4EF9BF95-3610-488F-911F-697245591C11");
            //public static readonly Guid NavigationSyncFromDBToSPLists = new Guid("44CC83F2-D06B-4C13-B26F-4D8AF73FA5B0");
        }

        public static class Features
        {
            public static class NavigationCore
            {
                public const string FeatureId = "437A3CD2-C031-452F-B67B-8048D4239B45";
                public const string NavigationDbUpgradeVersion = "1.1.69";
                public const string NavigationSyncDbToSPListUpgradeVersion = "1.1.140";
            }

            public static class WebStatistics
            {
                public const string FeatureId = "F959112D-D822-4BF4-AB3A-D1D113B7F122";
            }
        }

        public static class Navigation
        {
            public const string RootNodeDefaultPageName = "default.aspx";
        }

        public static class ViewTemplates
        {
            public const string LatestNews = "4E5CE245-15DE-42E5-AD7A-31A84988C53A";
            public const string RelatedNews = "CF63A0D2-25D1-4685-99F5-18921C8275FC";
            public const string MyLink = "F23DA197-8EFF-4FB5-AE42-1E0B69D46A53";
        }

        public static class WebStatistics
        {
            public const int PageStatisticsMonthRange = 3;
        }

        public static class UserProfileCompleteness
        {
            public const string LastVisitedStatisticObjectId = "b37751db-dfda-4653-b9ff-208d306afc63";
            public const string ProfilePhotoAgreedStatisticObjectId = "3116b73c-407d-4bec-9347-aefe8e4a3057";
        }

        public static class NavigationSyncDBToSPLists
        {
            public const int MaxListItemsToExecuteQuery = 100;
        }

        // ref: https://aspnetguru.wordpress.com/2007/06/01/understanding-the-sharepoint-calendar-and-how-to-export-it-to-ical-format/
        public static class CalendarEventType
        {
            public const int NormalEvent = 0;
            public const int RecurringEvent = 1;
            public const int DeletedEvent = 3;
            public const int ExceptionEvent = 4;
        }

        public static class MediaPicker
        {
            public const long MaxFileSizeToResizeKB = 2048;
        }

        public static class DateFormater
        {
            public static Dictionary<string, string> DateFormatByLocaleId = new Dictionary<string, string>()
            {
                {"ar","DD/MM/YYYY"},
                {"ar-ae","DD/MM/YYYY"},
                {"ar-bh","DD/MM/YYYY"},
                {"ar-jo","DD/MM/YYYY"},
                {"ar-kw","DD/MM/YYYY"},
                {"ar-lb","DD/MM/YYYY"},
                {"ar-sa","DD/MM/YYYY"},
                {"bg-bg","YYYY-M-D"},
                {"ca","DD/MM/YYYY"},
                {"ca-es","DD/MM/YYYY"},
                {"ca-es-euro","DD/MM/YYYY"},
                {"cs","D.M.YYYY"},
                {"cs-cz","D.M.YYYY"},
                {"da","DD-MM-YYYY"},
                {"da-dk","DD-MM-YYYY"},
                {"de","DD.MM.YYYY"},
                {"de-at","DD.MM.YYYY"},
                {"de-at-euro","DD.MM.YYYY"},
                {"de-ch","DD.MM.YYYY"},
                {"de-de","DD.MM.YYYY"},
                {"de-de-euro","DD.MM.YYYY"},
                {"de-lu","DD.MM.YYYY"},
                {"de-lu-euro","DD.MM.YYYY"},
                {"el-gr","D/M/YYYY"},
                {"en","M/D/YYYY"},
                {"en-au","D/MM/YYYY"},
                {"en-b","M/D/YYYY"},
                {"en-bm","M/D/YYYY"},
                {"en-ca","DD/MM/YYYY"},
                {"en-gb","DD/MM/YYYY"},
                {"en-gh","M/D/YYYY"},
                {"en-id","M/D/YYYY"},
                {"en-ie","DD/MM/YYYY"},
                {"en-ie-euro","DD/MM/YYYY"},
                {"en-nz","D/MM/YYYY"},
                {"en-sg","M/D/YYYY"},
                {"en-us","M/D/YYYY"},
                {"en-za","YYYY/MM/DD"},
                {"es"   ,"D/MM/YYYY"},
                {"es-ar","DD/MM/YYYY"},
                {"es-bo","DD-MM-YYYY"},
                {"es-cl","DD-MM-YYYY"},
                {"es-co","D/MM/YYYY"},
                {"es-cr","DD/MM/YYYY"},
                {"es-ec","DD/MM/YYYY"},
                {"es-es","D/MM/YYYY"},
                {"es-es-euro","D/MM/YYYY"},
                {"es-gt","D/MM/YYYY"},
                {"es-hn","mM-DD-YYYY"},
                {"es-mx","D/MM/YYYY"},
                {"es-pe","DD/MM/YYYY"},
                {"es-pr","mM-DD-YYYY"},
                {"es-py","DD/MM/YYYY"},
                {"es-sv","mM-DD-YYYY"},
                {"es-uy","DD/MM/YYYY"},
                {"es-ve","DD/MM/YYYY"},
                {"et-ee","D.MM.YYYY"},
                {"fi"   ,"D.M.YYYY"},
                {"fi-fi","D.M.YYYY"},
                {"fi-fi-euro","D.M.YYYY"},
                {"fr"   ,"DD/MM/YYYY"},
                {"fr-be","D/MM/YYYY"},
                {"fr-ca","YYYY-MM-DD"},
                {"fr-ch","DD.MM.YYYY"},
                {"fr-fr","DD/MM/YYYY"},
                {"fr-fr-euro","DD/MM/YYYY"},
                {"fr-lu","DD/MM/YYYY"},
                {"fr-mc","DD/MM/YYYY"},
                {"hr-hr","YYYY.MM.DD"},
                {"hu"   ,"YYYY.MM.DD" },
                {"hy-am","M/D/YYYY"},
                {"js-is","D.M.YYYY"},
                {"jt","DD/MM/YYYY" },
                {"jt-CH","DD.MM.YYYY"},
                {"jt-IT","DD/MM/YYYY" },
                {"jw","DD/MM/YYYY"},
                {"jw-il","DD/MM/YYYY"},
                {"ja","YYYY/MM/DD"},
                {"ja-jp","YYYY/MM/DD"},
                {"kk-kz","M/D/YYYY"},
                {"u","M/D/YYYY"},
                {"ko"   ,"YYYY. M. D"},
                {"ko-kr","YYYY. M. D"},
                {"lt-lt","YYYY.M.d"},
                {"lv-lv","YYYY.D.M" },
                {"ms-my","DD/MM/YYYY"},
                {"nl"   ,"D-M-YYYY"},
                {"nl-be","D/MM/YYYY"},
                {"nl-nl","D-M-YYYY"},
                {"nl-sr","D-M-YYYY"},
                {"nb"   ,"DD.MM.YYYY"},
                {"nb-no","DD.MM.YYYY"},
                {"pl"   ,"YYYY-MM-DD"},
                {"pt"   ,"DD-MM-YYYY"},
                {"pt-ao","DD-MM-YYYY"},
                {"pt-br","DD/MM/YYYY"},
                {"pt-pt","DD-MM-YYYY"},
                {"ro-ro","DD.MM.YYYY"},
                {"ru"   ,"DD.MM.YYYY"},
                {"sk-sk","D.M.YYYY"},
                {"sl-si","D.M.YYYY"},
                {"sv"   ,"YYYY-MM-DD"},
                {"sv-se","YYYY-MM-DD"},
                {"th"   ,"M/D/YYYY"},
                {"th-th","D/M/YYYY"},
                {"tr"   ,"DD.MM.YYYY"},
                {"ur-pk","M/D/YYYY"},
                {"vi-vn","DD/MM/YYYY"},
                {"zh"   ,"YYYY-M-D"},
                {"zh-cn","YYYY-M-D"},
                {"zh-hk","YYYY-M-D"},
                {"zh-tw","YYYY/M/D"}
            };
        }
        public static class ControlSettingsKey
        {
            public const string Banner = "6F4F4334-1052-4AB4-A823-A3072951DB85";
            public const string Poll = "17683B04-0156-4715-BC0F-5C5DC688CB33";
            public const string News = "FB58CF64-F53E-4A45-A489-DF747555834B";
            public const string PeopleRollUp = "E03EEBBA-CA3D-46FF-ABFE-E75FFFAB5177";
            public const string DocumentRollUp = "97AE49DF-2B1B-45C7-B3BE-873721313120";
        }
    }
}
