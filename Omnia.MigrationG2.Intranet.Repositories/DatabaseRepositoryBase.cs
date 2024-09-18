using AutoMapper;
using Microsoft.Extensions.Options;
using Omnia.MigrationG2.Core;
using Omnia.MigrationG2.Models.AppSettings;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Intranet.Repositories
{
    public class DatabaseRepositoryBase
    {
        private static Guid _tenantId;

        protected readonly OMIContext OMIContext;

        protected Guid TenantId
        {
            get
            {
                if (_tenantId == Guid.Empty)
                {
                    _tenantId = AppUtils.MigrationSettings.TenantId;
                }

                return _tenantId;
            }
        }

        public DatabaseRepositoryBase()
        {
            this.OMIContext = AppUtils.GetService<OMIContext>();
        }
    }
}
