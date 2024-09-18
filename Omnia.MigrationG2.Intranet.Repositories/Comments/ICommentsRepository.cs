using Omnia.MigrationG2.Intranet.Models.Comments;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Intranet.Repositories.Comments
{
    public interface ICommentsRepository
    {
        List<CommentItem> GetByPageId(int pageId, string webUrl);
        List<CommentItem> GetByTopicId (string topicId);
    }
}
