using AutoMapper;
using Newtonsoft.Json;
using Omnia.MigrationG2.Intranet.Models.Navigations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Intranet.Repositories.Navigations
{
    public class ToNavigationLabelsResolver : IValueResolver<Entities.Navigations.NavigationNode, NavigationNode, ICollection<NavigationLabel>>
    {
        public ICollection<NavigationLabel> Resolve(Entities.Navigations.NavigationNode source, NavigationNode destination, ICollection<NavigationLabel> member, ResolutionContext context)
        {
            if (string.IsNullOrWhiteSpace(source.Labels))
                return new List<NavigationLabel>();

            return JsonConvert.DeserializeObject<List<NavigationLabel>>(source.Labels);
        }
    }

    public class ToNavigationPropertiesResolver : IValueResolver<Entities.Navigations.NavigationNode, NavigationNode, NavigationProperties>
    {
        public NavigationProperties Resolve(Entities.Navigations.NavigationNode source, NavigationNode destination, NavigationProperties member, ResolutionContext context)
        {
            if (string.IsNullOrWhiteSpace(source.Properties))
                return new NavigationProperties();

            return JsonConvert.DeserializeObject<NavigationProperties>(source.Properties);
        }
    }
}
