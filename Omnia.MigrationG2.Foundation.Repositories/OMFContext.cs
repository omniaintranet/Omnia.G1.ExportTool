using Microsoft.EntityFrameworkCore;
using Omnia.MigrationG2.Foundation.Repositories.Entities.Configurations;
using Omnia.MigrationG2.Foundation.Repositories.Entities.Features;
using Omnia.MigrationG2.Foundation.Repositories.Entities.OmniaProfiles;
using Omnia.MigrationG2.Foundation.Repositories.Entities.Tenants;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Foundation.Repositories
{
    public class OMFContext : DbContext
    {
        public OMFContext(DbContextOptions<OMFContext> options): base(options)
        {

        }

        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<OmniaProfile> OmniaProfiles { get; set; }
        public DbSet<Configuration> Configurations { get; set; }
        public DbSet<FeatureInstance> FeatureInstances { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Configuration>()
                .HasKey(o => new { o.TenantId, o.Name, o.Region, o.ExtensionPackageId }); 
        }
    }
}
