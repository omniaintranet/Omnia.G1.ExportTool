using Newtonsoft.Json;
using Omnia.MigrationG2.Collector.Models.G1;
using Omnia.MigrationG2.Collector.Models.G2;
using Omnia.MigrationG2.Intranet.Models.ContentManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace Omnia.MigrationG2.Collector.Helpers
{
    public static class RelatedLinkHelper
    {
        public static List<RelatedLink> InitRelatedLink(PageInfo pageInfo, string spURL=null, ListDictionary listPages=null)
        {
            List<RelatedLink> lists = new List<RelatedLink>();

            var pageCustomData = JsonConvert.DeserializeObject<PageCustomData>(pageInfo.CustomData);
            var length = pageCustomData.Links.Count;
            for (int i = 0; i < length; i++)
            {
                var relatedLink = new RelatedLink();
                relatedLink.Icon = null;
                relatedLink.Index = i;
                relatedLink.LinkType = GetLinkType(pageCustomData.Links[i]);
                relatedLink.Title = pageCustomData.Links[i].Title;
                relatedLink.Url = pageCustomData.Links[i].Url;
                relatedLink.OpenInNewWindow = pageCustomData.Links[i].OpenNewWindow ?? false;

                lists.Add(relatedLink);
            }

            return lists;
        }

        public static string GetLinkType(LinkItem item)
        {
            if (item.LinkType == LinkTypes.Heading)
            {
                return RelatedLinkTypes.Heading;
            }
            else if (item.LinkType == LinkTypes.Custom || item.LinkType == LinkTypes.Document)
            {
                return RelatedLinkTypes.CustomLink;
            }
            else if (item.LinkType == LinkTypes.Page)
            {
                return RelatedLinkTypes.PageLink;
            }

            return null;
        }
        //public static string GetFriendlyLink(string url, string spURL, ListDictionary listPages)
        //{
        //    foreach (DictionaryEntry page in listPages)
        //    {
        //        if (url.ToLower().Contains(".aspx") && url.ToLower().StartsWith(spURL))
        //        {
        //            var parts = url.Split(spURL);
        //            url = parts.Last();
        //        }
        //        if (url == page.Key.ToString())
        //        {
        //            url = page.Value.ToString();
        //            break;
        //        }
        //    }
        //    return url;
        //}
    }
}
