using Omnia.MigrationG2.Foundation.Models.Features;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Foundation.Repositories.Features
{
    public interface IFeatureRepository
    {
        /// <summary>
        /// Gets the feature instances.
        /// </summary>
        /// <param name="featureId">The feature identifier.</param>
        /// <returns></returns>
        IEnumerable<FeatureInstance> GetFeatureInstances(Guid featureId);
    }
}
