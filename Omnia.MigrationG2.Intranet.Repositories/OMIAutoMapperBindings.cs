using AutoMapper;
using Omnia.MigrationG2.Intranet.Models.Navigations;
using Omnia.MigrationG2.Intranet.Models.Comments;
using Omnia.MigrationG2.Intranet.Models.Likes;
using Omnia.MigrationG2.Intranet.Models.CommonLinks;
using Omnia.MigrationG2.Intranet.Models.PersonalLinks;
using Omnia.MigrationG2.Intranet.Models.ImportantAnnouncements;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Omnia.MigrationG2.Intranet.Models.Shared;

namespace Omnia.MigrationG2.Intranet.Repositories
{
    public class OMIAutoMapperBindings : Profile
    {
        private const string LoginNamePrefix = "i:0#.f|membership|";
        public OMIAutoMapperBindings()
        {
            BindNavigationMapping();
            BindCommentMapping();
            BindLikeMapping();
            BindCommonLinksMapping();
            BindMyLinksMapping();
            BindAnnouncementsMapping();
        }

        private void BindNavigationMapping()
        {
            CreateMap<Entities.Navigations.NavigationNode, NavigationNode>()
                .ForMember(d => d.Labels, opt => opt.MapFrom<Navigations.ToNavigationLabelsResolver>())
                .ForMember(d => d.Properties, opt => opt.MapFrom<Navigations.ToNavigationPropertiesResolver>());

            CreateMap<Models.Navigations.NavigationNode, Models.Navigations.NavigationNode>()
                .ForMember(q => q.Parent, opt => opt.Ignore())
                .ForMember(q => q.Children, opt => opt.Ignore());
        }

        private void BindSearchPropertiesMapping()
        {
            CreateMap<Entities.SearchProperties.SearchProperty, Models.SearchProperties.SearchProperty>();
            CreateMap<Models.SearchProperties.SearchProperty, Entities.SearchProperties.SearchProperty>()
                .ForMember(q => q.CreatedAt, opt => opt.Ignore())
                .ForMember(q => q.ModifiedAt, opt => opt.Ignore())
                .ForMember(q => q.DeletedAt, opt => opt.Ignore());
        }


        private void BindCommentMapping()
        {
            CreateMap<Entities.Comments.CommentItem, CommentItem>()
                .ForMember(d => d.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy == null ?
                            null : ConvertUser(src.CreatedBy)))
                .ForMember(d => d.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedBy == null ?
                            null : ConvertUser(src.UpdatedBy)));

        }
        private void BindLikeMapping()
        {
            CreateMap<Entities.Likes.LikeItem, LikeItem>()
            .ForMember(d => d.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy == null ? 
                            null : ConvertUser(src.CreatedBy)))
            .ForMember(d => d.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedBy == null ?
                            null : ConvertUser(src.UpdatedBy)));

        }
        private void BindCommonLinksMapping()
        {
            CreateMap<Entities.CommonLinks.LinksItem, LinksItem>()
                .ForMember(d => d.Icon, opt =>opt.MapFrom(src => src.Icon != null ? JsonConvert.DeserializeObject<IconItem>(src.Icon) : null))
                .ForMember(d => d.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy == null ?
                            null : ConvertUser(src.CreatedBy)))
                .ForMember(d => d.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedBy == null ?
                            null : ConvertUser(src.UpdatedBy)));
        }
        private void BindMyLinksMapping()
        {

            CreateMap<Entities.PersonalLinks.MyLinksItem, MyLink>()
                .ForMember(d => d.Icon, opt => opt.MapFrom(src => (src != null && src.Icon != null) ? JsonConvert.DeserializeObject<IconItem>(src.Icon) : null))
                .ForMember(d => d.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy == null ?
                            null : ConvertUser(src.CreatedBy)))
                .ForMember(d => d.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedBy == null ?
                            null : ConvertUser(src.UpdatedBy)));
        }
        private void BindAnnouncementsMapping()
        {
            CreateMap<Entities.ImportantAnnouncements.Announcements, Announcements>()
                .ForMember(d => d.AnnouncementStatusTypes, opt => opt.MapFrom(src => src.AnnouncementStatusTypes))
                .ForMember(d => d.AnnouncementTypes, opt => opt.MapFrom(src => src.AnnouncementTypes))
                .ForMember(d => d.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy == null ?
                            null : ConvertUser(src.CreatedBy)))
                .ForMember(d => d.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedBy == null ?
                            null : ConvertUser(src.UpdatedBy)));

            CreateMap<Entities.ImportantAnnouncements.AnnouncementStatusTypes, AnnouncementStatusTypes>();
            CreateMap<Entities.ImportantAnnouncements.AnnouncementTypes, AnnouncementTypes>()
                .ForMember(d => d.Properties, opt => opt.MapFrom(src => JsonConvert.DeserializeObject<IconItem>(src.Properties)));
        }
        private string ConvertUser(string user)
        {
            if (user.Contains(LoginNamePrefix))
                return user.Substring(user.IndexOf(LoginNamePrefix) + LoginNamePrefix.Length);
            return user;
        }
    }
}
