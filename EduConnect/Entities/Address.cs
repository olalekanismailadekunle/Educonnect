using EduConnect.Contract;

namespace EduConnect.Entities
{
    public class Address : AuditableEntity
    {
        public string HouseNumber { get; set; }
        public string StreetName { get; set; }
        public string LGAOfResidence { get; set; }
        public string State { get; set; }
        public Tutor Tutor { get; set; }
        public int? TutorId { get; set; }
        public Parent Parent { get; set; }
        public int? ParentId { get; set; }
        public Student Student { get; set; }
        public int? StudentId { get; set; }

    }
}
