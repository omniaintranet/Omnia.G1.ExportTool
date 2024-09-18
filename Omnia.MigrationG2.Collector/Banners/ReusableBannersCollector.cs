using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Omnia.MigrationG2.Collector.ExtensionMethods;
using Omnia.MigrationG2.Core.Logger; 
using Omnia.MigrationG2.Intranet.Models.Banner;
using Omnia.MigrationG2.Intranet.Services.Banner;
namespace Omnia.MigrationG2.Collector.Banners
{
    public class ReusableBannersCollector : BaseCollectorService, IReusableBannersCollector
    {
        private readonly ILinkedInformationService _bannerService;
        public ReusableBannersCollector(ILinkedInformationService bannerService)
        {
            _bannerService = bannerService;
        }

        public List<GroupLinkedInformationContent> GetGroupLinkedInformationContents()
        {
            Logger.Info($"Checking commonlinks...");
            var result = _bannerService.GetGroupLinkedInformationContents();
            Logger.Info($"--------------------------------------");
            return result;
            //throw new NotImplementedException();
        }

        public List<LinkedInformationContent> GetLinkedInformationContents()
        {
            Logger.Info($"Checking commonlinks...");
            var result = _bannerService.GetLinkedInformationContents();
            Logger.Info($"--------------------------------------");
            return result;
            //throw new NotImplementedException();
        }

        public LinkedInformationContent GetLinkedInformationContentsByName(string templateName) {
            Logger.Info($"Checking Information Contents ByName...");
            var result = _bannerService.GetLinkedInformationContentsByName(templateName);
            Logger.Info($"--------------------------------------");
            return result;
        }
        public GroupLinkedInformationContent GetGroupLinkedInformationContentsByName(string templateName)
        {
            Logger.Info($"Checking Linked Information Contents ByName...");
            var result = _bannerService.GetGroupLinkedInformationContentsByName(templateName);
            Logger.Info($"--------------------------------------");
            return result;
        }

        public List<ReportItemMessage> GetReports()
        {
            return Logger.GetReport();
        } 
    }
}
