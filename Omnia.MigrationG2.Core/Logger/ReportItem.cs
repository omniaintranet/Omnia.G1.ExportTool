using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Core.Logger
{
    public class ReportItemMessage
    {
        public ReportStatus Status { get; set; }

        public string ErrorMessage { get; set; }

        [JsonIgnore]
        public Exception Exception { get; set; }
    }

    public class ReportItem<T>: ReportItemMessage
    {
        [JsonIgnore]
        public T Data { get; set; }
    }

    public enum ReportStatus
    {
        Success,
        Failed
    }
}
