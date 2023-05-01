using EduConnect.DTOs;
using System.Threading.Tasks;

namespace EduConnect.Interfaces.IServices
{
    public interface IStudentService
    {
        Task<StudentResponseModel> CreateStudent(StudentRequestModel requestModel);
        Task<StudentResponseModel> GetStudentByLocalGovernment(string name);
        Task<StudentResponseModel> GetStudentById(int id);
        Task<StudentResponseModel> UpdateStudent(StudentUpdateModel updateModel, int id);
        Task<StudentResponseModel> GetStudentByState(string name);
        Task<StudentResponseModel> DeleteStudent(int Id);
        Task<StudentResponseModel> DeleteStudentForParent(int Id);
        Task<StudentsResponseModel> GetAllStudent();
        Task<StudentsResponseModel> GetAllToBeApprovedDeletedStudent();
        Task<StudentsResponseModel> GetAllStudentForParent(int id);
    }
}
