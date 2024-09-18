using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Microsoft.Extensions.Options;
using Omnia.MigrationG2.Core;
using Omnia.MigrationG2.Intranet.Models.ImportantAnnouncements;

namespace Omnia.MigrationG2.Intranet.Repositories.ImportantAnnouncements
{
    public class AnnouncementsRepository : DatabaseRepositoryBase, IAnnouncementsRepository
    {
        public List<Announcements> GetAnnouncements()
        {
            try
            {
                var data = (from announcements in OMIContext.Announcements
                            join status in OMIContext.AnnouncementStatusTypes on announcements.AnnouncementStatusTypeId equals status.Id
                            join type in OMIContext.AnnouncementTypes on announcements.AnnouncementTypeId equals type.Id
                            where announcements.TenantId == TenantId && announcements.DeletedAt == null
                            select new Entities.ImportantAnnouncements.Announcements
                            {
                                Id = announcements.Id,
                                Title = announcements.Title,
                                Description = announcements.Description,
                                Status = announcements.Status,
                                StartDate = announcements.StartDate,
                                EndDate = announcements.EndDate,
                                Order = announcements.Order,
                                IsCloseDisabled = announcements.IsCloseDisabled,
                                ForceToRedisplay = announcements.ForceToRedisplay,
                                IsCommentsDisabled = announcements.IsCommentsDisabled,
                                AnnouncementStatusTypes = status,
                                AnnouncementTypes = type,
                                CreatedBy = announcements.CreatedBy,
                                CreatedAt = announcements.CreatedAt,
                                UpdatedBy = announcements.UpdatedBy,
                                ModifiedAt = announcements.ModifiedAt
                            }).ToList();
                var items = Mapper.Map<List<Announcements>>(data);
                return items;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
