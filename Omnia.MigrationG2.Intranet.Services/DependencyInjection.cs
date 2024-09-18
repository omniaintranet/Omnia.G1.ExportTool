using Omnia.MigrationG2.Intranet.Services.Comments;
using Omnia.MigrationG2.Intranet.Services.CommonLinks;
using Omnia.MigrationG2.Intranet.Services.PersonalLinks;
using Omnia.MigrationG2.Intranet.Services.ContentManagement;
using Omnia.MigrationG2.Intranet.Services.Navigations;
using Omnia.MigrationG2.Intranet.Services.News;
using Omnia.MigrationG2.Intranet.Services.Banner;
using Omnia.MigrationG2.Intranet.Services.ImportantAnnouncements;
using System;
using Omnia.MigrationG2.Intranet.Services.SearchProperties;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class OMIServicesDependencyInjection
    {
        public static void UseOmniaIntranetServices(this IServiceCollection services)
        {
            services.AddScoped<INavigationDBService, NavigationDBService>();
            services.AddScoped<INavigationTermService, NavigationTermService>();
            services.AddScoped<INavigationCacheService, NavigationCacheService>();
            services.AddScoped<INavigationService, NavigationService>();
            services.AddScoped<IContentManagementService, ContentManagementService>();
            services.AddScoped<INewsService, NewsService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<ICommonLinksService, CommonLinksService>();
            services.AddScoped<IMyLinksService, MyLinksService>();
            services.AddScoped<ILinkedInformationService, LinkedInformationService>();
            services.AddScoped<IAnnouncementsService, AnnouncementsService>();
            services.AddScoped<ISearchPropertyService, SearchPropertyService>();
        }
    }
}
