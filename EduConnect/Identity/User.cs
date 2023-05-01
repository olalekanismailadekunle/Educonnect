using EduConnect.Contract;
using EduConnect.Entities;
using System.Collections.Generic;

namespace EduConnect.Identity
{
    public class User : AuditableEntity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public Administrator Administrator { get; set; }
        public Tutor Tutor { get; set; }
        public Parent Parent { get; set; } 
        public List<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}
