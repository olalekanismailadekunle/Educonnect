using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace EduConnect.DTOs
{
    public class AdministratorDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

    }
    public class AdministratorResponseModel : BaseResponse
    {
        public AdministratorDto Data { get; set; }
    }
    public class AdministratorsResponseModel : BaseResponse
    {
        public ICollection<AdministratorDto> Data { get; set; } = new HashSet<AdministratorDto>();
    }
    public class AdministratorUpdateModel
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
    }
    public class AdministratorRequestModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

    }
}

