using Omnia.MigrationG2.Intranet.Models.CommonLinks;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Omnia.MigrationG2.Intranet.Services.CommonLinks
{
    public interface ICommonLinksService
    {
        /// <summary>
        /// get comments
        /// </summary>
        /// <param name="newsCenterUrl"></param>
        /// <returns></returns>
        List<LinksItem> GetCommonLinks();
    }
}
