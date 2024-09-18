using Omnia.MigrationG2.Foundation.Core.Caching;
using Omnia.MigrationG2.Foundation.Core.Configurations;
using Omnia.MigrationG2.Foundation.Core.Features;
using Omnia.MigrationG2.Foundation.Core.Publishing;
using Omnia.MigrationG2.Foundation.Core.Taxonomy;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class OMFCoreDependencyInjection
    {
        public static void UseOmniaFoundationCoreServices(this IServiceCollection services)
        {
            services.AddScoped<ICacheService, CacheService>();
            services.AddScoped<IConfigurationService, ConfigurationService>();
            services.AddScoped<ITaxonomyService, TaxonomyService>();
            services.AddScoped<IPublishingService, PublishingService>();
            services.AddScoped<IOmniaFeaturesService, OmniaFeaturesService>();
        }
    }
}
