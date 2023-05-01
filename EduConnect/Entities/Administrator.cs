using EduConnect.Contract;
using EduConnect.Identity;

namespace EduConnect.Entities
{
    public class Administrator : AuditableEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string MailAddress { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
    }
}
