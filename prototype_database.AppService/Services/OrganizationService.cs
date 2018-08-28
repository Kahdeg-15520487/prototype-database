using System;
using System.Collections.Generic;
using System.Linq;

using prototype_database.Contract;
using prototype_database.Contract.DTOs;
using prototype_database.Dal;
using prototype_database.AppService.Utility;

namespace prototype_database.AppService.Services
{
    public class OrganizationService : IOrganizationService
    {
        private readonly UserDbContext _context;

        public OrganizationService(UserDbContext context)
        {
            _context = context;
        }

        public OrganizationDTO GetOrganization(Guid organizationId)
        {
            var org = _context.Organizations.FirstOrDefault(o => o.Id.Equals(organizationId));

            if (org == null)
            {
                return null;
            }

            return Mapper.Map(org);
        }

        public IEnumerable<OrganizationDTO> GetOrganizations()
        {
            return _context.Organizations.Select(o => Mapper.Map(o));
        }
    }
}
