using Omnia.MigrationG2.Foundation.Models.OmniaProfiles;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Foundation.Repositories.OmniaProfiles
{
    public interface IOmniaProfileRepository
    {
        /// <summary>
        /// Get all valid Omnia Profiles
        /// </summary>
        /// <returns></returns>
        IList<OmniaProfile> GetAll();
    }
}
