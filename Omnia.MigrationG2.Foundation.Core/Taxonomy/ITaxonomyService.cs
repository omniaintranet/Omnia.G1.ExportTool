using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Foundation.Core.Taxonomy
{
    public interface ITaxonomyService
    {
        /// <summary>
        /// Gets the term languages.
        /// </summary>
        /// <param name="defaultLanguage">The default language.</param>
        /// <returns></returns>
        IEnumerable<int> GetTermLanguages(out int defaultLanguage);
    }
}
