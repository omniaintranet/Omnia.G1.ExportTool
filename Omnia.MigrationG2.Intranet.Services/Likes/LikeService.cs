using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Omnia.MigrationG2.Foundation.Core.Configurations;
using Omnia.MigrationG2.Foundation.Core.Services;
using Omnia.MigrationG2.Intranet.Models.Comments;
using Omnia.MigrationG2.Intranet.Models.Likes;
using Omnia.MigrationG2.Intranet.Repositories.Likes;

namespace Omnia.MigrationG2.Intranet.Services.Likes
{
    public class LikeService : ClientContextService, ILikeService
    {
        private readonly ILikesRepository _likesRepository;
        private readonly IConfigurationService _configurationService;

        public LikeService(
            ILikesRepository likesRepository,
            IConfigurationService configurationService)
        {
            _likesRepository = likesRepository;
            _configurationService = configurationService;
        }
        public List<LikeItem> GetByPageId(int pageId, string webUrl)
        {
            try
            {
                var likesFromDb = _likesRepository.GetByPageId(pageId, webUrl);
                return likesFromDb;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public List<LikeItem> GetByCommentId(Guid commentId)
        {
            try
            {
                var likesFromDb = _likesRepository.GetByCommentId(commentId);
                return likesFromDb;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        private void AddRootComment(Models.Comments.CommentItem currentComment,
            List<Models.Comments.CommentItem> rootComment,
            Dictionary<Guid, Models.Comments.CommentItem> commentsDict,
            List<Guid> processedCommentIds,
            Dictionary<string, List<Guid>> targetDict = null)
        {
           
            if (processedCommentIds.Contains(currentComment.Id))
                return;

            processedCommentIds.Add(currentComment.Id);
            if (commentsDict.ContainsKey(currentComment.ParentId))
            {
                var parentNode = commentsDict[currentComment.ParentId];
                parentNode.Children = new List<CommentItem>();
                parentNode.Children.Add(currentComment);
                currentComment.Parent = parentNode;
                AddRootComment(parentNode, rootComment, commentsDict, processedCommentIds,
                    targetDict: targetDict);
                currentComment.Level = parentNode.Level + 1;
            }
            else
            {
                currentComment.Level = 1;
                rootComment.Add(currentComment);
            }
        }

        private List<CommentItem> BuildHierarchy(int pageId, List<CommentItem> sourceComments,
            Dictionary<Guid, CommentItem> commentDict = null,
            bool setDuplicatedTarget = false)
        {
            var retVal = new List<CommentItem>();
            if (commentDict == null)
                commentDict = sourceComments.ToDictionary(n => n.Id);

            var processedNodeIds = new List<Guid>();
            Dictionary<string, List<Guid>> targetDict = null;
            if (setDuplicatedTarget)
                targetDict = new Dictionary<string, List<Guid>>();

            foreach (var item in sourceComments)
            {
                AddRootComment(item, retVal, commentDict, processedNodeIds, targetDict: targetDict);
            }
            return sourceComments;
        }
    }
}
