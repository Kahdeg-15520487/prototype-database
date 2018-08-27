namespace prototype_database.Contract.DTOs
{
    public class UserDTO
    {
        public string Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfileImage { get; set; }

        public Email Email { get; set; }
        public Phone Phone { get; set; }
        public Mobile Mobile { get; set; }

        public OrganizationDTO Organization { get; set; }

        public GroupDTO MainGroup { get; set; }
        public GroupDTO[] Groups { get; set; }

        public RoleDTO MainRole { get; set; }
        public RoleDTO[] Roles { get; set; }
    }

    public class Email
    {
        public string Main { get; set; }
        public string[] Emails { get; set; }
    }

    public class Phone
    {
        public string Main { get; set; }
        public string[] Work { get; set; }
        public string[] Private { get; set; }
    }

    public class Mobile
    {
        public string Main { get; set; }
        public string[] Mobiles { get; set; }
    }
}
