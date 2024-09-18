using Microsoft.SharePoint.Client;
using Newtonsoft.Json;
using Omnia.MigrationG2.Foundation.Models.Configurations; 
using System;
using System.Collections.Generic;
using Omnia.MigrationG2.Core;
using Omnia.MigrationG2.Foundation.Core.Configurations;
using Omnia.MigrationG2.Foundation.Core.Publishing;
using Omnia.MigrationG2.Foundation.Core.Services;
using Omnia.MigrationG2.Foundation.Core.SharePoint;
using Omnia.MigrationG2.Foundation.Core.Utilities; 
using Omnia.MigrationG2.Intranet.Models.Banner;

namespace Omnia.MigrationG2.Intranet.Services.Banner
{
    /// <summary>
    /// 
    /// </summary>
    public class LinkedInformationService : ClientContextService, ILinkedInformationService
    {
        private const string LinkedInformationConfigurationRegion = "LinkedInformation";
        private const string GroupLinkedInformationConfigurationRegion = "GroupLinkedInformation";
        private readonly IConfigurationService _configurationService;
        public LinkedInformationService(
            IConfigurationService configurationService)
        { 
            _configurationService = configurationService; 
        }
        private string _currentConfigurationRegion;
        private string CurrentConfigurationRegion
        {
            get
            {
                if (string.IsNullOrEmpty(_currentConfigurationRegion))
                {
                    Ctx.Load(Ctx.Site, site => site.Url);
                    Ctx.ExecuteQuery();
                    _currentConfigurationRegion = LinkedInformationConfigurationRegion + ":" + Ctx.Site.Url;
                }
                return _currentConfigurationRegion;
            }
        }

        private string _currentGroupConfigurationRegion;
        private string CurrentGroupConfigurationRegion
        {
            get
            {
                if (string.IsNullOrEmpty(_currentGroupConfigurationRegion))
                {
                    Ctx.Load(Ctx.Site, site => site.Url);
                    Ctx.ExecuteQuery();
                    _currentGroupConfigurationRegion = GroupLinkedInformationConfigurationRegion + ":" + Ctx.Site.Url;
                }
                return _currentGroupConfigurationRegion;
            }
        }

        public List<LinkedInformationContent> GetLinkedInformationContents()
        {
            var config = _configurationService.GetConfigurationsInRegion(CurrentConfigurationRegion);
            return ParseConfigurationsToLinkedInformationContentList(config);
        }

        public List<GroupLinkedInformationContent> GetGroupLinkedInformationContents()
        {
            var config = _configurationService.GetConfigurationsInRegion(CurrentGroupConfigurationRegion);
            return ParseConfigurationsToGroupLinkedInformationContentList(config);
        }


        private List<LinkedInformationContent> ParseConfigurationsToLinkedInformationContentList(IEnumerable<dynamic> configurations)
        {
            var result = new List<LinkedInformationContent>();
            foreach (var configuration in configurations)
            {
                var linkedInformationContent = ParseConfigurationToLinkedInformationContent(configuration);
                result.Add(linkedInformationContent);
            }
            return result;
        }

        private List<GroupLinkedInformationContent> ParseConfigurationsToGroupLinkedInformationContentList(IEnumerable<dynamic> configurations)
        {
            var result = new List<GroupLinkedInformationContent>();
            foreach (var configuration in configurations)
            {
                var linkedInformationContent = ParseConfigurationToGroupLinkedInformationContent(configuration);
                result.Add(linkedInformationContent);
            }
            return result;
        }

        private GroupLinkedInformationContent ParseConfigurationToGroupLinkedInformationContent(Configuration configuration)
        {
            var groupLinkedInformationContent = JsonConvert.DeserializeObject<GroupLinkedInformationContent>(configuration.Value);
            groupLinkedInformationContent.Name = configuration.Name;
            return groupLinkedInformationContent;
        }

        private LinkedInformationContent ParseConfigurationToLinkedInformationContent(Configuration configuration)
        {
            var linkedInformationContent = JsonConvert.DeserializeObject<LinkedInformationContent>(configuration.Value);
            linkedInformationContent.Name = configuration.Name;
            return linkedInformationContent;
        }

        public LinkedInformationContent GetLinkedInformationContentsByName(string templateName)
        {
            var config = _configurationService.GetConfiguration(templateName,CurrentConfigurationRegion);
            return ParseConfigurationToLinkedInformationContent(config);
        }

        public GroupLinkedInformationContent GetGroupLinkedInformationContentsByName(string templateName)
        {
            var config = _configurationService.GetConfiguration(templateName, CurrentGroupConfigurationRegion);
            return ParseConfigurationToGroupLinkedInformationContent(config);
            throw new NotImplementedException();
        }
    }
}
