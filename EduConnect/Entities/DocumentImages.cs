using EduConnect.Contract;

namespace EduConnect.Entities
{
    public class DocumentImages : AuditableEntity
    {
        public Tutor Tutor { get; set; }
        public int TutorId { get; set; }
        public string ImageName { get; set; }
        
    }
}
