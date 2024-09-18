using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Collector.Models.G2
{
    public class RelatedLink
    {
        public string Icon { get; set; } // null, G2 will handle

        public int Index { get; set; }

        public string LinkType { get; set; }

        public string Title { get; set; }

        public string Url { get; set; }

        public bool OpenInNewWindow { get; set; }
    }

    public static class RelatedLinkTypes
    {
        public const string Heading = "wcm-link-heading";

        public const string CustomLink = "wcm-custom-link";

        public const string PageLink = "wcm-link-page";
    }
}
