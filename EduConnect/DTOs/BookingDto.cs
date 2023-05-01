using EduConnect.Entities;
using EduConnect.Enums;
using System;
using System.Collections.Generic;

namespace EduConnect.DTOs
{
    public class BookingDto
    {
        public Guid OrderReference { get; set; }
        public string ParentName { get; set; }
        public string StudentName { get; set; }
        public string TutorName { get; set; }
        public int TutorId { get; set; }

        public int ParentId { get; set; }
        public int Id { get; set; }
       
         

       
        public ICollection<PaymentDto> Payments { get; set; } = new HashSet<PaymentDto>();
        public DateTime BookingDate { get; set; }

    }
    public class BookingResponseModel : BaseResponse
    {
        public BookingDto Data { get; set; }
    }
    public class BookingsResponseModel : BaseResponse
    {
        public ICollection<BookingDto> Data { get; set; } = new HashSet<BookingDto>();
    }
    
    public class BookingRequestModel
    {
        public int ParentId { get; set; }
        public int StudentId { get; set; }
        public int SubjectId { get; set; }
        public int TutorId { get; set; }

        public BookingHour BookingHour { get; set; }
        public  PaymentCategory PaymentCategory { get; set; }
    
        
    }
}
