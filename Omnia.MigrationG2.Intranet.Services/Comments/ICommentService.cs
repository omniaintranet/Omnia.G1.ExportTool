using Omnia.MigrationG2.Intranet.Models.Comments;
using Omnia.MigrationG2.Intranet.Models.Likes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Omnia.MigrationG2.Intranet.Services.Comments
{
    public interface ICommentService
    {
        /// <summary>
        /// get comments
        /// </summary>
        /// <param name="newsCenterUrl"></param>
        /// <returns></returns>
        List<CommentItem> GetComments(int pageId, string webUrl);
        List<LikeItem> GetLikeByPageId(int pageId, string webUrl);
        List<CommentItem> GetCommentsByTopicId(string topicId);
    }
}
