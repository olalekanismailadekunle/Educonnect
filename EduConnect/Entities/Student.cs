using EduConnect.Contract;
using EduConnect.Enums;
using System.Collections.Generic;

namespace EduConnect.Entities
{
    public class Student : AuditableEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Level Level { get; set; }
        public int ParentId { get; set; }
        public Parent Parent { get; set; }
        public Address Address { get; set; }
        public Deleted Deleted { get; set; }
        public ICollection<StudentTutor> Tutors { get; set; } = new List<StudentTutor>();
        public ICollection<StudentSubject> Subjects { get; set; } = new HashSet<StudentSubject>();
    }
}
