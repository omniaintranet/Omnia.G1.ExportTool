using Omnia.MigrationG2.Intranet.Models.Controls;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Collector.Models.G1
{
    public class GluePageControl
    {
        public string ControlId { get; set; }
        public string InstanceId { get; set; }
        public string Scope { get; set; }
        public object Settings { get; set; }
        //settings: any;
        public string ZoneId { get; set; }
        public int ZoneIndex { get; set; }
        public bool IsStatic { get; set; }
        public string SettingsKey { get; set; }
    }
}
