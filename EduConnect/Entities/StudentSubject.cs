using EduConnect.Contract;

namespace EduConnect.Entities
{
    public class StudentSubject : BaseEntity
    {
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
    }
}
