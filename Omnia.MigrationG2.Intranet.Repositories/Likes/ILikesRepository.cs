using Omnia.MigrationG2.Intranet.Models.Likes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Intranet.Repositories.Likes
{
    public interface ILikesRepository
    {
        List<LikeItem> GetByPageId(int pageId, string webUrl); 
        List<LikeItem> GetByCommentId(Guid commentId);
    }
}
