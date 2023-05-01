using EduConnect.Contract;

namespace EduConnect.Identity
{
    public class UserRole : AuditableEntity
    {
        public User User { get; set; }
        public int UserId { get; set; }
        public Role Role { get; set; }
        public int RoleId { get; set; }
    }
}
