using Omnia.MigrationG2.Intranet.Models.ContentManagement;
using Omnia.MigrationG2.Intranet.Models.Comments;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Omnia.MigrationG2.Intranet.Models.Likes;

namespace Omnia.MigrationG2.Intranet.Models.News
{
    public class NewsItem
    {
        public string UniqueId { get; set; }
        public int Id { get; set; }
        public int DefaultPageId { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public string PageLanguageLocale { get; set; }
        public List<PageLanguage> PageTargetLanguage { get; set; }
        public PageLanguage PageSourceLanguage { get; set; }
        public string NewsCenterUrl { get; set; }
        public string SiteCollectionRelativeUrl { get; set; }
        public List<LikeItem> Likes { get; set; }
        public List<NewsItem> TranslationPages { get; set; }
        public Guid ListId { get; set; }
        public List<CommentItem> Comments { get; set; }
        // MigrationG2 props
        public PageInfo PageInfo { get; set; }
        public PageInfoPropertiesSettings PageInfoPropertiesSettings { get; set; }
    }
}
