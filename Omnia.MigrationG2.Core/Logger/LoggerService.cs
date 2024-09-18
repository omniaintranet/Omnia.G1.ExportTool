using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omnia.MigrationG2.Core.Logger
{
    public class LoggerService : ILoggerService
    {
        private List<ReportItemMessage> _reportItems;

        public List<ReportItemMessage> ReportItems
        {
            get
            {
                if (_reportItems == null)
                {
                    _reportItems = new List<ReportItemMessage>();
                }

                return _reportItems;
            }
        }

        public List<ReportItemMessage> GetReport()
        {
            var clone = ReportItems.ToList();
            ReportItems.Clear();

            return clone;
        }

        public void Success<T>(string message, T reportItem)
        {
            Console.WriteLine(message);

            ReportItem<T> item = new ReportItem<T>()
            {
                Data = reportItem,
                Status = ReportStatus.Success
            };

            ReportItems.Add(item);
        }

        public void Error(string message, Exception exception = null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[ERROR] {message}", exception);
            Console.ForegroundColor = ConsoleColor.White;

            ReportItemMessage item = new ReportItemMessage()
            {
                Status = ReportStatus.Failed,
                ErrorMessage = message,
                Exception = exception
            };

            ReportItems.Add(item);
        }
        
        public void Error<T>(string message, T reportItem, Exception exception = null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[ERROR] {message}", exception);
            Console.ForegroundColor = ConsoleColor.White;

            ReportItem<T> item = new ReportItem<T>()
            {
                Data = reportItem,
                Status = ReportStatus.Failed,
                ErrorMessage = message,
                Exception = exception
            };

            ReportItems.Add(item);
        }

        public void Info(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
