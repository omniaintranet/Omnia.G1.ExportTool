using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Taxonomy;
using Newtonsoft.Json;
using Omnia.MigrationG2.Collector.Models.G1;
using Omnia.MigrationG2.Foundation.Core.SharePoint;
using Omnia.MigrationG2.Intranet.Models.ContentManagement;
using Omnia.MigrationG2.Intranet.Models.Navigations;
using Omnia.MigrationG2.Intranet.Models.News;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using NavigationNode = Omnia.MigrationG2.Intranet.Models.Navigations.NavigationNode;

namespace Omnia.MigrationG2.Collector.ExtensionMethods
{
    public static class CollectorMethods
    {
        public static bool IsPage(this NavigationNode node)
        {
            return string.IsNullOrEmpty(node.TargetUrl) == false;
        }

        public static bool IsCustomLink(this NavigationNode node)
        {
            return string.IsNullOrEmpty(node.TargetUrl);
        }

        public static bool IsPageInfoAvailable(this NavigationNode node)
        {
            if (node.PageInfo == null)
            {
                return false;
            }

            return !node.PageInfo.IsPageNotExist && !node.PageInfo.IsAccessDenied && !node.PageInfo.IsSiteNotExist;
        }

        public static string GetNodeLabel(this NavigationNode node, CultureInfo cultureInfo)
        {
            try
            {
                return node.Labels.First(i => i.LCID == cultureInfo.LCID).Value;
            }
            catch (Exception ex)
            {
                return "Node label is empty";
            }
        }

        public static bool IsPageInfoAvailable(this NewsItem newsItem)
        {
            if (newsItem.PageInfo == null)
            {
                return false;
            }

            return !newsItem.PageInfo.IsPageNotExist && !newsItem.PageInfo.IsAccessDenied && !newsItem.PageInfo.IsSiteNotExist;
        }

        public static bool IsPagePropertiesAvailable(this NavigationNode node)
        {
            if (node.PageInfoPropertiesSettings == null)
            {
                return false;
            }

            return true;
        }

        public static bool IsPagePropertiesAvailable(this NewsItem newsItem)
        {
            if (newsItem.PageInfoPropertiesSettings == null)
            {
                return false;
            }

            return true;
        }

        public static GluePageData GetGlueDataField(this PageInfo pageInfo)
        {
            if (pageInfo.Properties.ContainsKey(Intranet.Core.Constants.CustomFields.OmniaGlueData))
            {
                var fieldValue = pageInfo.Properties[Intranet.Core.Constants.CustomFields.OmniaGlueData];
                if (fieldValue != null)
                {
                    var glueData = fieldValue.ToString();
                    if (string.IsNullOrEmpty(glueData) == false)
                    {
                        try
                        {
                            return JsonConvert.DeserializeObject<GluePageData>(glueData);
                        }
                        catch(JsonException)
                        {
                            var decode = System.Web.HttpUtility.UrlDecode(glueData);    //Decode URLs in glueData   
                            return JsonConvert.DeserializeObject<GluePageData>(decode);
                        }
                    }
                }
            }

            return null;
        }

        public static string ToDateTimeG2(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd'T'HH:mm:ss'Z'");
        }

        public static List<string> ToUserFieldG2(this FieldUserValue fieldUserValue, ExtendedClientContext context)
        {
            var emails = new List<string>();
            if (fieldUserValue != null)
            {
                try
                {

                    var user = context.Web.GetUserById(fieldUserValue.LookupId);
                    context.Load(user, x => x.LoginName);
                    context.ExecuteQuery();
                    emails.Add(user.LoginName.Substring(user.LoginName.LastIndexOf('|') + 1));
                }
                catch (Exception)
                {
                    emails.Add(fieldUserValue.Email);
                }
            }

            return emails;
        }

        public static List<string> ToUserFieldG2(this FieldUserValue[] fieldUserValues, AppClientContext context)
        {
            var emails = new List<string>();
            if (fieldUserValues != null)
            {
                foreach (var fieldUserValue in fieldUserValues)
                {
                    emails.AddRange(ToUserFieldG2(fieldUserValue, context));
                }
                //emails.AddRange(fieldUserValues.Select(i => i.Email));  
            }

            return emails;
        }
    

    public static List<TaxonomyValue> ToTermValueG2(this TaxonomyFieldValue taxonomyFieldValue, string termSetId)
    {
        var values = new List<TaxonomyValue>();
        if (taxonomyFieldValue != null)
        {
            values.Add(new TaxonomyValue()
            {
                TermSetId = termSetId,
                TermGuid = taxonomyFieldValue.TermGuid,
                Label = taxonomyFieldValue.Label,
                WssId = taxonomyFieldValue.WssId
            });
        }

        return values;
    }

    public static List<TaxonomyValue> ToTermValueG2(this TaxonomyFieldValueCollection taxonomyFieldValues, string termSetId)
    {
        var values = new List<TaxonomyValue>();
        if (taxonomyFieldValues != null)
        {
            values.AddRange(taxonomyFieldValues.Select(i => new TaxonomyValue()
            {
                TermSetId = termSetId,
                TermGuid = i.TermGuid,
                Label = i.Label,
                WssId = i.WssId
            }));
        }

        return values;
    }
}
}
