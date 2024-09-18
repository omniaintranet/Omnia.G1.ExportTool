using Newtonsoft.Json;
using Omnia.MigrationG2.Intranet.Models.Likes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Intranet.Models.ImportantAnnouncements
{
    public class AnnouncementStatusTypes
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int PageId { get; set; }
        public string Properties { get; set; }
    }
}
