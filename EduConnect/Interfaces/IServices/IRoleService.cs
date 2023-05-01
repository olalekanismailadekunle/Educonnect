using EduConnect.DTOs;
using System.Threading.Tasks;

namespace EduConnect.Interfaces.IServices
{
    public interface IRoleService
    {
        Task<RoleResponseModel> AddRole(RoleRequest request);
        Task<RolesResponseModel> GetRoles();
        Task<RoleResponseModel> GetRoleById(int id);
        Task<RoleResponseModel> Delete(int id);
        Task<RoleResponseModel> UpdateRole(UpdateRoleRequest request, int id);
    }
}
