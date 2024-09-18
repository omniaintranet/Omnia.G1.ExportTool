using Microsoft.EntityFrameworkCore;
using Omnia.MigrationG2.Foundation.Repositories;
using Omnia.MigrationG2.Foundation.Repositories.Configurations;
using Omnia.MigrationG2.Foundation.Repositories.Features;
using Omnia.MigrationG2.Foundation.Repositories.OmniaProfiles;
using Omnia.MigrationG2.Foundation.Repositories.Tenants;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class OMFRepositoryDependencyInjection
    {
        public static void UseOmniaFoundationRepositories(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<OMFContext>(options =>
            {
                options.UseSqlServer(connectionString);
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            services.AddScoped<ITenantCloudRepository, TenantCloudRepository>();
            services.AddScoped<IOmniaProfileRepository, OmniaProfileRepository>();
            services.AddScoped<IConfigurationsRepository, ConfigurationsRepository>();
            services.AddScoped<IFeatureRepository, FeatureRepository>();
        }
    }
}