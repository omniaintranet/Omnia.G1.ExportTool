using Newtonsoft.Json;
using Omnia.MigrationG2.Intranet.Models.Likes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Intranet.Models.Comments
{
    public class CommentItem : ICommentItem<CommentItem>
    {
        public Guid Id { get; set; }
        public string WebUrl { get; set; }
        public int PageId { get; set; }
        public string Content { get; set; }
        public Guid ParentId { get; set; }
        public bool IsDelete { get; set; }
        public string TopicId { get; set; }
        public List<CommentItem> Children { get; set; }
        [JsonIgnore]
        public CommentItem Parent { get; set; }
        public int Level { get; set; }
        public List<LikeItem> Likes { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset ModifiedAt { get; set; }

    }
}
