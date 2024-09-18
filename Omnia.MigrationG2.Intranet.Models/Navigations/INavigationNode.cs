using Omnia.MigrationG2.Intranet.Models.Comments;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Intranet.Models.Navigations
{
    public interface INavigationNode<T>
    {
        Guid Id { get; set; }
        bool? SupportLegacyWebpart { get; set; }
        ICollection<NavigationLabel> Labels { get; set; }
        Guid ParentId { get; set; }
        string TargetUrl { get; set; }
        string SiteUrl { get; set; }
        List<T> Children { get; set; }
        string HoverText { get; set; }
        bool IsDraftNode { get; set; }
        string OwnerNavigationTerm { get; set; }
        string CustomLinkUrl { get; set; }
        string Url { get; set; }
        string BackupFriendlyUrl { get; set; }
        string CustomSortOrder { get; set; }
        string FriendlyUrlSegment { get; set; }
        bool IsDuplicateTargetUrl { get; set; }
        DateTime LastModifiedDate { get; set; }
        bool ForceChildrenUseDelayUrl { get; set; }
        Guid TermStoreId { get; set; }
        Guid TermSetId { get; set; }
        DateTimeOffset CreatedAt { get; set; }
        List<NavigationNode> TranslationPages { get; set; }
        List<CommentItem> Comments { get; set; }
        Guid ListId { get; set; }
    }
}
