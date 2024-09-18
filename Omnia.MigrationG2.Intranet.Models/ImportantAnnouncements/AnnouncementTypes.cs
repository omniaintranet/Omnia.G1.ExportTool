using Newtonsoft.Json;
using Omnia.MigrationG2.Intranet.Models.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Intranet.Models.ImportantAnnouncements
{
    public class AnnouncementTypes
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int PageId { get; set; }
        public IconItem Properties { get; set; }
    }
}
