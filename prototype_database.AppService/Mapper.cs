using System;

using prototype_database.Contract.DTOs;
using prototype_database.Dal;

namespace prototype_database.AppService.Utility
{
    public class Mapper
    {
        public static GroupDTO Map(Group group)
        {
            return new GroupDTO()
            {
                Id = group.Id.ToString(),
                Name = group.Name
            };
        }

        public static Group Map(GroupDTO groupDTO, Guid organizationId)
        {
            return new Group()
            {
                Id = Guid.Parse(groupDTO.Id),
                Name = groupDTO.Name,
                OrganizationId = organizationId
            };
        }

        public static OrganizationDTO Map(Organization organization)
        {
            return new OrganizationDTO()
            {
                Id = organization.Id.ToString(),
                Name = organization.Name
            };
        }

        public static Organization Map(OrganizationDTO organizationDTO)
        {
            return new Organization()
            {
                Id = Guid.Parse(organizationDTO.Id),
                Name = organizationDTO.Name
            };
        }

        public static RoleDTO Map(Role role)
        {
            return new RoleDTO()
            {
                Id = role.Id.ToString(),
                Name = role.Name
            };
        }

        public static Role Map(RoleDTO roleDTO)
        {
            return new Role()
            {
                Id = Guid.Parse(roleDTO.Id),
                Name = roleDTO.Name
            };
        }
    }
}
