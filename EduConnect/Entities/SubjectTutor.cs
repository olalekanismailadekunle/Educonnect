using EduConnect.Contract;

namespace EduConnect.Entities
{
    public class SubjectTutor : BaseEntity
    {

        public int TutorId { get; set; }
        public Tutor Tutor { get; set; }
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }

    }
}
