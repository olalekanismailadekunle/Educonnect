using EduConnect.Contract;
using EduConnect.Identity;
using System.Collections.Generic;

namespace EduConnect.Entities
{
    public class Parent : AuditableEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Address Address { get; set; }
        public string MailAddress { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public ICollection<Student> Students { get; set; } = new List<Student>();
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    }
}
