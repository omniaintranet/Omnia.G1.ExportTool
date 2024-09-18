using Omnia.MigrationG2.Intranet.Models.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Intranet.Models.CommonLinks
{
    public class LinksItem
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Category { get; set; }
        public string Information { get; set; }
        public IconItem Icon { get; set; }
        public bool IsOpenNewWindow { get; set; }
        public string LimitAccess { get; set; }
        public bool Mandatory { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset ModifiedAt { get; set; }
    }
}
