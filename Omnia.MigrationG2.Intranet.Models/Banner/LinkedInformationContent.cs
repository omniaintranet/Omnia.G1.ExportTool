using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Omnia.MigrationG2.Intranet.Models.Banner
{
    public class LinkedInformationContent
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public bool IsEnabled { get; set; }
        public string ViewId { get; set; }
        public string BackgroundColor { get; set; }
        public string Title { get; set; }
        public string TitleColor { get; set; }
        public string ImageUrl { get; set; }
        public string Content { get; set; }
        public string ContentColor { get; set; }
        public string LinkUrl { get; set; }
        public string Footer { get; set; }
        public string FooterColor { get; set; }
        public bool IsOpenLinkNewWindow { get; set; }
        public Nullable<float> Opacity { get; set; }
        public object TitleSettings { get; set; }
    }
}
