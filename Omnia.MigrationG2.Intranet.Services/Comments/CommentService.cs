using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Omnia.MigrationG2.Foundation.Core.Configurations;
using Omnia.MigrationG2.Foundation.Core.Services;
using Omnia.MigrationG2.Intranet.Models.Comments;
using Omnia.MigrationG2.Intranet.Models.Likes;
using Omnia.MigrationG2.Intranet.Repositories.Comments;
using Omnia.MigrationG2.Intranet.Repositories.Likes;

namespace Omnia.MigrationG2.Intranet.Services.Comments
{
    public class CommentService : ClientContextService, ICommentService
    {
        private readonly ICommentsRepository _commentsRepository;
        private readonly IConfigurationService _configurationService;
        private readonly ILikesRepository _likesRepository;

        public CommentService(
            ICommentsRepository commentsRepository,
            IConfigurationService configurationService,
            ILikesRepository likesRepository)
        {
            _commentsRepository = commentsRepository;
            _configurationService = configurationService;
            _likesRepository = likesRepository;
        }
        public List<LikeItem> GetLikeByPageId(int pageId, string webUrl)
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
        public List<CommentItem> GetComments(int pageId, string webUrl)
        {
            try
            {
                var commentsFromDb = _commentsRepository.GetByPageId(pageId, webUrl);
                commentsFromDb = BuildHierarchy(commentsFromDb, setDuplicatedTarget: true);
                commentsFromDb = commentsFromDb.Where(i => i.Level == 1).ToList();
                return commentsFromDb;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public List<CommentItem> GetCommentsByTopicId(string topicId)
        {
            try
            {
                var commentsFromDb = _commentsRepository.GetByTopicId(topicId);
                commentsFromDb = BuildHierarchy(commentsFromDb, setDuplicatedTarget: true);
                commentsFromDb = commentsFromDb.Where(i => i.Level == 1).ToList();
                return commentsFromDb;
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
            if (currentComment.Children != null)
            {
                var childNode = commentsDict.Where(i => i.Value.ParentId == currentComment.Id).ToList();
                if (childNode == null) return;
                var diffChild = childNode.Where(i => !currentComment.Children.Any(c => c.Id == i.Value.Id)).Distinct().ToList();
                if (diffChild == null) return;
                foreach (var child in diffChild)
                {
                    currentComment.Children.Add(child.Value);
                }
            }
            if (processedCommentIds.Contains(currentComment.Id))
                return;

            processedCommentIds.Add(currentComment.Id);
            if (commentsDict.ContainsKey(currentComment.ParentId))
            {
                var parentNode = commentsDict[currentComment.ParentId];  //get parent

                parentNode.Children = new List<CommentItem>();  // add children of node
                parentNode.Children.Add(currentComment);
                currentComment.Parent = parentNode;

                currentComment.Likes = _likesRepository.GetByCommentId(currentComment.Id);
                AddRootComment(parentNode, rootComment, commentsDict, processedCommentIds,
                    targetDict: targetDict);
                currentComment.Level = parentNode.Level + 1;
            }
            else
            {
                currentComment.Level = 1;
                currentComment.Likes = _likesRepository.GetByCommentId(currentComment.Id);
                rootComment.Add(currentComment);
            }
        }

        private List<CommentItem> BuildHierarchy(List<CommentItem> sourceComments,
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
                var likeFromDB = _likesRepository.GetByCommentId(item.Id);

                AddRootComment(item, retVal, commentDict, processedNodeIds, targetDict: targetDict);
            }
            return sourceComments;
        }
    }
}
