using EduConnect.Contract;
using EduConnect.Enums;
using System;
using System.Collections.Generic;

namespace EduConnect.Entities
{
    public class Booking : AuditableEntity
    {
        public Guid OrderReference { get; set; } 
        public PaymentCategory PaymentCategory { get; set; }
        public int SubjectId { get; set; }
        public BookingHour BookingHour { get; set; }
        public Subject Subject { get; set; }
        public int ParentId { get; set; }
        public Parent Parent { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int TutorId { get; set; }
        public Tutor Tutor { get; set; }
        public BookingStatus BookingStatus { get; set; }
        public ICollection<Payment> Payments { get; set; } = new HashSet<Payment>();
        public Payment Payment { get; set; } 
        public DateTime BookingDate { get; set; }
    }
}
