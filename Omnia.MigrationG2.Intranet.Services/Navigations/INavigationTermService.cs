using Omnia.MigrationG2.Intranet.Models.Navigations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Intranet.Services.Navigations
{
    public interface INavigationTermService
    {
        /// <summary>
        /// Gets the navigation languages.
        /// </summary>
        /// <returns></returns>
        IEnumerable<NavigationLanguage> GetNavigationLanguages();
    }
}
