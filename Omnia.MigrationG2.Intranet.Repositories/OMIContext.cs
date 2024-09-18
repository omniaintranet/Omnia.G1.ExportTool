using Microsoft.EntityFrameworkCore;
using Omnia.MigrationG2.Intranet.Repositories.Entities.Navigations;
using Omnia.MigrationG2.Intranet.Repositories.Entities.Comments;
using Omnia.MigrationG2.Intranet.Repositories.Entities.Likes;
using System;
using System.Collections.Generic;
using System.Text;
using Omnia.MigrationG2.Intranet.Repositories.Entities.CommonLinks; 
using Omnia.MigrationG2.Intranet.Repositories.Entities.ImportantAnnouncements;
using Omnia.MigrationG2.Intranet.Repositories.Entities.PersonalLinks;
using Omnia.MigrationG2.Intranet.Models.SearchProperties;

namespace Omnia.MigrationG2.Intranet.Repositories
{
    public class OMIContext : DbContext
    {
        public OMIContext(DbContextOptions<OMIContext> options) : base(options)
        {

        }

        public DbSet<NavigationNode> NavigationNodes { get; set; }
        public DbSet<CommentItem> Comments { get; set; }
        public DbSet<LikeItem> Likes { get; set; }
        public DbSet<LinksItem> Links { get; set; }
        public DbSet<MyLinksItem> MyLinks { get; set; }
        public DbSet<Announcements> Announcements { get; set; }
        public DbSet<AnnouncementStatusTypes> AnnouncementStatusTypes { get; set; }
        public DbSet<AnnouncementTypes> AnnouncementTypes { get; set; }
        public DbSet<SearchProperty> SearchProperties { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MyLinksItem>()
                .HasKey(o => new { o.TenantId, o.LinkId, o.UserLoginName});
        }
    }
}
