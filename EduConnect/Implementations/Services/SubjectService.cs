using EduConnect.DTOs;
using EduConnect.Entities;
using EduConnect.Interfaces.IRepositories;
using EduConnect.Interfaces.IServices;
using System.Linq;
using System.Threading.Tasks;

namespace EduConnect.Implementations.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly ISubjectRepository _subjectRepository;

        public SubjectService(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        public async Task<SubjectResponseModel> CreateSubject(SubjectRequestModel requestModel)
        {
            var alresdyExist = await _subjectRepository.Get(x => x.Name == requestModel.SubjectName && x.IsDeleted == false);
            if (alresdyExist != null)
            {
                return new SubjectResponseModel
                {
                    Status = false,
                    Message = "Subject Already Exist"
                };
            }
            var subject = new Subject
            {
                IsDeleted = false,
                Name = requestModel.SubjectName,
                
            };
            await _subjectRepository.Create(subject);
            return new SubjectResponseModel
            {
                Data = new SubjectDto
                {
                    Name = requestModel.SubjectName
                },
                Status = true,
                Message = "Subject Successfully Created"
            };
        }

        public async Task<SubjectResponseModel> DeleteSubject(int Id)
        {
            var subject = await _subjectRepository.Get(x => x.Id == Id);
            if (subject == null)
            {
                return new SubjectResponseModel
                {
                    Message = "Subject not found",
                    Status = false
                };
            }
            subject.IsDeleted = true;
            await _subjectRepository.Update(subject);
            return new SubjectResponseModel
            {
                Status = true,
                Message = "Subject Successfully deleted",
            };
        }

        public async Task<SubjectsResponseModel> GetAllSubject()
        {
            var subjects = await _subjectRepository.GetAll(x => x.IsDeleted == false);
            if (subjects.ToList().Count == 0)
            {
                return new SubjectsResponseModel
                {
                    Status = false,
                    Message = "subject List is empty"
                };
            }
            return new SubjectsResponseModel
            {
                Data = subjects.Select(subject => new SubjectDto
                {
                    Id = subject.Id,
                    Name = subject.Name,
                }).ToList(),
                Status = true,
                Message = "Subjects Successfully Retrieved"
            };
        }

        public async Task<SubjectResponseModel> GetSubjectById(int id)
        {
            var subject = await _subjectRepository.Get(x => x.Id == id);
            if (subject == null)
            {
                return new SubjectResponseModel
                {
                    Status = false,
                    Message = "Subject Not Found"
                };
            }
            return new SubjectResponseModel
            {
                Data = new SubjectDto
                {
                    Id = subject.Id,
                    Name = subject.Name
                },
                Status = true,
                Message = "Subject Successfully Retrieved"
            };
        }

        public async Task<SubjectResponseModel> UpdateSubject(SubjectUpdateModel updateModel, int id)
        {
            var subject = await _subjectRepository.Get(x => x.Id == id);
            if (subject == null)
            {
                return new SubjectResponseModel
                {
                    Message = "Subject not found",
                    Status = false
                };
            }
            subject.Name = updateModel.SubjectName;
            await _subjectRepository.Update(subject);
            return new SubjectResponseModel
            {
                Status = true,
                Message = "Subject Successfully Updated",
                Data = new SubjectDto
                {
                    Name = subject.Name,
                },
            };
        }
    }
}
