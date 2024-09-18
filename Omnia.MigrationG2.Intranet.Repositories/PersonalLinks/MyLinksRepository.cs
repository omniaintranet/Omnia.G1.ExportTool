using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Omnia.MigrationG2.Core;
using Omnia.MigrationG2.Intranet.Models.PersonalLinks;

namespace Omnia.MigrationG2.Intranet.Repositories.PersonalLinks
{
    public class MyLinksRepository : DatabaseRepositoryBase, IMyLinksRepository
    {
        public List<MyLink> GetAllPersonalLinks()
        {
            try
            {
                
                var items = OMIContext
                    .MyLinks
                .Where(item => item.TenantId == TenantId && item.DeletedAt == null && item.UserLoginName.Contains("."))
                .Select(Mapper.Map<MyLink>)
                .ToList();

                return items;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
