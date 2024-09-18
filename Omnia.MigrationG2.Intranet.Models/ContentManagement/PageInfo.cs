using Microsoft.SharePoint.Client;
using Omnia.MigrationG2.Foundation.Core.SharePoint;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Intranet.Models.ContentManagement
{
    public class PageInfo
    {
        public string Title { get; set; }
        public string NavigationTitle { get; set; }
        public string NavigationDescription { get; set; }
        public string Content { get; set; }
        public string Summary { get; set; }
        public string Image { get; set; }
        public string PageUrl { get; set; }
        public bool CheckedOut { get; set; }
        public string CheckedOutBy { get; set; }
        public bool HasCancelCheckoutPermission { get; set; }
        public string ContentTypeId { get; set; }
        public string PublishingContactName { get; set; }
        public FieldUserValue PublishingContact { get; set; } // For mapping to G2
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset ModifiedAt { get; set; }
        public bool Published { get; set; }

        public string SiteUrl { get; set; }
        public string SiteRelativeUrl { get; set; }
        public string SiteTitle { get; set; }
        public string SiteCollectionUrl { get; set; }
        public string SiteCollectionRelativeUrl { get; set; }
        public string SiteCollectionTitle { get; set; }
        public string PageLibraryUrl { get; set; }

        public string CustomData { get; set; }
        public bool HasCustomDataField { get; set; }

        public string DraftNavigationTerm { get; set; }

        //public SettingsAdministration PublishSettings { get; set; }
        public List<PageProperty> PagePropertiesSetting { get; set; }
        public Dictionary<string, object> Properties { get; set; }

        public bool IsSiteNotExist { get; set; }
        public bool IsPageNotExist { get; set; }
        public bool IsAccessDenied { get; set; }

        public List<string> BooleanFields { get; set; }
        public int PageItemId { get; set; }
        public int CurrentPageItemId { get; set; }
        public int DefaultPageItemId { get; set; }
        public string PageLayoutUrl { get; set; }
        public string PageListId { get; set; }
        public string ListTitle { get; set; }
        //public Models.Navigations.NavigationNode NavigationNode { get; set; }

        // we ignore migrating targeting for now
        //public PageTargetingForm ObjectTargetingForm { get; set; }
        //public TargetingSettings TargetingSettings { get; set; }
        //public TargetingDefinition TargetingDefinition { get; set; }

        public string PageLanguageLocale { get; set; }
        public PageLanguage PageSourceLanguage { get; set; }
        public List<PageLanguage> PageTargetLanguage { get; set; }
        public List<LanguageLocale> PageLocales { get; set; }
        public List<PageInfo> TranslationPages { get; set; }
        public bool IsCheckOutByCurrentUser { get; set; }
        //public string CurrentUICultureName { get; set; }
        //public int CurrentLanguage { get; set; }

        public int MajorVersion { get; set; }
        public List<FileMajorVersion> FileMajorVersions { get; set; }
        public int MinorVersion { get; set; }
        public string UIVersionLabel { get; set; }
        public DateTime? PublishingStartDate { get; set; }
        public DateTime? PublishingEndDate { get; set; }
        public bool IsEnabledScheduledPublishing { get; set; }
        public bool IsSwitchingScheduledPublishing { get; set; }
        public bool IsSiteEnableScheduled { get; set; }

        public bool NeedToCorrectDefaultLanguage { get; set; }
        public string TopNodeKey { get; set; }

        public bool CanAddAndCustomizePages { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
        public Guid ListId { get; set; }
        public Guid PhysicalPageUniqueId { get; set; }
        public ExtendedClientContext Context { get; set; }
        public PageInfo()
        {
            Properties = new Dictionary<string, object>();
            PagePropertiesSetting = new List<PageProperty>();
            BooleanFields = new List<string>();
        }        
    }
}
