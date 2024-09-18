using Omnia.MigrationG2.Foundation.Models.Features;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Foundation.Core.Features
{
    public interface IOmniaFeaturesService
    {
        /// <summary>
        /// Get the app feature instances
        /// </summary>        
        /// <returns></returns>
        IEnumerable<FeatureInstance> GetFeatureInstances(Guid id);
    }
}
