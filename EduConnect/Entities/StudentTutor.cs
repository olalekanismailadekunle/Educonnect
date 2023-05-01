using EduConnect.Contract;

namespace EduConnect.Entities
{
    public class StudentTutor : BaseEntity
    {
        public int TutorId { get; set; }
        public Tutor Tutor { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
    }
}
