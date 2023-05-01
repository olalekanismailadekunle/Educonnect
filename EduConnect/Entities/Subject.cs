using EduConnect.Contract;
using System.Collections.Generic;

namespace EduConnect.Entities
{
    public class Subject : AuditableEntity
    {
        public string Name { get; set; }
        public ICollection<SubjectTutor> SubjectTutor { get; set; } = new HashSet<SubjectTutor>();
        public ICollection<StudentSubject> StudentSubject { get; set; } = new HashSet<StudentSubject>();
    }
}
