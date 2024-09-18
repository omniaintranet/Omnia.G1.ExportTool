using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Omnia.MigrationG2.Collector.ExtensionMethods;
using Omnia.MigrationG2.Core.Logger;
using Omnia.MigrationG2.Intranet.Models.ImportantAnnouncements;
using Omnia.MigrationG2.Intranet.Services.Comments;
using Omnia.MigrationG2.Intranet.Services.ImportantAnnouncements;
namespace Omnia.MigrationG2.Collector.ImportantAnnouncements
{
    public class ImportantAnnouncementsCollector : BaseCollectorService, IImportantAnnouncementsCollector
    {
        private readonly IAnnouncementsService _announcementsService;
        private readonly ICommentService _commentsService;
        public ImportantAnnouncementsCollector(IAnnouncementsService announcementsService,
            ICommentService commentsService)
        {
            _announcementsService = announcementsService;
            _commentsService = commentsService;
        }

        public List<Announcements> GetAnnouncements()
        {
            Logger.Info($"Checking announcements...");

            var result = _announcementsService.GetAnnouncements();
            foreach(var item in result)
            {
                item.Comments = _commentsService.GetCommentsByTopicId(item.Id.ToString());
            }
            Logger.Info($"--------------------------------------");
            return result;
        }

        public List<ReportItemMessage> GetReports()
        {
            return Logger.GetReport();
        }
    }
}
