using Microsoft.Extensions.Configuration;
using Omnia.MigrationG2.Models.AppSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security;
using System.Text;

namespace Omnia.MigrationG2.Core
{
    public static class AppUtils
    {
        private static IServiceProvider _serviceProvider;

        private static IConfiguration _configuration;

        private static OmniaSettings _omniaSettings;

        private static MigrationSettings _migrationSettings;

        private static UserSettings _userSettings;

        private static HttpClientSettings _httpClientSettings;

        public static OmniaSettings OmniaSettings
        {
            get
            {
                if (_omniaSettings == null)
                {
                    _omniaSettings = _configuration.GetSection(Constants.AppSettings.OmniaSettingsSection).Get<OmniaSettings>();
                }

                return _omniaSettings;
            }
        }

        public static UserSettings UserSettings
        {
            get {
                if(_userSettings == null)
                {
                    _userSettings = _configuration.GetSection(Constants.AppSettings.UserSettingsSection).Get<UserSettings>();
                }

                return _userSettings;
            }
        }

        public static MigrationSettings MigrationSettings
        {
            get
            {
                if (_migrationSettings == null)
                {
                    _migrationSettings = _configuration.GetSection(Constants.AppSettings.MigrationSettingsSection).Get<MigrationSettings>();
                }

                return _migrationSettings;
            }
        }

        public static HttpClientSettings httpClientSettings
        {
            get
            {
                if (_httpClientSettings == null)
                {
                    try
                    {
                        _httpClientSettings = _configuration.GetSection(Constants.AppSettings.HttpClientSettingsSection).Get<HttpClientSettings>();
                    }catch(Exception e)
                    {
                        return null;
                    }
                }

                return _httpClientSettings;
            }
        }

        public static void RegisterServiceProvider(IServiceProvider serviceProvider)
        {
            if (_serviceProvider == null)
            {
                _serviceProvider = serviceProvider;
            }
        }

        public static void RegisterConfiguration(IConfiguration configuration)
        {
            if (_configuration == null)
            {
                _configuration = configuration;
            }
        }

        public static T GetService<T>()
        {
            return (T)_serviceProvider.GetService(typeof(T));
        }
    }
}
