using EduConnect.Entities;
using System.Collections.Generic;

namespace EduConnect.DTOs
{
    public class SubjectDto
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public ICollection<TutorDto> SubjectTutor { get; set; } = new HashSet<TutorDto>();
        public ICollection<StudentDto> StudentSubject { get; set; } = new HashSet<StudentDto>();
    }
    public class SubjectResponseModel : BaseResponse
    {
        public SubjectDto Data { get; set; }
    }
    public class SubjectsResponseModel : BaseResponse
    {
        public ICollection<SubjectDto> Data { get; set; } = new HashSet<SubjectDto>();
    }
    public class SubjectUpdateModel
    {
         public string  SubjectName { get; set; }
    }
    public class SubjectRequestModel
    {
        public string SubjectName { get; set; }
    }
}
