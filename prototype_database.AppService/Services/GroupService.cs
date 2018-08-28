using System;
using System.Collections.Generic;
using System.Linq;

using prototype_database.Contract;
using prototype_database.Contract.DTOs;
using prototype_database.Dal;
using prototype_database.AppService.Utility;

namespace prototype_database.AppService.Services
{
    public class GroupService : IGroupService
    {
        private readonly UserDbContext _context;

        public GroupService(UserDbContext context)
        {
            _context = context;
        }

        public IEnumerable<GroupDTO> GetAllGroup()
        {
            return _context.Groups.Select(g => Mapper.Map(g));
        }

        public IEnumerable<GroupDTO> GetAllGroupBelongToOrganization(Guid organizationId)
        {
            return
                from grp in _context.Groups
                where grp.OrganizationId.Equals(organizationId)
                select Mapper.Map(grp)
                ;
        }

        public GroupDTO GetGroup(Guid groupId)
        {
            var group = _context.Groups.FirstOrDefault(g => g.Id.Equals(groupId));

            if (group == null)
            {
                return null;
            }

            return Mapper.Map(group);
        }
    }
}
