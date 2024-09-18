using Omnia.MigrationG2.Collector.Models.G1;
using Omnia.MigrationG2.Collector.Models.G2;
using Omnia.MigrationG2.Intranet.Models.Comments;
using Omnia.MigrationG2.Intranet.Models.Likes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Collector.Models.MigrationItems
{
    public class PageNavigationMigrationItem : NavigationMigrationItem
    {
        public PageData PageData { get; set; }

        public string MainContent { get; set; }

        public string UrlSegment { get; set; }
        public string TargetUrl { get; set; }

        public Guid? GlueLayoutId { get; set; }

        public List<RelatedLink> RelatedLinks { get; set; }

        public List<CommentItem> Comments { get; set; }

        public List<LikeItem> Likes { get; set; }

        public List<PageNavigationMigrationItem> TranslationPages { get; set; }

        public List<GluePageControl> BlockSettings { get; set; }

        public string PageLanguage { get; set; }

        public string CreatedAt { get; set; }

        public string CreatedBy { get; set; }

        public string ModifiedAt { get; set; }

        public string ModifiedBy { get; set; }

        public PageNavigationMigrationItem()
        {
            BlockSettings = new List<GluePageControl>();
            TranslationPages = new List<PageNavigationMigrationItem>();
            Comments = new List<CommentItem>();
            Likes = new List<LikeItem>();
            RelatedLinks = new List<RelatedLink>();
        }
    }
}
