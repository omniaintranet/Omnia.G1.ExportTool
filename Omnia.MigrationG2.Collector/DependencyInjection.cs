using Omnia.MigrationG2.Collector.CommonLinks;
using Omnia.MigrationG2.Collector.ImportantAnnouncements;
using Omnia.MigrationG2.Collector.MigrationMapper;
using Omnia.MigrationG2.Collector.MyLinks;
using Omnia.MigrationG2.Collector.Navigations;
using Omnia.MigrationG2.Collector.Banners;
using Omnia.MigrationG2.Collector.News;
using System;
using System.Collections.Generic;
using System.Text;
using Omnia.MigrationG2.Collector.Summary;
using Omnia.MigrationG2.Collector.Http;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class CollectorDependencyInjection
    {
        public static void UseCollector(this IServiceCollection services)
        {
            services.AddScoped<INavigationNodesCollector, NavigationNodesCollector>();
            services.AddScoped<INewsCollector, NewsCollector>();
            services.AddScoped<IMigrationMapper, MigrationMapper>();
            services.AddScoped<ICommonLinksCollector, CommonLinksCollector>();
            services.AddScoped<IReusableBannersCollector, ReusableBannersCollector>();
            services.AddScoped<IImportantAnnouncementsCollector, ImportantAnnouncementsCollector>();
            services.AddScoped<IMyLinksCollector, MyLinksCollector>();
            services.AddScoped<ISummaryDataCollector, SummaryDataCollector>();

        }

        public static void useCollectorHttpClient(this IServiceCollection services)
        {
            services.AddTransient<ODMHttpClient>();
        }
    }
}
