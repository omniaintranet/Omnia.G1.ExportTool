using Omnia.MigrationG2.Core.Logger;
using System.Net.Http;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class CoreDependencyInjection
    {
        public static void UseLogger(this IServiceCollection services)
        {
            services.AddScoped<ILoggerService, LoggerService>();
        }

        public static void UseHttpClient(this IServiceCollection services)
        {
            services.AddHttpClient("omnia").ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                UseCookies = false
            });
        }
    }
}
