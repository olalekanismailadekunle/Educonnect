using EduConnect.DTOs;
using System.Threading.Tasks;

namespace EduConnect.Interfaces.IServices
{
    public interface IAdministratorService
    {
        Task<AdministratorResponseModel> CreateAdministrator(AdministratorRequestModel requestModel);
        Task<AdministratorResponseModel> GetAdministratorById(int id);
        Task<AdministratorResponseModel> GetAdministratorByIdByAdmin(int id);
        Task<AdministratorResponseModel> UpdateAdministrator(AdministratorUpdateModel updateModel, int id);
        Task<AdministratorResponseModel> DeleteAdministrator(int Id);
        Task<AdministratorsResponseModel> GetAllAdministrator();
        Task<AdministratorResponseModel> CreateSuperAdmin(AdministratorRequestModel requestModel);
    }
}
