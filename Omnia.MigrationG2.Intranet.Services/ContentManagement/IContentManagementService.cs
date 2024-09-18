using Omnia.MigrationG2.Intranet.Models.ContentManagement;
using Omnia.MigrationG2.Intranet.Models.Navigations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Intranet.Services.ContentManagement
{
    public interface IContentManagementService
    {
        PageInfo GetQuickPage(string navigationSourceUrl, NavigationNode navigationNode, int lcid, string locale = null, bool isGlueEdit = false);

        PageInfo GetQuickPageByPhysicalUrl(string navigationSourceUrl, string siteUrl, string pageUrl, int lcid, string locale = null, bool isGlueEdit = false);

        PageInfoPropertiesSettings GetQuickPagePropertiesSettings(string navigationSourceUrl, string pageUrl, Guid pageListId);

        /// <summary>
        /// Get Page Properties Settings
        /// </summary>
        /// <returns></returns>
        SettingsAdministration GetPagePropertiesSettings(string siteUrl);
    }
}
