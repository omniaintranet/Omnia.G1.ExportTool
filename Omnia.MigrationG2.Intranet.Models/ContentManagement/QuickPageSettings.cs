using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Intranet.Models.ContentManagement
{
    public class QuickPageSettings
    {
        public EnableQuickPageOptions EnableSetting { get; set; }
    }

    public enum EnableQuickPageOptions
    {
        DefaultSupportLegacyWebpart = 0,
        DefaultNotSupportLegacyWebpart = 1,
        Disable = 2
    }
}
