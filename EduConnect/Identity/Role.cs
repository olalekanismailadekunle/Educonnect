using EduConnect.Contract;
using System.Collections.Generic;

namespace EduConnect.Identity
{
    public class Role : AuditableEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<UserRole> UserRole { get; set; } = new List<UserRole>();
    }
}
