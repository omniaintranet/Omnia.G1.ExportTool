using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Foundation.Repositories
{
    public class OMFAutoMapperBindings : Profile
    {
        public OMFAutoMapperBindings()
        {
            BindTenantMapping();
            BindOmniaProfileMapping();
            BindConfigurationMapping();
            BindFeatureMapping();
        }

        private void BindTenantMapping()
        {
            CreateMap<Entities.Tenants.Tenant, Models.Tenants.Tenant>();
        }

        private void BindOmniaProfileMapping()
        {
            CreateMap<Entities.OmniaProfiles.OmniaProfile, Models.OmniaProfiles.OmniaProfile>()
                .ForMember(d => d.Data, opt => opt.MapFrom<OmniaProfiles.ToOmniaProfileInfoResolver>());
        }

        private void BindConfigurationMapping()
        {
            CreateMap<Entities.Configurations.Configuration, Models.Configurations.Configuration>();
        }

        private void BindFeatureMapping()
        {
            CreateMap<Entities.Features.FeatureInstance, Models.Features.FeatureInstance>();
        }
    }
}
