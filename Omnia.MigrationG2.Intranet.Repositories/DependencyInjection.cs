using Microsoft.EntityFrameworkCore;
using Omnia.MigrationG2.Intranet.Repositories;
using Omnia.MigrationG2.Intranet.Repositories.Navigations;
using Omnia.MigrationG2.Intranet.Repositories.Comments;
using Omnia.MigrationG2.Intranet.Repositories.Likes;
using Omnia.MigrationG2.Intranet.Repositories.ImportantAnnouncements;
using System;
using System.Collections.Generic;
using System.Text;
using Omnia.MigrationG2.Intranet.Repositories.CommonLinks;
using Omnia.MigrationG2.Intranet.Repositories.PersonalLinks;
using Omnia.Intranet.Repositories.SearchProperties;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class OMIRepositoryDependencyInjection
    {
        public static void UseOmniaIntranetRepositories(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<OMIContext>(options =>
            {
                options.UseSqlServer(connectionString);
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            services.AddScoped<INavigationNodeRepository, NavigationNodeRepository>();
            services.AddScoped<ICommentsRepository, CommentsRepository>();
            services.AddScoped<ILikesRepository, LikesRepository>();
            services.AddScoped<ILinksRepository, LinksRepository>();
            services.AddScoped<IMyLinksRepository, MyLinksRepository>();
            services.AddScoped<IAnnouncementsRepository, AnnouncementsRepository>();
            services.AddScoped<ISearchPropertyRepository, SearchPropertyRepository>();
        }
    }
}
