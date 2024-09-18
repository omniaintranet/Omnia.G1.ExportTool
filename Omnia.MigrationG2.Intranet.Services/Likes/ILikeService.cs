using Omnia.MigrationG2.Intranet.Models.Likes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Omnia.MigrationG2.Intranet.Services.Likes
{
    public interface ILikeService
    {
        /// <summary>
        /// get comments
        /// </summary>
        /// <param name="newsCenterUrl"></param>
        /// <returns></returns>
        List<LikeItem> GetByPageId(int pageId, string webUrl);
        List<LikeItem> GetByCommentId(Guid commentId);
    }
}
