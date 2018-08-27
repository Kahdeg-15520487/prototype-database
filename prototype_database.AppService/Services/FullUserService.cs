using System;
using System.Linq;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

using prototype_database.Contract;
using prototype_database.Contract.DTOs;
using prototype_database.Dal;
using Newtonsoft.Json;

namespace prototype_database.AppService.Services
{
    internal class FullUserService : IUserService
    {
        private readonly UserDbContext _context;

        public FullUserService(UserDbContext context)
        {
            _context = context;
        }

        public ICollection<UserDTO> GetUsers()
        {
            var users = _context.Users
                .Select((user) =>
                    new UserDTO
                    {
                        Id = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Organization = new OrganizationDTO()
                        {
                            Id = user.Organization.Id.ToString(),
                            Name = user.Organization.Name
                        },
                        Email = JsonConvert.DeserializeObject<Email>(user.Email),
                        Phone = JsonConvert.DeserializeObject<Phone>(user.Phone),
                        Mobile = JsonConvert.DeserializeObject<Mobile>(user.Mobile)
                    }
                )
                .ToList()
                ;

            users = users.ConvertAll(user =>
            {
                var groups = from gr in _context.Groups.Include(g => g.Organization)
                             join usgr in _context.UserGroups on gr.Id equals usgr.GroupId
                             join us in _context.Users on usgr.UserId equals us.Id
                             select new { Group = gr, isMain = usgr.IsMain };

                user.Groups = groups.Select(gr =>
                        new GroupDTO()
                        {
                            Id = gr.Group.Id.ToString(),
                            Name = gr.Group.Name
                        }
                        ).ToArray();

                var mainGroup = groups.First(g => g.isMain).Group;
                user.MainGroup = new GroupDTO()
                {
                    Id = mainGroup.Id.ToString(),
                    Name = mainGroup.Name
                };

                var roles = from role in _context.Roles
                            join usrl in _context.UserRoles on role.Id equals usrl.RoleId
                            join us in _context.Users on usrl.UserId equals us.Id
                            select new { role, isMain = usrl.IsMain };

                user.Roles = roles.Select((role) =>
                        new RoleDTO()
                        {
                            Id = role.role.Id.ToString(),
                            Name = role.role.Name
                        }
                        )
                        .ToArray();

                var mainRole = roles.First(r => r.isMain).role;
                user.MainRole = new RoleDTO()
                {
                    Id = mainRole.Id.ToString(),
                    Name = mainRole.Name
                };

                return user;
            });

            return users;
        }

        public UserDTO GetUser(string id)
        {
            var users = (
                from usr in _context.Users.Include(u => u.Organization)
                where usr.Id.Equals(id)
                select new UserDTO
                {
                    Id = usr.Id,
                    FirstName = usr.FirstName,
                    LastName = usr.LastName,
                    Organization = new OrganizationDTO()
                    {
                        Id = usr.Organization.Id.ToString(),
                        Name = usr.Organization.Name
                    },
                    Email = JsonConvert.DeserializeObject<Email>(usr.Email),
                    Phone = JsonConvert.DeserializeObject<Phone>(usr.Phone),
                    Mobile = JsonConvert.DeserializeObject<Mobile>(usr.Mobile)
                }
                )
                .ToList()
                ;

            if (users.Count == 0)
            {
                return null;
            }

            var user = users[0];

            var groups = from gr in _context.Groups.Include(g => g.Organization)
                         join usgr in _context.UserGroups on gr.Id equals usgr.GroupId
                         join us in _context.Users on usgr.UserId equals us.Id
                         select new { Group = gr, isMain = usgr.IsMain };

            user.Groups = groups.Select(gr =>
                    new GroupDTO()
                    {
                        Id = gr.Group.Id.ToString(),
                        Name = gr.Group.Name
                    }
                    ).ToArray();

            var mainGroup = groups.First(g => g.isMain).Group;
            user.MainGroup = new GroupDTO()
            {
                Id = mainGroup.Id.ToString(),
                Name = mainGroup.Name
            };

            var roles = from role in _context.Roles
                        join usrl in _context.UserRoles on role.Id equals usrl.RoleId
                        join us in _context.Users on usrl.UserId equals us.Id
                        select new { role, isMain = usrl.IsMain };

            user.Roles = roles.Select((role) =>
                    new RoleDTO()
                    {
                        Id = role.role.Id.ToString(),
                        Name = role.role.Name
                    }
                    )
                    .ToArray();

            var mainRole = roles.First(r => r.isMain).role;
            user.MainRole = new RoleDTO()
            {
                Id = mainRole.Id.ToString(),
                Name = mainRole.Name
            };

            return user;
        }

        public string Create(UserDTO dto)
        {
            var id = dto.Id;

            if (_context.Users.FirstOrDefault(u => u.Id.Equals(id)) != null)
            {
                return null;
            }

            var orgid = Guid.Parse(dto.Organization.Id);

            //check org exist
            Organization org = _context.Organizations.FirstOrDefault(o => o.Id.Equals(orgid));
            if (org == null)
            {
                return null;
            }

            //check all group exist
            if (!dto.Groups.All(groupDto => _context.Groups.FirstOrDefault(gr => groupDto.Id.Equals(gr.Id.ToString())) != null))
            {
                return null;
            }

            //check all role exist
            if (!dto.Roles.All(roleDto => _context.Roles.FirstOrDefault(rl => roleDto.Id.Equals(rl.Id.ToString())) != null))
            {
                return null;
            }

            var userGroups = (
                from gr in _context.Groups
                where dto.Groups.FirstOrDefault(gDto => gDto.Id.Equals(gr.Id.ToString())) != null
                select new UserGroup()
                {
                    Id = Guid.NewGuid(),
                    GroupId = gr.Id,
                    UserId = id,
                    IsMain = dto.MainGroup.Id.Equals(gr.Id.ToString())
                })
            .ToArray()
            ;

            var userRoles = (
                from rl in _context.Roles
                where dto.Groups.FirstOrDefault(gDto => gDto.Id.Equals(rl.Id.ToString())) != null
                select new UserRole()
                {
                    Id = Guid.NewGuid(),
                    RoleId = rl.Id,
                    UserId = id,
                    IsMain = dto.MainGroup.Id.Equals(rl.Id.ToString())
                })
            .ToArray()
            ;

            User user = new User()
            {
                Id = id,

                FirstName = dto.FirstName,
                LastName = dto.LastName,
                ProfileImage = dto.ProfileImage,

                OrganizationId = org.Id,

                Email = JsonConvert.SerializeObject(dto.Email),
                Phone = JsonConvert.SerializeObject(dto.Phone),
                Mobile = JsonConvert.SerializeObject(dto.Mobile),
            };

            _context.UserGroups.AddRange(userGroups);
            _context.UserRoles.AddRange(userRoles);

            _context.Users.Add(user);
            _context.SaveChanges();

            return id;
        }

        public string Update(UserDTO dto)
        {
            throw new System.NotImplementedException();
        }

        public bool Delete(string id)
        {
            throw new System.NotImplementedException();
        }
    }
}