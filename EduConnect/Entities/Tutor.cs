using EduConnect.Contract;
using EduConnect.Enums;
using EduConnect.Identity;
using System;
using System.Collections.Generic;


namespace EduConnect.Entities
{
    public class Tutor : AuditableEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Address Address { get; set; }
        public string City { get; set; }

  
        public string ProfilePicture { get; set; }
        public List<DocumentImages> DocumentImages { get; set; }
         public Qualification Qualification { get; set; }
        public Specialization Specialization { get; set; }
        //public Subject Subject { get; set; }
        public string MailAddress { get; set; }
        public Status Status { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public WorkingHours WorkinHoursPerday { get; set; }
        public ICollection<SubjectTutor> SubjectTutor { get; set; } = new HashSet<SubjectTutor>();
        public ICollection<StudentTutor> StudentTutor { get; set; } = new HashSet<StudentTutor>();


    }
}
