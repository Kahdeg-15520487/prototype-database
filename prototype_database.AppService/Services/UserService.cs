using System;
using System.Linq;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

using prototype_database.Contract;
using prototype_database.Contract.DTOs;
using prototype_database.Dal;
using Newtonsoft.Json;
using prototype_database.AppService.Utility;

namespace prototype_database.AppService.Services
{
    public class UserService : IUserService
    {
        private readonly UserDbContext _context;

        public UserService(UserDbContext context)
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
                        Organization = Mapper.Map(user.Organization),
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

                user.Groups = groups.Select(group => Mapper.Map(group.Group)).ToArray();

                var mainGroup = groups.First(g => g.isMain).Group;
                user.MainGroup = Mapper.Map(mainGroup);

                var roles = from role in _context.Roles
                            join usrl in _context.UserRoles on role.Id equals usrl.RoleId
                            join us in _context.Users on usrl.UserId equals us.Id
                            select new { role, isMain = usrl.IsMain };

                user.Roles = roles.Select(role => Mapper.Map(role.role)).ToArray();

                var mainRole = roles.First(r => r.isMain).role;
                user.MainRole = Mapper.Map(mainRole);

                return user;
            });

            return users;
        }

        public ICollection<UserDTO> GetLightUsers()
        {
            return
            _context.Users
                .Select((user) =>
                    new UserDTO
                    {
                        Id = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Organization = Mapper.Map(user.Organization),
                        Email = JsonConvert.DeserializeObject<Email>(user.Email),
                        Phone = JsonConvert.DeserializeObject<Phone>(user.Phone),
                        Mobile = JsonConvert.DeserializeObject<Mobile>(user.Mobile)
                    }
                )
                .ToList()
                ;
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
                    Organization = Mapper.Map(usr.Organization),
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

            user.Groups = groups.Select(gr => Mapper.Map(gr.Group)).ToArray();

            var mainGroup = groups.First(g => g.isMain).Group;
            user.MainGroup = Mapper.Map(mainGroup);

            var roles = from role in _context.Roles
                        join usrl in _context.UserRoles on role.Id equals usrl.RoleId
                        join us in _context.Users on usrl.UserId equals us.Id
                        select new { role, isMain = usrl.IsMain };

            user.Roles = roles.Select(role => Mapper.Map(role.role)).ToArray();

            var mainRole = roles.First(r => r.isMain).role;
            user.MainRole = Mapper.Map(mainRole);

            return user;
        }

        public UserDTO GetLightUser(string id)
        {
            var users = (
                from usr in _context.Users.Include(u => u.Organization)
                where usr.Id.Equals(id)
                select new UserDTO
                {
                    Id = usr.Id,
                    FirstName = usr.FirstName,
                    LastName = usr.LastName,
                    Organization = Mapper.Map(usr.Organization),
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

            return user;
        }

        public string Create(UserDTO dto, string id)
        {
            dto.Id = id;

            if (_context.Users.FirstOrDefault(u => u.Id.Equals(id)) != null)
            {
                throw new ArgumentException($"{id} already existed");
            }

            var orgid = Guid.Parse(dto.Organization.Id);

            //check org exist
            Organization org = _context.Organizations.FirstOrDefault(o => o.Id.Equals(orgid));
            if (org == null)
            {
                throw new ArgumentException($"organization {orgid} does not exist");
            }

            //check all group exist
            foreach (var groupDto in dto.Groups)
            {
                if (_context.Groups.FirstOrDefault(gr => groupDto.Id.Equals(gr.Id.ToString())) == null)
                {
                    throw new ArgumentException($"group {groupDto.Id} does not exist");
                }
            }

            //check all role exist
            foreach (var roleDto in dto.Roles)
            {
                if (_context.Roles.FirstOrDefault(rl => roleDto.Id.Equals(rl.Id.ToString())) == null)
                {
                    throw new ArgumentException($"role {roleDto.Id} does not exist");
                }
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
                where dto.Roles.FirstOrDefault(rDto => rDto.Id.Equals(rl.Id.ToString())) != null
                select new UserRole()
                {
                    Id = Guid.NewGuid(),
                    RoleId = rl.Id,
                    UserId = id,
                    IsMain = dto.MainRole.Id.Equals(rl.Id.ToString())
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