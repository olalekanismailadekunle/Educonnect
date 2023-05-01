using EduConnect.Entities;
using EduConnect.Enums;
using System.Collections.Generic;

namespace EduConnect.DTOs
{
    public class StudentDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string HouseNumber { get; set; }
        public string StreetName { get; set; }
        public string LGAOfResidence { get; set; }
        public string State { get; set; }
        public Level Level { get; set; }
        public AddressDto Address { get; set; }
       
        public ICollection<TutorDto> Tutors { get; set; } = new List<TutorDto>();
        public ICollection<SubjectDto> Subjects { get; set; } = new HashSet<SubjectDto>();
    }
    public class StudentResponseModel : BaseResponse
    {
        public StudentDto Data { get; set; }
    }
    public class StudentsResponseModel : BaseResponse
    {
        public ICollection<StudentDto> Data { get; set; } = new HashSet<StudentDto>();
    }
    public class StudentUpdateModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Level Level { get; set; }
        public string HouseNumber { get; set; }
        public string StreetName { get; set; }
        public string LGAOfResidence { get; set; }
        public string State { get; set; }
        public IList<int> SubjectIds { get; set; } = new List<int>();

    }
    public class StudentRequestModel
    {
        public int ParentId { get;set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Level Level { get; set; }
        public string HouseNumber { get; set; }
        public string StreetName { get; set; }
        public string LGAOfResidence { get; set; }
        public string State { get; set; }
        public IList<int> SubjectIds { get; set; } = new List<int>();
    }
}
