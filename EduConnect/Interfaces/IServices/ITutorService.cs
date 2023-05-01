using EduConnect.DTOs;
using EduConnect.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EduConnect.Interfaces.IServices
{
    public interface ITutorService
    {
        Task<TutorResponseModel> CreateTutor(TutorRequestModel requestModel);
        Task<TutorResponseModel> GetTutorById(int id);
        Task<TutorResponseModel> GetTutorByIdForAdmin(int id);
        //Task<LocalGovernmentResponseModel> GetAllLocalGovernment();
        Task<TutorsResponseModel> GetTutorsByLocalGovernment(string name);
        Task<TutorsResponseModel> GetTutorByQualification(Qualification qualification);
        Task<TutorsResponseModel> GetTutorBySpecialization(Specialization specialization);

        Task<TutorsResponseModel> GetTutorByStatus(Status status);
        IList<string> GetEnumBySpecialization();
        IList<string> GetEnumByStatus();
        //Task<StatesResponseModel> GetAllState();
        Task<TutorsResponseModel> GetTutorsByState(string name);
        Task<TutorsResponseModel> GetTutorsAccordingToStudent(int id);
        Task<TutorResponseModel> UpdateTutor(TutorUpdateModel updateModel, int id);
        Task<BaseResponse> DeleteTutor(int Id);
        Task<TutorsResponseModel> GetAllTutor();
        IList<string> GetEnumQualification();
    }
}
