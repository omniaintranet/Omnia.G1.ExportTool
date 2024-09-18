using Omnia.MigrationG2.Core.Logger;
using Omnia.MigrationG2.Intranet.Models.PersonalLinks;
using Omnia.MigrationG2.Intranet.Models.News;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Omnia.MigrationG2.Intranet.Models.Banner;

namespace Omnia.MigrationG2.Collector.Banners
{
    public interface IReusableBannersCollector
    { 
        List<LinkedInformationContent> GetLinkedInformationContents(); 
        List<GroupLinkedInformationContent> GetGroupLinkedInformationContents();
        LinkedInformationContent GetLinkedInformationContentsByName(string templateName);
        GroupLinkedInformationContent GetGroupLinkedInformationContentsByName(string templateName);
    }
}
