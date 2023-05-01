using EduConnect.DTOs;
using System.Threading.Tasks;

namespace EduConnect.Interfaces.IServices
{
    public interface ISubjectService
    {
        Task<SubjectResponseModel> CreateSubject(SubjectRequestModel requestModel);
        Task<SubjectResponseModel> GetSubjectById(int id);
        Task<SubjectResponseModel> UpdateSubject(SubjectUpdateModel updateModel, int id);
        Task<SubjectResponseModel> DeleteSubject(int Id);
        Task<SubjectsResponseModel> GetAllSubject();
    }
}
