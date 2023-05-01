using EduConnect.DTOs;
using EduConnect.Entities;
using EduConnect.Identity;
using EduConnect.Interfaces.IRepositories;
using EduConnect.Interfaces.IServices;
using System.Linq;
using System.Threading.Tasks;

namespace EduConnect.Implementations.Services
{
    public class ParentService : IParentService
    {
        private readonly IParentRepository _parentRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;


        public ParentService(IParentRepository parentRepository, IRoleRepository roleRepository, IUserRepository userRepository)
        {
            _parentRepository = parentRepository;
            _roleRepository = roleRepository;
            _userRepository = userRepository;
        }

        public async Task<ParentResponseModel> CreateParent(ParentRequestModel requestModel)
        {

            var parentExist = await _parentRepository.Get(x => x.MailAddress == requestModel.MailAddress && x.IsDeleted == false);
            if (parentExist != null)
            {
                return new ParentResponseModel
                {
                    Status = false,
                    Message = "Parent already exist"
                };
            }
            var user = new User
            {
                Email = requestModel.MailAddress,
                UserName = requestModel.FirstName,
                Password = BCrypt.Net.BCrypt.HashPassword(requestModel.PassWord),
                IsDeleted = false,

            };
            var role = await _roleRepository.Get(x => x.Name == "Parent".ToLower());
            var userRole = new UserRole
            {
                User = user,
                UserId = user.Id,
                Role = role,
                RoleId = role.Id
            };
            user.UserRoles.Add(userRole);
            

            var parent = new Parent
            {
                FirstName = requestModel.FirstName,
                LastName = requestModel.LastName,
                UserId = user.Id,
                IsDeleted = false,
                MailAddress = requestModel.MailAddress,
                User = user,
                
            };
            var address = new Address
            {
                HouseNumber = requestModel.HouseNumber,
                LGAOfResidence = requestModel.LGAOfResidence,
                State = requestModel.State,
                StreetName = requestModel.StreetName,
                ParentId = parent.Id,
                Parent = parent
            };
            parent.Address = address;

            await _userRepository.Create(user);
            var adminss = await _parentRepository.Create(parent);
            return new ParentResponseModel
            {
                Message = "Parent created successfully",
                Status = true,
                Data = new ParentDto
                {
                    FirstName = parent.FirstName,
                    LastName = parent.LastName,
                    MailAddress = parent.MailAddress,
                    Address = new AddressDto
                    {
                        State = parent.Address.State,
    
                    }
                }
            };

        }

        public async Task<ParentResponseModel> DeleteParent(int Id)
        {
            var parent = await _parentRepository.Get(x => x.Id == Id);
            if (parent == null)
            {
                return new ParentResponseModel
                {
                    Message = "Parent not found",
                    Status = false
                };
            }
            parent.IsDeleted = true;
            await _parentRepository.Update(parent);
            return new ParentResponseModel
            {
                Status = true,
                Message = "Parent Successfully deleted",
            };
        }

        public async Task<ParentsResponseModel> GetAllParent()
        {
            var parents = await _parentRepository.GetAllParent();
            if (parents.Count == 0)
            {
                return new ParentsResponseModel
                {
                    Status = false,
                    Message = "Parent List is empty"
                };
            }
            return new ParentsResponseModel
            {
                Data = parents.Select(parent => new ParentDto
                {
                    Id = parent.Id,
                    FirstName = parent.FirstName,
                    LastName = parent.LastName,
                    MailAddress = parent.MailAddress,
                    Address = new AddressDto
                    {
                        State = parent.Address.State,
                        LGAOfResidence = parent.Address.LGAOfResidence,
                        HouseNumber = parent.Address.HouseNumber,
                        StreetName = parent.Address.StreetName
                    },
                }).ToList(),
                Status = true,
                Message = "Parents Successfully Retrieved"
            };
        }

        public async Task<ParentResponseModel> GetparentById(int id)
        {
            var parent = await _parentRepository.GetParent(x => x.User.Id == id);
            if (parent == null)
            {
                return new ParentResponseModel
                {
                    Status = false,
                    Message = "Parent Not Found"
                };
            }
            return new ParentResponseModel
            {
                Data = new ParentDto
                {
                    Id = parent.Id,
                    FirstName = parent.FirstName,
                    LastName = parent.LastName,
                    MailAddress = parent.MailAddress,
                    Address = new AddressDto
                    {
                        State = parent.Address.State,
                        LGAOfResidence = parent.Address.LGAOfResidence,
                        HouseNumber = parent.Address.HouseNumber,
                        StreetName = parent.Address.StreetName
                    },
                },
                Status = true,
                Message = "Parent Successfully Retrieved"
            };
        }

        public async Task<ParentResponseModel> GetparentByIdForAdmin(int id)
        {
            var parent = await _parentRepository.GetParent(x => x.Id == id);
            if (parent == null)
            {
                return new ParentResponseModel
                {
                    Status = false,
                    Message = "Parent Not Found"
                };
            }
            return new ParentResponseModel
            {
                Data = new ParentDto
                {
                    FirstName = parent.FirstName,
                    LastName = parent.LastName,
                    MailAddress = parent.MailAddress,
                    Address = new AddressDto
                    {
                        State = parent.Address.State,
                        LGAOfResidence = parent.Address.LGAOfResidence,
                        HouseNumber = parent.Address.HouseNumber,
                        StreetName = parent.Address.StreetName
                    },
                },
                Status = true,
                Message = "Parent Successfully Retrieved"
            };
        }

        public async Task<ParentResponseModel> UpdateParent(ParentUpdateModel updateModel, int id)
        {
            var parent = await _parentRepository.GetParent(x => x.UserId == id);
            var user = await _userRepository.Get(x => x.Id == id);
            if (parent == null)
            {
                return new ParentResponseModel
                {
                    Message = "Parent not found",
                    Status = false
                };
            }
            if(updateModel.State != null)
            {
                var adress = new Address
                {
                    State = updateModel.State,
                    LGAOfResidence = updateModel.LGAOfResidence,
                    HouseNumber = updateModel.HouseNumber,

                    StreetName = updateModel.StreetName,

                };
                parent.Address = adress;

            }
           
            parent.FirstName = updateModel.FirstName ?? parent.FirstName;
            parent.LastName = updateModel.LastName ?? parent.LastName;
            parent.MailAddress = updateModel.MailAddress ?? parent.MailAddress;
            user.Email = updateModel.MailAddress ?? user.Email;
            await _parentRepository.Update(parent);
         

            return new ParentResponseModel
            {
                Status = true,
                Message = "Parent Successfully Updated",
                Data = new ParentDto
                {
                    FirstName = parent.FirstName,
                    LastName = parent.LastName,
                    MailAddress = parent.MailAddress,
                    Address = new AddressDto
                    {
                        State = updateModel.State,
                        LGAOfResidence = updateModel.LGAOfResidence,
                        HouseNumber = updateModel.HouseNumber,

                        StreetName = updateModel.StreetName,
                    },
                },
            };
        }
    }
}
