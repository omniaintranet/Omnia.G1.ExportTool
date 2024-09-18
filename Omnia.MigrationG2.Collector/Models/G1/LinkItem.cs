using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Collector.Models.G1
{
    public class LinkItem
    {
        public string Title { get; set; }

        public LinkTypes LinkType { get; set; }

        public string Url { get; set; }

        public string Group { get; set; }

        public bool? OpenNewWindow { get; set; }

        public bool? OpenDocument { get; set; }

        public string UrlToShow { get; set; }
    }

    public enum LinkTypes
    {
        Page = 0,
        Document = 1,
        Custom = 2,
        Heading = 3,
        NavigationCustomLink = 4
    }
}
