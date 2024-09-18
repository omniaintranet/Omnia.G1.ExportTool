using Newtonsoft.Json;
using Omnia.MigrationG2.Intranet.Models.ContentManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Intranet.Models.Likes
{
    public class LikeItem
    {
        public string WebUrl { get; set; }
        public int PageId { get; set; }
        public string CommentId { get; set; }
        public string TopicId { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset ModifiedAt { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
    }
}
