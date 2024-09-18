using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Microsoft.Extensions.Options;
using Omnia.MigrationG2.Core;
using Omnia.MigrationG2.Foundation.Core.Services;
using Omnia.MigrationG2.Foundation.Core.Taxonomy;
using Omnia.MigrationG2.Intranet.Models.Navigations;

namespace Omnia.MigrationG2.Intranet.Services.Navigations
{
    public class NavigationTermService : ClientContextService, INavigationTermService
    {
        public IEnumerable<NavigationLanguage> GetNavigationLanguages()
        {
            var taxonomyService = new TaxonomyService(Ctx);

            int defaultLanguage;
            var languageResult = taxonomyService.GetTermLanguages(out defaultLanguage);
            var languages = new List<NavigationLanguage>();
            foreach (var lcid in languageResult)
            {
                var cultureInfo = new CultureInfo(lcid);
                var language = new NavigationLanguage()
                {
                    Name = cultureInfo.Name,
                    DisplayName = cultureInfo.DisplayName,
                    LCID = lcid
                };
                var countryInfoIndex = language.DisplayName.IndexOf("(");
                if (countryInfoIndex > 0)
                    language.DisplayName = language.DisplayName.Substring(0, countryInfoIndex).Trim();
                if (lcid == defaultLanguage)
                {
                    language.IsDefault = true;
                }
                languages.Add(language);
            }
            return languages;
        }
    }
}
