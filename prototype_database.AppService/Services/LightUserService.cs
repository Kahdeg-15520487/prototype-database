using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using prototype_database.Contract;
using prototype_database.Contract.DTOs;
using prototype_database.Dal;

namespace prototype_database.AppService.Services
{
    class LightUserService : IUserService
    {
        private readonly UserDbContext _context;

        public LightUserService(UserDbContext context)
        {
            _context = context;
        }

        public ICollection<UserDTO> GetUsers()
        {
            return
            _context.Users
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

            return user;
        }

        public string Create(UserDTO dto,string id)
        {
            throw new NotImplementedException();
        }

        public bool Delete(string id)
        {
            throw new NotImplementedException();
        }

        public string Update(UserDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
