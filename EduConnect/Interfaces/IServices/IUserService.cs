using EduConnect.DTOs;
using EduConnect.Identity;
using System.Threading.Tasks;

namespace EduConnect.Interfaces.IServices
{
    public interface IUserService 
    {
        Task<UserResponseModel> Login(LoginRequest login);
        Task<UsersResponseModel> GetAll();
        Task<UserResponseModel> GetUser(int id);
        Task<UserResponseModel1> GetUserById(int id);
        Task<BaseResponse> DeleteUser(int id);
    }
}
