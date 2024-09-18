using Omnia.MigrationG2.Intranet.Models.ImportantAnnouncements;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Omnia.MigrationG2.Intranet.Services.ImportantAnnouncements
{
    public interface IAnnouncementsService
    {
        /// <summary>
        /// get comments
        /// </summary>
        /// <param name="newsCenterUrl"></param>
        /// <returns></returns>
        List<Models.ImportantAnnouncements.Announcements> GetAnnouncements();
    }
}
