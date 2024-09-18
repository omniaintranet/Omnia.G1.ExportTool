using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Taxonomy;
using Omnia.MigrationG2.Collector.ExtensionMethods;
using Omnia.MigrationG2.Core;
using Omnia.MigrationG2.Foundation.Core.SharePoint;
using Omnia.MigrationG2.Intranet.Models.ContentManagement;
using Omnia.MigrationG2.Intranet.Models.Navigations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Omnia.MigrationG2.Core.Constants;
using NavigationNode = Omnia.MigrationG2.Intranet.Models.Navigations.NavigationNode;

namespace Omnia.MigrationG2.Collector.Helpers
{
    public static class EnterprisePropertiesHelper
    {
        public static bool InitContentManagementProperties(Dictionary<string, object> enterpriseProperties, PageInfoPropertiesSettings pageInfoPropertiesSettings, ExtendedClientContext context)
        {
            bool flag = true;
            foreach (var pageProperty in pageInfoPropertiesSettings.Properties)
            {
                string key = pageProperty.Key;
                if (pageProperty.Value != null)
                {
                    var pagePropertySettings = pageInfoPropertiesSettings.PagePropertiesSetting
                        .SingleOrDefault(i => i.InternalName == key);

                    if (pagePropertySettings != null && pagePropertySettings.IsMigrationG2Property == false)
                    {
                        var tflag = InitProperties(enterpriseProperties, pagePropertySettings.InternalName, pageProperty, pagePropertySettings, context);
                        flag = flag ? tflag : false;
                    }
                }
            }

            return flag;
        }

        public static bool InitDefaultProperties(Dictionary<string, object> enterpriseProperties, PageInfo pageInfo)
        {
            bool flag = true;
            var propertiesMappings = AppUtils.MigrationSettings.DefaultPropertiesMappings;

            if (propertiesMappings.ContainsKey(DefaultProperties.PageSummary))
            {
                enterpriseProperties[propertiesMappings[DefaultProperties.PageSummary]] = pageInfo.Summary;

                if (string.IsNullOrEmpty(pageInfo.Summary))
                {
                    enterpriseProperties[propertiesMappings[DefaultProperties.PageSummary]] = pageInfo.NavigationDescription;
                }
            }

            if (propertiesMappings.ContainsKey(DefaultProperties.PageContent))
            {
                enterpriseProperties[propertiesMappings[DefaultProperties.PageContent]] = pageInfo.Content;
            }

            if (propertiesMappings.ContainsKey(DefaultProperties.PageImage))
            {
                enterpriseProperties[propertiesMappings[DefaultProperties.PageImage]] = pageInfo.Image;
            }

            if (propertiesMappings.ContainsKey(DefaultProperties.PublishingContact))
            {
                var values = pageInfo.PublishingContact.ToUserFieldG2(pageInfo.Context);
                flag = values.Any(i => string.IsNullOrEmpty(i) == false);
                enterpriseProperties[propertiesMappings[DefaultProperties.PublishingContact]] = values;
            }
            if (propertiesMappings.ContainsKey(DefaultProperties.CreatedBy))
            {
                enterpriseProperties[propertiesMappings[DefaultProperties.CreatedBy]] = pageInfo.CreatedBy;
            }
            if (propertiesMappings.ContainsKey(DefaultProperties.CreatedAt))
            {
                enterpriseProperties[propertiesMappings[DefaultProperties.CreatedAt]] = pageInfo.CreatedAt;
            }
            if (propertiesMappings.ContainsKey(DefaultProperties.ModifiedBy))
            {
                enterpriseProperties[propertiesMappings[DefaultProperties.ModifiedBy]] = pageInfo.ModifiedBy;
            }
            if (propertiesMappings.ContainsKey(DefaultProperties.ModifiedAt))
            {
                enterpriseProperties[propertiesMappings[DefaultProperties.ModifiedAt]] = pageInfo.ModifiedAt;
            }
            if (propertiesMappings.ContainsKey(DefaultProperties.FileMajorVersionCreatedAt))
            {
                enterpriseProperties[propertiesMappings[DefaultProperties.FileMajorVersionCreatedAt]] =
                    pageInfo.FileMajorVersions[0].CreatedAt.ToDateTimeG2();
            }

            return flag;
        }

        public static bool InitCustomProperties(Dictionary<string, object> enterpriseProperties, PageInfoPropertiesSettings pageInfoPropertiesSettings, ExtendedClientContext context)
        {
            bool flag = true;
            var propertiesMappings = AppUtils.MigrationSettings.CustomPropertiesMappings;

            if (propertiesMappings != null)
            {
                foreach (var pageProperty in pageInfoPropertiesSettings.Properties)
                {
                    string key = pageProperty.Key;
                    if (propertiesMappings.ContainsKey(key) && pageProperty.Value != null)
                    {
                        var pagePropertySettings = pageInfoPropertiesSettings.PagePropertiesSetting
                            .SingleOrDefault(i => i.InternalName == key);

                        if (pagePropertySettings != null && pagePropertySettings.IsMigrationG2Property)
                        {
                            var tflag = InitProperties(enterpriseProperties, propertiesMappings[key], pageProperty, pagePropertySettings, context);
                            flag = flag ? tflag : false;
                        }
                    }
                }
            }

            return flag;
        }

        private static bool InitProperties(Dictionary<string, object> enterpriseProperties, string key, KeyValuePair<string, object> pageProperty, PageProperty pagePropertySettings, ExtendedClientContext context)
        {
            bool flag = true;

            if (pagePropertySettings.TypeAsString == Foundation.Core.Fields.UserFieldAttribute.TypeSingleAsStringValue)
            {
                var values = (pageProperty.Value as FieldUserValue).ToUserFieldG2(context);
                flag = values.Any(i => string.IsNullOrEmpty(i) == false);
                enterpriseProperties[key] = values;
            }
            else if (pagePropertySettings.TypeAsString == Foundation.Core.Fields.UserFieldAttribute.TypeMultiAsStringValue)
            {
                var values = (pageProperty.Value as FieldUserValue).ToUserFieldG2(context);
                flag = values.Any(i => string.IsNullOrEmpty(i) == false);
                enterpriseProperties[key] = values;
            }
            else if (pagePropertySettings.TypeAsString == Foundation.Core.Fields.DateTimeFieldAttribute.TypeAsStringValue)
            {
                enterpriseProperties[key] = Convert.ToDateTime(pageProperty.Value).ToDateTimeG2();
            }
            else if (pagePropertySettings.TypeAsString == Foundation.Core.Fields.ManagedMetadataFieldAttribute.TypeSingleAsStringValue)
            {
                var termSetId = pagePropertySettings.AdditionalInfo[Foundation.Core.Constants.SharePoint.PagePropertyInfoKey.TermSetId];
                enterpriseProperties[key] = (pageProperty.Value as TaxonomyFieldValue).ToTermValueG2(termSetId);
            }
            else if (pagePropertySettings.TypeAsString == Foundation.Core.Fields.ManagedMetadataFieldAttribute.TypeMultiAsStringValue)
            {
                var termSetId = pagePropertySettings.AdditionalInfo[Foundation.Core.Constants.SharePoint.PagePropertyInfoKey.TermSetId];
                enterpriseProperties[key] = (pageProperty.Value as TaxonomyFieldValueCollection).ToTermValueG2(termSetId);
            }
            else
            {
                enterpriseProperties[key] = pageProperty.Value.ToString();
            }

            return flag;
        }
    }
}
