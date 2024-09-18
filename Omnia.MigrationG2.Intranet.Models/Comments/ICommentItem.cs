using Omnia.MigrationG2.Intranet.Models.Likes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Intranet.Models.Comments
{
    public interface ICommentItem<T>
    {
        Guid Id { get; set; }
        string WebUrl { get; set; }
        int PageId { get; set; }
        string Content { get; set; }
        Guid ParentId { get; set; }
        bool IsDelete { get; set; }
        string TopicId { get; set; }
        List<T> Children { get; set; }
        DateTimeOffset CreatedAt { get; set; }
        List<LikeItem> Likes { get; set; }
    }
}
