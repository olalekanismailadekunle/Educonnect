using EduConnect.DTOs;
using System.Threading.Tasks;

namespace EduConnect.Interfaces.IServices
{
    public interface IParentService
    {
        Task<ParentResponseModel> CreateParent(ParentRequestModel requestModel);
        Task<ParentResponseModel> GetparentById(int id);
        Task<ParentResponseModel> GetparentByIdForAdmin(int id);
        Task<ParentResponseModel> UpdateParent(ParentUpdateModel updateModel, int id);
        Task<ParentResponseModel> DeleteParent(int Id);
        Task<ParentsResponseModel> GetAllParent();
    }
}
