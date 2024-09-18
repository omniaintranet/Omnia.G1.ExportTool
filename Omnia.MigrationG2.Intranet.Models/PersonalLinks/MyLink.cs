using Omnia.MigrationG2.Intranet.Models.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Intranet.Models.PersonalLinks
{
    public class MyLink
    {
        public System.Guid TenantId { get; set; }
        public System.Guid LinkId { get; set; }
        public string UserLoginName { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Category { get; set; }
        public string Information { get; set; }
        public IconItem Icon { get; set; }
        public bool IsOpenNewWindow { get; set; }
        public bool IsOwner { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset ModifiedAt { get; set; }
        public Nullable<System.DateTimeOffset> DeletedAt { get; set; }
    }
}
