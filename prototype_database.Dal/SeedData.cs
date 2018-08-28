using System;
using System.Collections.Generic;
using System.Text;

namespace prototype_database.Dal
{
    /// <summary>
    /// singleton for seed data
    /// </summary>
    public class SeedData
    {
        public readonly Organization RosenOrg, UITOrg;
        public readonly Group RosenTechGroup, RosenHRGroup, UITSEGroup, UITCEGroup;
        public readonly Role TechLeadRole, HRLeadRole, EngineerRole;
        public readonly User User;
        public readonly UserGroup[] UserGroups;
        public readonly UserRole[] UserRoles;

        private static SeedData instance;
        public static SeedData Instance => instance ?? (instance = new SeedData());

        private SeedData()
        {
            #region organization
            RosenOrg = new Organization()
            {
                Id = Guid.Parse("c00af6d2-5c26-44cc-8414-dbb420d0f942"),
                Name = "Rosen"
            };
            UITOrg = new Organization()
            {
                Id = Guid.Parse("a7bd1b7b-1110-4c6c-9fd6-f47a9cc7fbda"),
                Name = "UIT"
            };
            #endregion

            #region group
            RosenTechGroup = new Group()
            {
                Id = Guid.Parse("3777ec35-2393-4053-95ad-cc587d87a3e3"),
                Name = "Technical",
                OrganizationId = RosenOrg.Id
            };
            RosenHRGroup = new Group()
            {
                Id = Guid.Parse("ab2ace08-2daf-4422-9242-293025aab9f6"),
                Name = "HR",
                OrganizationId = RosenOrg.Id
            };

            UITSEGroup = new Group()
            {
                Id = Guid.Parse("abba6119-b935-4870-9c06-be6b8872fb32"),
                Name = "SoftwareEngineer",
                OrganizationId = UITOrg.Id
            };
            UITCEGroup = new Group()
            {
                Id = Guid.Parse("f90317a4-a87c-4800-8d24-8e7c5e84073e"),
                Name = "ComputerEngineer",
                OrganizationId = UITOrg.Id
            };
            #endregion

            #region role

            TechLeadRole = new Role
            {
                Id = Guid.Parse("fa83781c-c13e-4b2a-a13b-cc557cfba720"),
                Name = "Technical Lead"
            };
            EngineerRole = new Role
            {
                Id = Guid.Parse("d1eb257f-9a58-4751-8a6d-a1f0ed91b3ba"),
                Name = "Engineer"
            };
            HRLeadRole = new Role
            {
                Id = Guid.Parse("77817bb6-2a22-4635-8dda-b820356ed8f9"),
                Name = "HR Lead"
            };

            #endregion

            #region user
            User = new User()
            {
                Id = "12345",
                FirstName = "Minh",
                LastName = "Nguyen Le",
                OrganizationId = RosenOrg.Id,
                ProfileImage = "image",
                Email = "{\"main\": \"em@email.com\",\"emails\": [\"em@email.com\",\"em@yahoo.com\"]}",
                Phone = "{\"main\": \"1234\",\"work\": [\"1234\",\"5678\"], \"private\": [\"91011\"]}",
                Mobile = "{\"main\": \"333444\",\"mobiles\": [\"333444\",\"555666\"]}"
            };

            UserGroups = new UserGroup[]
            {
                new UserGroup()
                {
                     Id = Guid.Parse("540ab5fc-4615-4cde-a284-cc9d9698a238"),
                      UserId = User.Id,
                      GroupId = RosenTechGroup.Id,
                      IsMain = true
                },
                new UserGroup()
                {
                    Id = Guid.Parse("a2bc5162-d036-436e-9ac2-ab4571ec0694"),
                    UserId = User.Id,
                    GroupId = RosenHRGroup.Id,
                    IsMain = false
                }
            };
            UserRoles = new UserRole[]
            {
                new UserRole()
                {
                    Id = Guid.Parse("d0bd5e70-f6b3-4671-b587-0a87995daf84"),
                    UserId = User.Id,
                    RoleId=EngineerRole.Id,
                    IsMain = true
                }
            };

            #endregion
        }
    }
}
