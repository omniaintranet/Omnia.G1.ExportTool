using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Foundation.Core
{
    public class Constants
    {

        public class App
        {
            public const string LoginName = "AppOnly";
            public const string UserAgent = "ISV|PrecioFishbone|OmniaFoundation/1.0";
        }

        public class Settings
        {
            /// <summary>
            /// The Foundation appsettings namespace
            /// </summary>
            public const string AppSettingsNameSpace = "Omnia.Foundation.Settings";

            /// <summary>
            /// The Foundation url
            /// </summary>
            public const string FoundationUrl = "FoundationUrl";

            /// <summary>
            /// The tenant identifier key
            /// </summary>
            public const string TenantId = "TenantId";

            /// <summary>
            /// The api secret
            /// </summary>
            public const string ApiSecret = "ApiSecret";

            /// <summary>
            /// The extension identifier
            /// </summary>
            public const string ExtensionId = "ExtensionId";

            /// <summary>
            /// The extensions folder path
            /// </summary>
            public const string ExtensionsRootPath = "ExtensionsRootPath";

            /// <summary>
            /// The encryption salt
            /// </summary>
            public const string EncryptionSalt = "EncryptionSalt";

            /// <summary>
            /// The encryption phrase
            /// </summary>
            public const string EncryptionPhrase = "EncryptionPhrase";

            /// <summary>
            /// The encryption vector
            /// </summary>
            public const string EncryptionVector = "EncryptionVector";


        }

        /// <summary>
        /// Public Api Service
        /// </summary>
        public static class Api
        {
            public const string DefaultContentType = "application/x-www-form-urlencoded";

            public const string errorCreatingContext = "Error when creating SharePoint Context";

            public const string Authorization = "Authorization";

            public const string XRequestDigest = "X-RequestDigest";

            public static class Parameters
            {
                public const string SPUrl = "SPUrl";
                public const string SPHostUrl = "SPHostUrl";
                public const string IsFileUrl = "IsFileUrl";
                public const string Lang = "Lang";
                public const string PfpAction = "PFPAction";
                public const string TokenKey = "TokenKey";
                public const string ContentType = "ContentType";
                public const string TenantId = "TenantId";
                public const string ApiSecret = "ApiSecret";
                public const string IsAppContext = "IsAppContext";
                public const string AADToken = "AADToken";
            }
        }

        public class Common
        {
            /// <summary>
            /// The content type group
            /// </summary>
            public const string ContentTypeGroup = "$Localize:OMF.Common.ContentTypeGroup;";

            /// <summary>
            /// The site column group
            /// </summary>
            public const string SiteColumnGroup = "$Localize:OMF.Common.SiteColumnGroup;";

            /// <summary>
            /// The content type group
            /// </summary>
            public const string OmniaSynchronizationContentTypeGroup = "Omnia Synchronization Content Types";

            /// <summary>
            /// The site column group
            /// </summary>
            public const string OmniaSynchronizationSiteColumnGroup = "Omnia Synchronization Site Columns";

            /// <summary>
            /// The default language lcid
            /// </summary>
            public const int DefaultLanguageLCID = 1033;

            public const string NavigationSourceRootKey = "{root}";
        }

        public static class Extensions
        {
            /// <summary>
            /// The product identifier
            /// </summary>
            public static Guid BuiltInExtensionPackageId
            {
                get { return new Guid("AA000000-0000-AAAA-0000-0000000000AA"); }
            }
        }

        /// <summary>
        /// TenantCloud
        /// </summary>
        public static class TenantCloud
        {
            /// <summary>
            /// The identifier for the TenantCloud
            /// </summary>
            public static Guid TenantCloudId
            {
                get { return new Guid("FF000000-0000-FFFF-0000-0000000000FF"); }
            }

            /// <summary>
            /// The app feature identifier for tenant resource created by users
            /// </summary>
            /// <value>
            /// The user created resource.
            /// </value>
            public static Guid UserCreatedResourceId
            {
                get { return new Guid("AA000000-0000-FFFF-0000-0000000000AA"); }
            }

            /// <summary>
            /// Gets the Omnia instance mode configuration key.
            /// </summary>
            /// <value>
            /// The Omnia instance mode configuration key.
            /// </value>
            public static string InstanceModeConfigurationKey
            {
                get { return "OmniaInstanceMode"; }
            }
        }

        public class AzADSecurity
        {
            /// <summary>
            /// Configuration key
            /// </summary>
            public const string isAzADEnabled = "enableAzAD";

            /// <summary>
            /// Return Message when there is no AzAD Token in Cache
            /// </summary>
            public const string MissingAzADTokenMessage = "AzAdSecurity.MissingToken";

            /// <summary>
            /// The Cache Region
            /// </summary>
            public const string CacheRegion = "AzADToken";

            /// <summary>
            /// The Configuration Region
            /// </summary>
            public const string Region = "AzAD";
        }

        public class Security
        {
            /// <summary>
            /// The un authorized message
            /// </summary>
            public const string UnAuthorizedMessage = "You dont have the required permissions";

            /// <summary>
            /// Roles
            /// </summary>
            public class Roles
            {
                /// <summary>
                /// Role that has all the permissions in a tenant
                /// </summary>
                public const string GlobalAdmin = "Omnia.GlobalAdmin";

                /// <summary>
                /// Role that has the permission to activate app features in a tenant/site collection/site depending on the scope, , except of activating new Tenant app features
                /// </summary>
                public const string Admin = "Omnia.Admin";
            }
        }

        /// <summary>
        /// Caching
        /// </summary>
        public static class Caching
        {
            //internal const string TenantCloudCacheVersionKey = "FSO.Apps.Core.Caching.TenantCloudCacheVersion";

            /// <summary>
            /// The tenants key
            /// </summary>
            public const string PurgeLogInfoKey = "Omnia.Foundation.Extensibility.Core.Caching.PurgeLog";

            /// <summary>
            /// The tenants key
            /// </summary>
            public const string TenantsKey = "Omnia.Foundation.Extensibility.Core.Caching.Tenants";

            /// <summary>
            /// The global navigation key
            /// </summary>
            public const string NavigationKey = "Omnia.Foundation.Extensibility.Core.Caching.Navigation";

            /// <summary>
            /// The global key for page layout collections
            /// </summary>
            public const string PageLayoutKey = "Omnia.Foundation.Extensibility.Core.Caching.PageLayouts";

            /// <summary>
            /// The glue layout region
            /// </summary>
            public const string GlueLayoutsRegion = "GlueLayouts";

            /// <summary>
            /// The user token region
            /// </summary>
            public const string UserTokenRegion = "UserToken";

            /// <summary>
            /// The global key for All Web Url collections
            /// </summary>
            public const string AllWebUrls = "Omnia.Foundation.Extensibility.Core.Caching.AllWebUrls";

            /// <summary>
            /// The global key for The page list identifier
            /// </summary>
            public const string PageListId = "Omnia.Foundation.Extensibility.Core.Caching.PageListId";

            /// <summary>
            /// The global key for The user profile properties
            /// </summary>
            public const string UserProfileProperties = "Omnia.Foundation.Extensibility.Core.Caching.UserProfileProperties";

            /// <summary>
            /// 
            /// </summary>
            public const string ParentTerms = "Omnia.Foundation.Extensibility.Core.Caching.ParentTerms";

            /// <summary>
            /// The partial key for Termset Cache
            /// </summary>
            public const string TermSet = "Omnia.Foundation.Extensibility.Core.Caching.TermSet";

            /// <summary>
            /// 
            /// </summary>
            public const string ParentTermsRequest = "Omnia.Foundation.Extensibility.Core.Caching.ParentTermsRequest";

            /// <summary>
            /// 
            /// </summary>
            public const string UserADGroups = "Omnia.Foundation.Extensibility.Core.Caching.UserADGroups";

            /// <summary>
            /// The user filtered ad groups
            /// </summary>
            public const string UserFilteredADGroups = "Omnia.Foundation.Extensibility.Core.Caching.UserFilteredADGroups";

            /// <summary>
            /// The publish settings key
            /// </summary>
            public const string PublishingSettingsDisplay = "Omnia.Foundation.Extensibility.Core.Caching.PublishingSettingsDisplay";

            /// <summary>
            /// The application token tenant
            /// </summary>
            public const string AppTokenTenant = "Omnia.Foundation.Extensibility.Core.Caching.AppTokenTenant";

            /// <summary>
            /// The global navigation key
            /// </summary>
            public const string SiteCollectionTemplates = "Omnia.Foundation.Extensibility.Core.Caching.SiteCollectionTemplates";

            /// <summary>
            /// The global navigation key
            /// </summary>
            public const string SiteTemplates = "Omnia.Foundation.Extensibility.Core.Caching.SiteTemplates";

            /// <summary>
            /// The API secret keys cache key
            /// </summary>
            public const string ApiSecretKeys = "Omnia.Foundation.Extensibility.Core.Caching.ApiSecretKeys";

            /// <summary>
            /// The configuration CSS key
            /// </summary>
            public const string ConfigCssKey = "Omnia.Foundation.Extensibility.Core.Caching.ConfigCssKey";

            /// <summary>
            /// The configuration js key
            /// </summary>
            public const string ConfigJsKey = "Omnia.Foundation.Extensibility.Core.Caching.ConfigJsKey";

            /// <summary>
            /// The cache key for TermStoreLanguage
            /// </summary>
            public const string TermStoreLanguage = "Omnia.Foundation.Extensibility.Core.Caching.TermStoreLanguage";

            /// <summary>
            /// The master page js
            /// </summary>
            public const string MasterPageJs = "masterpage.js";

            /// <summary>
            /// The tenant CSS
            /// </summary>
            public const string TenantCss = "tenant.public.bundle.css";
        }

        /// <summary>
        /// SharePoint
        /// </summary>
        public static class SharePoint
        {
            /// <summary>
            /// The unique identifier for SharePoint
            /// </summary>
            public const string PID = "00000003-0000-0ff1-ce00-000000000000";

            /// <summary>
            /// The taxonomy field note suffix
            /// </summary>
            public const string TaxonomyFieldNoteSuffix = "TaxHTField0";

            /// <summary>
            /// The query string term format
            /// </summary>
            public const string QueryStringTermFormat = "?TermStoreId={0}&TermSetId={1}&TermId={2}";

            /// <summary>
            /// Web Properties
            /// </summary>
            public static class WebProperties
            {
                public const string AvailablePageLayouts = "__PageLayouts";
                public const string DefaultPageLayouts = "__DefaultPageLayout";
            }

            /// <summary>
            /// PagePropertyInfoKey
            /// </summary>
            public static class PagePropertyInfoKey
            {
                public const string TermSetId = "TermSetId";
                public const string AllowMultipleValues = "AllowMultipleValues";
                public const string CreateValuesInEditForm = "CreateValuesInEditForm";
                public const string Open = "Open";
                public const string SelectionMode = "SelectionMode";
            }

            /// <summary>
            /// Fields
            /// </summary>
            public static class Fields
            {
                public const string Title = "Title";
                public const string UniqueId = "UniqueId";
                public const string PublishingAssociatedContentType = "PublishingAssociatedContentType";
                public const string PublishingHidden = "PublishingHidden";
                public const string PublishingPageContent = "PublishingPageContent";
                public const string PublishingPageImage = "PublishingPageImage";
                public const string UserName = "UserName";
                public const string ID = "ID";
                public const string Comments = "Comments";
                public const string PublishingContact = "PublishingContact";
                public const string ArticleStartDate = "ArticleStartDate";
                public const string Modified = "Modified";
                public const string TaxKeyword = "TaxKeyword";
                public const string FileRef = "FileRef";
                public const string FileLeafRef = "FileLeafRef";
                public const string ContentTypeId = "ContentTypeId";
                public const string UIVersionString = "_UIVersionString";
                public const string EventDate = "EventDate";
                public const string EndDate = "EndDate";
                public const string Description = "Description";
                public const string Category = "Category";
                public const string Location = "Location";
                public const string RecurrenceData = "RecurrenceData";
                public const string fRecurrence = "fRecurrence";
                public const string fAllDayEvent = "fAllDayEvent";
                public const string Duration = "Duration";
                public const string MasterSeriesItemID = "MasterSeriesItemID";
                public const string RecurrenceID = "RecurrenceID";
                public const string EventType = "EventType";
                public const string PublishingPageLayout = "PublishingPageLayout";
            }

            public class CustomFields
            {
                public const string OmniaSynchronizationItemId = "OmniaSynchronizationItemId";
                public const string OmniaSynchronizationItemJson = "OmniaSynchronizationItemJson";
            }

            public class Lists
            {
                public const string SiteFeedImages = "SiteFeedImagesTest";
            }

            /// <summary>
            /// Catalog List Type
            /// </summary>
            public static class CatalogType
            {
                public const int MasterPage = 116;
            }

            /// <summary>
            /// ContentTypes
            /// </summary>
            public class ContentTypes
            {

                /// <summary>
                /// PageLayout
                /// </summary>
                public class PageLayout
                {
                    /// <summary>
                    /// The identifier
                    /// </summary>
                    public const string Id = "0x01010007FF3E057FA8AB4AA42FCB67B453FFC100E214EEE741181F4E9F7ACC43278EE811";
                }

                /// <summary>
                /// Image
                /// </summary>
                public class Image
                {
                    /// <summary>
                    /// The identifier
                    /// </summary>
                    public const string Id = "0x0101009148F5A04DDD49CBA7127AADA5FB792B00AADE34325A8B49CDA8BB4DB53328F214";
                }

            }
        }

        /// <summary>
        /// Localization Constants
        /// </summary>
        public class Localization
        {
            /// <summary>
            /// The default localization compile sequence
            /// </summary>
            public const int DefaultLocalizationCompileSequence = 100;
        }

        /// <summary>
        /// SiteTemplate
        /// </summary>
        public class SiteTemplate
        {
            /// <summary>
            /// The omnia site request identifier property key
            /// </summary>
            public const string OmniaSiteRequestIdPropertyKey = "omf_siterequest_id";
        }

        /// <summary>
        /// Queues
        /// </summary>
        public class Queues
        {
            public class BuiltInQueues
            {
                /// <summary>
                /// Add or update extension package
                /// </summary>
                public const string NewExtensionPackage = "newextensionpackage";

                /// <summary>
                /// Add or update documentation package
                /// </summary>
                public const string NewDoucmentationPackage = "newdocumentationpackage";

                /// <summary>
                /// Remove extension package
                /// </summary>
                public const string RemoveExtensionPackage = "removeextensionpackage";

                /// <summary>
                /// Reload the extension package
                /// </summary>
                public const string ReloadExtensionPackage = "reloadextensionpackage";

                /// <summary>
                /// The process feature
                /// </summary>
                public const string ProcessFeature = "processfeature";

                /// <summary>
                /// provision site queuename
                /// </summary>
                public const string ProvisionSite = "provisionsite";

                /// <summary>
                /// provision site collection
                /// </summary>
                public const string ProvisionSiteCollection = "provisionsitecollection";

                /// <summary>
                /// setup site collection
                /// </summary>
                public const string SetupNewSiteCollection = "setupnewsitecollection";

                /// <summary>
                /// upgrade sites to the latest settings of site template
                /// </summary>
                public const string MigrateSites = "migratesites";

                /// <summary>
                /// The attach site template
                /// </summary>
                public const string AttachSiteTemplate = "attachsitetemplate";

                /// <summary>
                /// Cache all terms in a termset
                /// </summary>
                public const string CacheTermSet = "cachetermset";
            }
        }

        public class Configurations
        {
            /// <summary>
            /// Regions
            /// </summary>
            public class Regions
            {
                /// <summary>
                /// Cache settings
                /// </summary>
                public const string CacheSettings = "CacheSettings";
            }

            /// <summary>
            /// Names
            /// </summary>
            public class Names
            {
                /// <summary>
                /// Terms cache time
                /// </summary>
                public const string TermsCacheTime = "TermsCacheTime";
                public const string UserProfileCacheTime = "UserProfileCacheTime";
                public const string UserADCacheTime = "UserADCacheTime";
            }
        }

        public class BundleNames
        {
            public const string DynamicBundlePrefix = "dynamicbundle.";

            public class Foundation
            {
                public const string SharePoint = "omf.public";
                public const string OmniaAdmin = "omf.admin";
            }
        }
    }
}
