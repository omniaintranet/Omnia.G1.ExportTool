using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Foundation.Models.Features
{
    /// <summary>
    /// FeatureInstanceStatus
    /// </summary>
    public enum FeatureInstanceStatus
    {
        NotActivated = -1,
        Activating = 0,
        Activated = 1,
        Upgrading = 2,
        Deactivating = 3,
        Error = 4
    }
}
