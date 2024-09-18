using Omnia.MigrationG2.Foundation.Models.Shared;
using Omnia.MigrationG2.Intranet.Models.Banner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Omnia.MigrationG2.Intranet.Services.Banner
{
    public interface ILinkedInformationService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<LinkedInformationContent> GetLinkedInformationContents();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<GroupLinkedInformationContent> GetGroupLinkedInformationContents();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        LinkedInformationContent GetLinkedInformationContentsByName(string templateName);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        GroupLinkedInformationContent GetGroupLinkedInformationContentsByName(string templateName);
    }
}
