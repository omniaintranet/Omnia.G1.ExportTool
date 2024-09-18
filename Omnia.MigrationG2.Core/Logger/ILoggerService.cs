using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Core.Logger
{
    public interface ILoggerService
    {
        List<ReportItemMessage> GetReport();

        void Info(string message);

        void Success<T>(string message, T reportItem);

        void Error(string message, Exception exception = null);

        void Error<T>(string message, T reportItem, Exception exception = null);
    }
}
