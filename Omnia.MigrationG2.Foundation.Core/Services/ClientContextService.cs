using Omnia.MigrationG2.Core;
using Omnia.MigrationG2.Core.Logger;
using Omnia.MigrationG2.Foundation.Core.SharePoint;
using Omnia.MigrationG2.Foundation.Models.Shared;
using Omnia.MigrationG2.Foundation.Models.Tenants;
using Omnia.MigrationG2.Foundation.Repositories.Tenants;
using Omnia.MigrationG2.Models.AppSettings;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Foundation.Core.Services
{
    public class ClientContextService
    {
        /// <summary>
        /// The tenant
        /// </summary>
        private static Tenant _tenant;

        /// <summary>
        /// Omnia Instance Mode
        /// </summary>
        private static OmniaInstanceModes? _omniaInstanceMode = null;

        /// <summary>
        /// Omnia MigrationG2 logger
        /// </summary>
        private static ILoggerService _logger;

        /// <summary>
        /// The ClientContext
        /// </summary>
        ExtendedClientContext _ctx;

        /// <summary>
        /// The current extension ID
        /// </summary>
        Guid? extensionId;

        /// <summary>
        /// Gets the current ClientContext.
        /// </summary>
        /// <value>
        /// The ClientContext
        /// </value>
        public ExtendedClientContext Ctx
        {
            get
            {
                _ctx.IsNull(() =>
                {
                    _ctx = SharePointContextProvider.CreateClientContext(AppUtils.MigrationSettings.NavigationSourceUrl, Tenant);
                });

                return _ctx;
            }
        }

        public Tenant Tenant
        {
            get
            {
                _tenant.IsNull(() =>
                {
                    var tenantCloudRepository = AppUtils.GetService<ITenantCloudRepository>();
                    _tenant = tenantCloudRepository.GetCurrentTenant();
                });

                return _tenant;
            }
        }

        public ILoggerService Logger
        {
            get
            {
                _logger.IsNull(() =>
                {
                    _logger = AppUtils.GetService<ILoggerService>();
                });

                return _logger;
            }
        }

        /// <summary>
        /// Elevate permission to app context.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="spUrl">The sp URL.</param>
        /// <param name="tenant">The tenant.</param>
        /// <param name="language">The language.</param>
        /// <param name="func">The function.</param>
        /// <returns></returns>
        protected T Elevate<T>(string spUrl, Tenant tenant, string language, Func<ExtendedClientContext, T> func)
        {
            return SharePointContextProvider.Elevate<T>(spUrl, tenant, language, func);
        }

        /// <summary>
        /// Elevate permission to app context.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="spUrl">The sp URL.</param>
        /// <param name="tenant">The tenant.</param>
        /// <param name="func">The function.</param>
        /// <returns></returns>
        protected T Elevate<T>(string spUrl, Tenant tenant, Func<ExtendedClientContext, T> func)
        {
            return SharePointContextProvider.Elevate<T>(spUrl, tenant, func);
        }

        /// <summary>
        /// Elevate permission to app context.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="language">The language.</param>
        /// <param name="func">The function.</param>
        /// <returns></returns>
        protected T Elevate<T>(string language, Func<ExtendedClientContext, T> func)
        {
            return SharePointContextProvider.Elevate<T>(Ctx.Url, Tenant, language, func);
        }


        /// <summary>
        /// Elevate permission to app context.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func">The function.</param>
        /// <returns></returns>
        protected T Elevate<T>(Func<ExtendedClientContext, T> func)
        {
            return SharePointContextProvider.Elevate<T>(Ctx.Url, Tenant, func);
        }

        /// <summary>
        /// Elevate permission to app context.
        /// </summary>
        /// <param name="spUrl">The sp URL.</param>
        /// <param name="tenant">The tenant.</param>
        /// <param name="language">The language.</param>
        /// <param name="action">The action.</param>
        protected void Elevate(string spUrl, Tenant tenant, string language, Action<ExtendedClientContext> action)
        {
            SharePointContextProvider.Elevate(spUrl, tenant, language, action);
        }

        /// <summary>
        /// Elevate permission to app context.
        /// </summary>
        /// <param name="spUrl">The sp URL.</param>
        /// <param name="tenant">The tenant.</param>
        /// <param name="action">The action.</param>
        protected void Elevate(string spUrl, Tenant tenant, Action<ExtendedClientContext> action)
        {
            SharePointContextProvider.Elevate(spUrl, tenant, action);
        }

        /// <summary>
        /// Elevate permission to app context.
        /// </summary>
        /// <param name="language">The language.</param>
        /// <param name="action">The action.</param>
        protected void Elevate(string language, Action<ExtendedClientContext> action)
        {
            SharePointContextProvider.Elevate(Ctx.Url, Tenant, language, action);
        }

        /// <summary>
        /// Elevate permission to app context.
        /// </summary>
        /// <param name="action">The action.</param>
        protected void Elevate(Action<ExtendedClientContext> action)
        {
            SharePointContextProvider.Elevate(Ctx.Url, Tenant, action);
        }

        //public OmniaInstanceModes OmniaInstanceMode
        //{
        //    get
        //    {
        //        if (_omniaInstanceMode == null)
        //        {
        //            var configurationService = OmniaApi.WorkWith(Ctx.Omnia()).Configurations();
        //            _omniaInstanceMode = configurationService.GetOmniaInstanceMode();
        //        }

        //        return _omniaInstanceMode ?? OmniaInstanceModes.Tenant;
        //    }
        //}
    }
}
