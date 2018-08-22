using System;

namespace prototype_database.Dal
{
    public class UserRole
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public Guid RoleId { get; set; }
        public Role Role { get; set; }
    }
}