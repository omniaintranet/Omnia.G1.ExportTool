using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Microsoft.Extensions.Options;
using Omnia.MigrationG2.Core;
using Omnia.MigrationG2.Intranet.Models.Comments;

namespace Omnia.MigrationG2.Intranet.Repositories.Comments
{
    public class CommentsRepository : DatabaseRepositoryBase, ICommentsRepository
    {
        public List<CommentItem> GetByPageId(int pageId, string webUrl)
        {
            try
            {
                var items = OMIContext
                    .Comments
                    .Where(item => item.TenantId == TenantId && !item.isDelete && item.PageId == pageId && item.WebUrl == webUrl)                       
                    .Select(Mapper.Map<CommentItem>)
                    .ToList();

                return items;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<CommentItem> GetByTopicId(string topicId)
        {
            try
            {
                var items = OMIContext
                    .Comments
                    .Where(item => item.TenantId == TenantId && !item.isDelete && item.TopicId != null && item.TopicId.ToString() == topicId)
                    .Select(Mapper.Map<CommentItem>)
                    .Distinct()
                    .ToList();

                return items;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
