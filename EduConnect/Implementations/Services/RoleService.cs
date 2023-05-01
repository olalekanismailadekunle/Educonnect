using EduConnect.DTOs;
using EduConnect.Identity;
using EduConnect.Interfaces.IRepositories;
using EduConnect.Interfaces.IServices;
using System.Linq;
using System.Threading.Tasks;

namespace EduConnect.Implementations.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;

        }

        public async  Task<RoleResponseModel> AddRole(RoleRequest request)
        {
            var alresdyExist = await _roleRepository.Get(x => x.Name == request.Name && x.IsDeleted == false);
            if (alresdyExist != null)
            {
                return new RoleResponseModel
                {
                    Status = false,
                    Message = "Role Already Exist"
                };
            }
            var role = new Role
            {
                IsDeleted = false,
                Name = request.Name,

            };
            await _roleRepository.Create(role);
            return new RoleResponseModel
            {
                Data = new RoleDto
                {
                    Name = request.Name
                },
                Status = true,
                Message = "Role Successfully Created"
            };

        }

        public async Task<RoleResponseModel> Delete(int id)
        {
            var role = await _roleRepository.Get(x => x.Id == id);
            if (role == null)
            {
                return new RoleResponseModel
                {
                    Message = "Role not found",
                    Status = false
                };
            }
            role.IsDeleted = true;
            await _roleRepository.Update(role);
            return new RoleResponseModel
            {
                Status = true,
                Message = "Role Successfully deleted",
            };
        }

        public async Task<RoleResponseModel> GetRoleById(int id)
        {
            var role = await _roleRepository.Get(x => x.Id == id);
            if (role == null)
            {
                return new RoleResponseModel
                {
                    Status = false,
                    Message = "Role Not Found"
                };
            }
            return new RoleResponseModel
            {
                Data = new RoleDto
                {
                    Id = role.Id,
                    Name = role.Name,
                    Description = role.Description,
                },
                Status = true,
                Message = "Role Successfully Retrieved"
            };
        }

        public async Task<RolesResponseModel> GetRoles()
        {
            var roles = await _roleRepository.GetAll();
            if (roles.ToList().Count == 0)
            {
                return new RolesResponseModel
                {
                    Status = false,
                    Message = "Role List is empty"
                };
            }
            return new RolesResponseModel
            {
                Data = roles.Select(role => new RoleDto
                {
                    Id = role.Id,
                    Name = role.Name
                }).ToList(),
                Status = true,
                Message = "Subjects Successfully Retrieved"
            };
        }

        public async Task<RoleResponseModel> UpdateRole(UpdateRoleRequest request, int id)
        {
            var role = await _roleRepository.Get(id);
            if (role == null)
            {
                return new RoleResponseModel
                {
                    Message = "Role does not exist",
                    Status = false
                };
            }
            role.Name = request.Name ?? role.Name;
            role.Description = request.Description ?? role.Description;
            await _roleRepository.Update(role);
            return new RoleResponseModel
            {
                Message = "Role updated successfully",
                Status = true,
                Data = new RoleDto
                {
                    Id = role.Id,
                    Name = role.Name,
                    Description = role.Description
                }
            };
        }
    }
}
