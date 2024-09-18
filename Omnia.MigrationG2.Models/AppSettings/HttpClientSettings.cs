using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Models.AppSettings
{
    public class HttpClientSettings
    {
        public string FoundationUrl { get; set; }
        public string IntranetUrl { get; set; }
        public string ODMUrl { get; set; }
        public Guid ExtensionId { get; set; }
        public string ApiSecret { get; set; }
        public string TokenKey { get; set; }
        public string SharePointUrl { get; set; }
    }
}
