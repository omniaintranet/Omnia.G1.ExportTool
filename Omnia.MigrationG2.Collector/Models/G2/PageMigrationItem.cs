using Omnia.MigrationG2.Collector.Models.G1;
using Omnia.MigrationG2.Intranet.Models.Comments;
using Omnia.MigrationG2.Intranet.Models.Likes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Collector.Models.G2
{
    public class PageMigrationItem : NavigationMigrationItem
    {
        public PageData PageData { get; set; }

        public List<PageMigrationItem> TranslationPages { get; set; }

        public string UrlSegment { get; set; }
        public string TargetUrl { get; set; }

        public Guid? GlueLayoutId { get; set; }

        public List<RelatedLink> RelatedLinks { get; set; }

        public Guid ListId { get; set; }

        public List<CommentItem> Comments { get; set; }

        public List<LikeItem> Likes { get; set; }

        public string PageLanguage { get; set; }

        public List<GluePageControl> BlockSettings { get; set; }

        public Guid PhysicalPageUniqueId { get; set; } 

        public PageMigrationItem()
        {
            BlockSettings = new List<GluePageControl>();
            TranslationPages = new List<PageMigrationItem>();
        }

    }
}
