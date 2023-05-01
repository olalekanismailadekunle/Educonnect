using System;
using System.Collections.Generic;

namespace EduConnect.DTOs
{
    public class RoleDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public class RoleResponseModel : BaseResponse
    {
        public RoleDto Data { get; set; }
    }
    public class RolesResponseModel : BaseResponse
    {
        public ICollection<RoleDto> Data { get; set; } = new HashSet<RoleDto>();
    }
    public class RoleRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public class UpdateRoleRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
