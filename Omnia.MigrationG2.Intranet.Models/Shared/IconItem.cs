using Omnia.MigrationG2.Intranet.Models.ContentManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Intranet.Models.Shared
{
    public enum IconType
    {
        Font,
        Custom
    }
    public class IconItem
    {
        public IconType? IconType { get; set; }
        public string FontValue { get; set; }
        public string CustomValue { get; set; }
        public string BackgroundColor { get; set; }
    }
}
