using EduConnect.DTOs;
using EduConnect.Entities;
using EduConnect.Identity;
using EduConnect.Interfaces.IRepositories;
using EduConnect.Interfaces.IServices;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EduConnect.Implementations.Services
{
    public class AdministratorService : IAdministratorService
    {
        private readonly IAdministratorRepository _administratorRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        public AdministratorService(IAdministratorRepository administratorRepository, IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _administratorRepository = administratorRepository;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public async Task<AdministratorResponseModel> CreateAdministrator(AdministratorRequestModel requestModel)
        { 
            var adminExist = await _administratorRepository.Get(x => x.MailAddress == requestModel.Email && x.IsDeleted == false);
            if (adminExist != null)
            {
                return new AdministratorResponseModel
                {
                    Status = false,
                    Message = "Administrator already exist"
                };
            }
            var user = new User
            {
                Email = requestModel.Email,
                UserName = requestModel.FirstName,
                Password = BCrypt.Net.BCrypt.HashPassword(requestModel.Password),
                IsDeleted = false,
                
            };
            var role = await _roleRepository.Get(x => x.Name == "Administrator");
            var userRole = new UserRole
            {
                User = user,
                UserId = user.Id,
                Role = role,
                RoleId = role.Id
            };
            user.UserRoles.Add(userRole);
          

            var administrator = new Administrator
            {
                FirstName = requestModel.FirstName,
                LastName = requestModel.LastName,
                UserId = user.Id,
                IsDeleted = false,
                MailAddress = requestModel.Email, 
                User = user,
            };
            await _userRepository.Create(user);
            var adminss = await _administratorRepository.Create(administrator);
            return new AdministratorResponseModel
            {
                Message = "Administrator created successfully",
                Status = true,
                Data = new AdministratorDto
                {
                    Id = administrator.Id,
                    FirstName = administrator.FirstName,
                    LastName = administrator.LastName,
                    Email = requestModel.Email,
                    PhoneNumber = requestModel.PhoneNumber,
                }
            };
        }   

        public async Task<AdministratorResponseModel> DeleteAdministrator(int Id)
        {
            var admin = await _administratorRepository.Get(x => x.Id == Id);
            if (admin == null)
            {
                return new AdministratorResponseModel
                {
                    Message = "Administrator not found",
                    Status = false
                };
            }
            admin.IsDeleted = true;
            await _administratorRepository.Update(admin);
            return new AdministratorResponseModel
            {
                Status = true,
                Message = "Administrator Successfully deleted",
            };
        }

        public async Task<AdministratorResponseModel> GetAdministratorById(int id)
        {
            var admin = await _administratorRepository.Get(x => x.User.Id == id);
            if (admin == null)
            {
                return new AdministratorResponseModel
                {
                    Status = false,
                    Message = "Administrator Not Found"
                };
            }
            return new AdministratorResponseModel
            {
                Data = new AdministratorDto
                {
                    Id = admin.Id,
                    FirstName = admin.FirstName,
                    LastName = admin.LastName,
                    Email = admin.MailAddress,
                },
                Status = true,
                Message = "Administrator Successfully Retrieved"
            };

        }

        public async Task<AdministratorsResponseModel> GetAllAdministrator()
        {
            var admins = await _administratorRepository.GetAll();
            if (admins.ToList().Count == 0)
            {
                return new AdministratorsResponseModel
                {
                    Status = false,
                    Message = "Administrator List is empty"
                };
            }
            return new AdministratorsResponseModel
            {
                Data = admins.Select(admin => new AdministratorDto
                {
                    Id = admin.Id,
                    FirstName = admin.FirstName,
                    LastName = admin.LastName,
                    Email = admin.MailAddress,
                }).ToList(),
                Status = true,
                Message = "Administrators Successfully Retrieved"
            };
        }

        public async Task<AdministratorResponseModel> UpdateAdministrator(AdministratorUpdateModel updateModel, int id)
        {

            var admin = await _administratorRepository.Get(x => x.Id == id);
            if (admin == null)
            {
                return new AdministratorResponseModel
                {
                    Message = "Administrator not found",
                    Status = false
                };
            }
            admin.FirstName = updateModel.FirstName;
            admin.LastName = updateModel.LastName;
            admin.MailAddress = updateModel.Email;
            admin.Address = updateModel.Address;

            await _administratorRepository.Update(admin);
            return new AdministratorResponseModel
            {
                Status = true,
                Message = "Administrator Successfully Updated",
                Data = new AdministratorDto
                {
                    Id = admin.Id,
                    FirstName = admin.FirstName,
                    LastName = admin.LastName,
                    Email = admin.MailAddress,
                },
            };
        }
        public async Task<AdministratorResponseModel> CreateSuperAdmin(AdministratorRequestModel requestModel)
        {
            var adminExist = await _administratorRepository.Get(x => x.MailAddress == requestModel.Email && x.IsDeleted == false);
            if (adminExist != null)
            {
                return new AdministratorResponseModel
                {
                    Status = false,
                    Message = "Administrator already exist"
                };
            }
            var user = new User
            {
                Email = requestModel.Email,
                UserName = requestModel.FirstName,
                Password = BCrypt.Net.BCrypt.HashPassword(requestModel.Password),
                IsDeleted = false,

            };
            var role = await _roleRepository.Get(x => x.Name == "SuperAdmin");
            var userRole = new UserRole
            {
                User = user,
                UserId = user.Id,
                Role = role,
                RoleId = role.Id
            };
            user.UserRoles.Add(userRole);


            var administrator = new Administrator
            {
                FirstName = requestModel.FirstName,
                LastName = requestModel.LastName,
                UserId = user.Id,
                IsDeleted = false,
                MailAddress = requestModel.Email,
                User = user,
            };
            await _userRepository.Create(user);
            var adminss = await _administratorRepository.Create(administrator);
            return new AdministratorResponseModel
            {
                Message = "Administrator created successfully",
                Status = true,
                Data = new AdministratorDto
                {
                    Id = administrator.Id,
                    FirstName = administrator.FirstName,
                    LastName = administrator.LastName,
                    Email = requestModel.Email,
                    PhoneNumber = requestModel.PhoneNumber,
                }
            };
        }

        public async Task<AdministratorResponseModel> GetAdministratorByIdByAdmin(int id)
        {
            var admin = await _administratorRepository.Get(x => x.Id == id);
            if (admin == null)
            {
                return new AdministratorResponseModel
                {
                    Status = false,
                    Message = "Administrator Not Found"
                };
            }
            return new AdministratorResponseModel
            {
                Data = new AdministratorDto
                {
                    Id = admin.Id,
                    FirstName = admin.FirstName,
                    LastName = admin.LastName,
                    Email = admin.MailAddress,
                },
                Status = true,
                Message = "Administrator Successfully Retrieved"
            };
        }
    }
}
