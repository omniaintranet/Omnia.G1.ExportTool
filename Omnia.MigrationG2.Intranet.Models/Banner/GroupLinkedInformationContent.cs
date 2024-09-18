using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Omnia.MigrationG2.Intranet.Models.Banner
{
    public class GroupLinkedInformationContent
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public List<string> LinkedInformationNames { get; set; }
    }
}
