using Omnia.MigrationG2.Intranet.Models.ImportantAnnouncements;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Intranet.Repositories.ImportantAnnouncements
{
    public interface IAnnouncementsRepository
    {
        List<Announcements> GetAnnouncements(); 
    }
}
