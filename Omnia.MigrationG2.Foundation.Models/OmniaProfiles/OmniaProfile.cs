using Omnia.MigrationG2.Foundation.Models.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Foundation.Models.OmniaProfiles
{
    public class OmniaProfile : ModelBase
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public OmniaProfileInfo Data { get; set; }
    }
}
