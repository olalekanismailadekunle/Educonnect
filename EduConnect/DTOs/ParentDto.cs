using EduConnect.Entities;
using EduConnect.Identity;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace EduConnect.DTOs
{
    public class ParentDto
    {
        public int Id { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; }
       public AddressDto Address { get; set; }
        public string MailAddress { get; set; }
        public BookingDto Booking { get; set; }
        public int BookingId { get; set; }
        public UserDto User { get; set; }
   
        public ICollection<StudentDto> Students { get; set; } = new List<StudentDto>();
    }
    public class ParentResponseModel : BaseResponse
    {
        public ParentDto Data { get; set; }
    }
    public class ParentsResponseModel : BaseResponse
    {
        public ICollection<ParentDto> Data { get; set; } = new HashSet<ParentDto>();
    }
    public class ParentUpdateModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string HouseNumber { get; set; }
        public string StreetName { get; set; }
        public string LGAOfResidence { get; set; }
        public string State { get; set; }
        public string MailAddress { get; set; }
        public Booking Booking { get; set; }
        public int BookingId { get; set; }
    }
    public class ParentRequestModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string HouseNumber { get; set; }
        public string StreetName { get; set; }
        public string LGAOfResidence { get; set; }
        public string State { get; set; }
        public string MailAddress { get; set; }
        public string PassWord { get; set; }

        
        //public Booking Booking { get; set; }
        //public int BookingId { get; set; }
       // public User User { get; set; }
        //public int UserId { get; set; }
        public string UserName { get; set; }
    }
}
