using Omnia.MigrationG2.Intranet.Models.PersonalLinks;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Omnia.MigrationG2.Intranet.Services.PersonalLinks
{
    public interface IMyLinksService
    {
        /// <summary>
        /// get comments
        /// </summary>
        /// <param name="newsCenterUrl"></param>
        /// <returns></returns>
        List<MyLink> GetAllPersonalLinks();
    }
}
