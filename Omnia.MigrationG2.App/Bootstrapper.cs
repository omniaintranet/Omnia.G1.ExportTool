using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Omnia.MigrationG2.Core;
using Omnia.MigrationG2.Foundation.Repositories;
using Omnia.MigrationG2.Intranet.Repositories;
using Omnia.MigrationG2.Models.AppSettings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Omnia.MigrationG2.App
{
    public static class Bootstrapper
    {
        private static IConfiguration _configuration;

        private static IServiceProvider _serviceProvider;

        public static IConfiguration Configuration
        {
            get
            {
                if (_configuration == null)
                {
                    _configuration = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile(Constants.AppSettingsFile)
                        .Build();
                }

                return _configuration;
            }
        }

        public static IServiceProvider ServiceProvider
        {
            get
            {
                if (_serviceProvider == null)
                {
                    InitServiceProvider();
                }

                return _serviceProvider;
            }
        }

        private static void InitServiceProvider()
        { 
            if (_serviceProvider != null)
            {
                return;
            }

            InitMapper();

            var services = new ServiceCollection();

            // Built-in
            services.UseLogger();
            services.UseHttpClient();

            // Omnia Foundation
            services.UseOmniaFoundationRepositories(GetFoundationConnectionString());
            services.UseOmniaFoundationCoreServices();

            // Omnia Intranet
            services.UseOmniaIntranetRepositories(GetIntranetConnectionString());
            services.UseOmniaIntranetServices();

            // Omnia MigrationG2
            services.UseCollector();
            services.useCollectorHttpClient();

            _serviceProvider = services.BuildServiceProvider();
        }

        private static void InitMapper()
        {
            Mapper.Initialize(config =>
            {
                config.AddProfile<OMFAutoMapperBindings>();
                config.AddProfile<OMIAutoMapperBindings>();
            });
        }

        private static string GetFoundationConnectionString()
        {
            return Configuration
                .GetSection(Constants.AppSettings.ConnectionStringsSection)
                .GetValue<string>(Constants.AppSettings.FoundationConnectionString);
        }

        private static string GetIntranetConnectionString()
        {
            return Configuration
                .GetSection(Constants.AppSettings.ConnectionStringsSection)
                .GetValue<string>(Constants.AppSettings.IntranetConnectionString);
        }
    }
}
