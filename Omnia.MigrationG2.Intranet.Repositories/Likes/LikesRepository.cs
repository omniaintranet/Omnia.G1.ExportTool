using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Microsoft.Extensions.Options;
using Omnia.MigrationG2.Core;
using Omnia.MigrationG2.Intranet.Models.Likes;

namespace Omnia.MigrationG2.Intranet.Repositories.Likes
{
    public class LikesRepository : DatabaseRepositoryBase, ILikesRepository
    {
        public List<LikeItem> GetByCommentId(Guid commentId)
        {
            try
            {
                var items = OMIContext
                    .Likes
                    .Where(item => item.TenantId == TenantId && item.DeletedAt == null && item.CommentId == commentId.ToString())
                    .Select(Mapper.Map<LikeItem>)
                    .ToList();

                return items;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public List<LikeItem> GetByPageId(int pageId, string webUrl)
        {
            try
            {
                var items = OMIContext
                    .Likes
                    .Where(item => item.TenantId == TenantId && item.DeletedAt == null && item.PageId == pageId && item.CommentId == "" && item.WebUrl == webUrl)
                    .Select(Mapper.Map<LikeItem>)
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
