using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Omnia.MigrationG2.Foundation.Core.Configurations;
using Omnia.MigrationG2.Foundation.Core.Services;
using Omnia.MigrationG2.Intranet.Models.ImportantAnnouncements;
using Omnia.MigrationG2.Intranet.Repositories.Comments;
using Omnia.MigrationG2.Intranet.Repositories.ImportantAnnouncements;

namespace Omnia.MigrationG2.Intranet.Services.ImportantAnnouncements
{
    public class AnnouncementsService : ClientContextService, IAnnouncementsService
    {
        private readonly IAnnouncementsRepository _announcementsRepository;
        public AnnouncementsService(IAnnouncementsRepository announcementsRepository)
        {
            _announcementsRepository = announcementsRepository;
        }
        public List<Announcements> GetAnnouncements ()
        {
            try
            {
                var announcementsFromDb = _announcementsRepository.GetAnnouncements();
                return announcementsFromDb;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
