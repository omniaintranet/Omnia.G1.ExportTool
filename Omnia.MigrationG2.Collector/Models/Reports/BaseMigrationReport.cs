using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Omnia.MigrationG2.Collector.Models
{
    public abstract class BaseMigrationReport
    {
        public DateTime StartedAt { get; protected set; }

        public DateTime FinishedAt { get; protected set; }

        public double DurationInMinutes { get; protected set; }

        public string Customer { get; protected set; }

        public string UrlSegment { get; protected set; }

        public abstract string ReportName { get; }

        public virtual void Init(string site)
        {
            Customer = site;

            StartedAt = DateTime.Now;
            DurationInMinutes = 0;
        }

        protected virtual string GetReportFileName()
        {
            return $"Report.{ReportName}.{Customer}.{UrlSegment}.{DateTime.Now.ToString("yyyy-MM-dd--HH-mm-ss")}.json";
        }

        public virtual void ExportTo(string path)
        {
            FinishedAt = DateTime.Now;
            DurationInMinutes = (FinishedAt - StartedAt).TotalMinutes;
            Directory.CreateDirectory(path);

            string filePath = Path.Combine(path, GetReportFileName());
            File.WriteAllText(filePath, JsonConvert.SerializeObject(this, Formatting.Indented));
        }
    }
}
