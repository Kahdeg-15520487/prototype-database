using System;

namespace prototype_database.Dal
{
    public class User
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfileImage { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }

        public Guid OrganizationId { get; set; }
        public Organization Organization { get; set; }
    }
}