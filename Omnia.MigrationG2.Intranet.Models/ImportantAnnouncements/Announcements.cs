using Newtonsoft.Json;
using Omnia.MigrationG2.Intranet.Models.Comments;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Intranet.Models.ImportantAnnouncements
{
    public class Announcements
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public AnnouncementStatus Status { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public int Order { get; set; }
        public bool IsCloseDisabled { get; set; }
        public bool ForceToRedisplay { get; set; }
        public bool IsCommentsDisabled { get; set; }
        [JsonIgnore]
        public Guid AnnouncementTypeId { get; set; }
        [JsonIgnore]
        public Guid AnnouncementsStatusTypeId { get; set; }
        public string UpdatedBy { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset ModifiedAt { get; set; }
        public object AnnouncementStatusTypes { get; set; }
        public object AnnouncementTypes { get; set; }
        public List<CommentItem> Comments { get; set; }
    }
    public enum AnnouncementStatus
    {
        Normal = 0,
        High = 1,
    }
}
