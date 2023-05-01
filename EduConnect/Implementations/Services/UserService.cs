using EduConnect.DTOs;
using EduConnect.Interfaces.IRepositories;
using EduConnect.Interfaces.IServices;
using Org.BouncyCastle.Crypto.Generators;
using System.Linq;
using System.Threading.Tasks;

namespace EduConnect.Implementations.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;

        }
        public async Task<BaseResponse> DeleteUser(int id)
        {

            var user = await _userRepository.Get(id);
            if (user == null)
            {
                return new BaseResponse
                {
                    Message = "User not found",
                    Status = false
                };
            }
            user.IsDeleted = true;
            await _userRepository.SaveChanges();
            return new BaseResponse
            {
                Message = $"User with mail {user.Email} deleted Statusfully",
                Status = true,
                
            };
        }

        public async Task<UsersResponseModel> GetAll()
        {

            var allUser = await _userRepository.GetAll(x => x.IsDeleted == false);
            if(allUser.ToList().Count == 0)
            {
                return new UsersResponseModel
                {
                  
                    Status = false,
                    Message = "Statusful"
                };
            }
            var check = allUser.Select(x => new UserDto
            {

                Id = x.Id,
                UserName = x.UserName,
                Email = x.Email,
                Password = x.Password

            }).ToList();
            return new UsersResponseModel
            {
                Data = check,
                Status = true,
                Message = "Statusful"
            };
        }

        public async Task<UserResponseModel> GetUser(int id)
        {
            var user = await _userRepository.Get(id);
            if (user == null)
            {
                return new UserResponseModel
                {
                    Message = "User not found",
                    Status = false
                };
            }
            return new UserResponseModel
            {
                Message = "User found",
                Status = true,
                Data = new UserDto
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    Password = user.Password
                }
            };
        }

        public async Task<UserResponseModel1> GetUserById(int id)
        {
            var user = await _userRepository.GetById(id);
            if (user == null)
            {
                return new UserResponseModel1
                {
                    Message = "User not found",
                    Status = false
                };
            }
            return new UserResponseModel1
            {
                Message = "User found",
                Status = true,
                Data = new UserDto
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    Password = user.Password
                },
                
            };
        }

        public async Task<UserResponseModel> Login(LoginRequest login)
        {
            var user = await _userRepository.GetByEmail(login.Email);
            if (user == null)
            {
                return new UserResponseModel
                {
                    Message = "Invalid Username or password",
                    Status = false,
                };
            }

            //var hashedPass = BCrypt.Net.BCrypt.HashPassword(login.Password);
            var userVerify = BCrypt.Net.BCrypt.Verify(login.Password, user.Password);

            if (!userVerify)
            {
                return new UserResponseModel
              {
                   Status = false,
                    Message = "Invalid Username or password"
               };
            }
            return new UserResponseModel
            {
                Message = $"User {user.UserName} Successfully Signed in",
                Status = true,
                Data = new UserDto
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    Password = user.Password,
                    Roles = user.UserRoles.Select(x => new RoleDto
                    {
                        Name = x.Role.Name,
                        Description = x.Role.Description,
                    }).ToList()
                }
            };
        }

      
    }
}
