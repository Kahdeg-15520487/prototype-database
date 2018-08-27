using System;
using System.Collections.Generic;
using System.Text;

using Xunit;

using prototype_database.AppService.Utility;
using prototype_database.Contract.DTOs;
using prototype_database.Dal;

namespace prototype_database.AppService.Test
{
    public class TestMapper
    {
        [Fact]
        public void TestFromGroupToGroupDTO()
        {
            var groupId = Guid.NewGuid();
            var groupName = "Test Group";
            Group group = new Group()
            {
                Id = groupId,
                Name = groupName
            };
            GroupDTO dto = Mapper.Map(group);

            Assert.Equal(dto.Id, groupId.ToString());
            Assert.Equal(dto.Name, groupName);
        }

        [Fact]
        public void TestFromGroupDTOToGroup()
        {
            var groupId = Guid.NewGuid();
            var groupName = "Test Group";
            var orgId = Guid.NewGuid();
            GroupDTO groupDto = new GroupDTO()
            {
                Id = groupId.ToString(),
                Name = groupName
            };
            Group group = Mapper.Map(groupDto, orgId);

            Assert.Equal(group.Id, groupId);
            Assert.Equal(group.Name, groupName);
            Assert.Equal(group.OrganizationId, orgId);
        }

        [Fact]
        public void TestFromOrganizationToOrganizationDTO()
        {
            var orgId = Guid.NewGuid();
            var orgName = "Test Organization";

            Organization org = new Organization()
            {
                Id = orgId,
                Name = orgName
            };

            OrganizationDTO orgDTO = Mapper.Map(org);

            Assert.Equal(orgDTO.Id, orgId.ToString());
            Assert.Equal(orgDTO.Name, orgName);
        }

        [Fact]
        public void TestFromOrganizationDTOToOrganization()
        {
            var orgId = Guid.NewGuid();
            var orgName = "Test Organization";

            OrganizationDTO orgDTO = new OrganizationDTO()
            {
                Id = orgId.ToString(),
                Name = orgName
            };

            Organization org = Mapper.Map(orgDTO);

            Assert.Equal(org.Id, orgId);
            Assert.Equal(org.Name, orgName);
        }

        [Fact]
        public void TestFromRoleToRoleDTO()
        {
            var roleId = Guid.NewGuid();
            var roleName = "Test Role";

            Role role = new Role()
            {
                Id = roleId,
                Name = roleName
            };

            RoleDTO roleDTO = Mapper.Map(role);

            Assert.Equal(roleDTO.Id, roleId.ToString());
            Assert.Equal(roleDTO.Name, roleName);
        }

        [Fact]
        public void TestFromRoleDTOToRole()
        {
            var roleId = Guid.NewGuid();
            var roleName = "Test Role";

            RoleDTO roleDTO = new RoleDTO()
            {
                Id = roleId.ToString(),
                Name = roleName
            };

            Role role = Mapper.Map(roleDTO);

            Assert.Equal(role.Id, roleId);
            Assert.Equal(role.Name, roleName);
        }
    }
}
