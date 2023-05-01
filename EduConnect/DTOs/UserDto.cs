using EduConnect.Entities;
using System;
using System.Collections.Generic;

namespace EduConnect.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public List<RoleDto> Roles { get; set; } = new List<RoleDto>();

    }
    public class UserResponseModel : BaseResponse
    {
        public UserDto Data { get; set; }
    }
    public class UserResponseModel1 : BaseResponse
    {
        public UserDto Data { get; set; }
        public int AdminId { get; set; }
    }
    public class UsersResponseModel : BaseResponse
    {
        public ICollection<UserDto> Data { get; set; } = new HashSet<UserDto>();
    }
    public class CreateUserRequset
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
    }
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }

    }
}

