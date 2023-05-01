using EduConnect.Entities;
using EduConnect.Enums;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace EduConnect.DTOs
{
    public class TutorDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
       
        public int AddressId { get; set; }
        public string City { get; set; }
        public string ProfilePicture { get; set; }
        public List<string> DocumentImages { get; set; }
        public Qualification Qualification { get; set; }
        public WorkingHours WorkinHoursPerday { get; set; }
        public int SubjectId { get; set; }
        public string MailAddress { get; set; }
        public Status Status { get; set; }
        public AddressDto Address { get; set; }
        public ICollection<SubjectDto> Subjects { get; set; } = new HashSet<SubjectDto>();
        public ICollection<StudentDto> Students { get; set; } = new HashSet<StudentDto>();


    }
    public class TutorResponseModel : BaseResponse
    {
        public TutorDto Data { get; set; }
    }
    public class TutorsResponseModel : BaseResponse
    {
        public ICollection<TutorDto> Data { get; set; }
    }
    public class TutorUpdateModel 
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string HouseNumber { get; set; }
        public string StreetName { get; set; }
        public string LGAOfResidence { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Password { get; set; }
        public WorkingHours WorkinHoursPerday { get; set; }
        public IFormFile ProfilePicture { get; set; }
        public IList<IFormFile> DocumentImages { get; set; } = new List<IFormFile>();
        public Qualification Qualification { get; set; }
        public Specialization Specialization { get; set; }
        public IList<int> SubjectIds { get; set; } = new List<int>();
        public string MailAddress { get; set; }
    }
    public class TutorRequestModel 
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string HouseNumber { get; set; }
        public string StreetName { get; set; }
        public string LGAOfResidence { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Password { get; set; }
        public WorkingHours WorkinHoursPerday { get; set; }
        public IFormFile ProfilePicture { get; set; }
        public List<IFormFile> DocumentImages { get; set; }
        public Qualification Qualification { get; set; }
        public Specialization Specialization { get; set; }
        public IList<int> SubjectIds { get; set; } = new List<int>();
        public string MailAddress { get; set; }
    }
}
