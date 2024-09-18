using System;
using System.Collections.Generic;
using System.Text;
using Omnia.MigrationG2.Foundation.Models.Features;
using Omnia.MigrationG2.Foundation.Repositories.Features;

namespace Omnia.MigrationG2.Foundation.Core.Features
{
    public class OmniaFeaturesService : IOmniaFeaturesService
    {
        private readonly IFeatureRepository _featureRepository;

        public OmniaFeaturesService(IFeatureRepository featureRepository)
        {
            _featureRepository = featureRepository;
        }

        public IEnumerable<FeatureInstance> GetFeatureInstances(Guid id)
        {
            return _featureRepository.GetFeatureInstances(id);
        }
    }
}
