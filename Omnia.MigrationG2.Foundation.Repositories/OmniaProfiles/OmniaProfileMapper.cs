using AutoMapper;
using Newtonsoft.Json;
using Omnia.MigrationG2.Foundation.Models.OmniaProfiles;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Foundation.Repositories.OmniaProfiles
{
    public class ToOmniaProfileInfoResolver : IValueResolver<Entities.OmniaProfiles.OmniaProfile, OmniaProfile, OmniaProfileInfo>
    {
        public OmniaProfileInfo Resolve(Entities.OmniaProfiles.OmniaProfile source, OmniaProfile destination, OmniaProfileInfo member, ResolutionContext context)
        {
            if (string.IsNullOrWhiteSpace(source.Value))
                return null;

            return JsonConvert.DeserializeObject<OmniaProfileInfo>(source.Value);
        }
    }
}
