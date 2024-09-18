using Microsoft.SharePoint.Client.Taxonomy;
using Omnia.MigrationG2.Foundation.Core.Services;
using Omnia.MigrationG2.Foundation.Core.SharePoint;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Foundation.Core.Taxonomy
{
    public class TaxonomyService : ITaxonomyService
    {
        private ExtendedClientContext _ctx;

        public ExtendedClientContext Ctx
        {
            get
            {
                return _ctx;
            }
        }

        public TaxonomyService(ExtendedClientContext ctx)
        {
            _ctx = ctx;
        }

        /// <summary>
        /// Gets the term languages.
        /// </summary>
        /// <param name="defaultLanguage">The default language.</param>
        /// <returns></returns>
        public IEnumerable<int> GetTermLanguages(out int defaultLanguage)
        {
            var taxonomySession = TaxonomySession.GetTaxonomySession(Ctx);
            taxonomySession.UpdateCache();

            var termStore = taxonomySession.GetDefaultSiteCollectionTermStore();
            Ctx.Load(termStore,
                store => store.Languages,
                store => store.DefaultLanguage);
            Ctx.ExecuteQuery();
            defaultLanguage = termStore.DefaultLanguage;
            return termStore.Languages;
        }
    }
}
