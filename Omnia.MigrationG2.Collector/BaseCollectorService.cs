using Omnia.MigrationG2.Core;
using Omnia.MigrationG2.Core.Logger;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Collector
{
    public class BaseCollectorService
    {
        private static ILoggerService _logger;

        protected ILoggerService Logger
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
    }
}
