using EduConnect.Entities;
using System.Collections.Generic;

namespace EduConnect.DTOs
{
    public class AddressDto
    {
        public string HouseNumber { get; set; }
        public string StreetName { get; set; }
        public string LGAOfResidence { get; set; }
        public string State { get; set; }
         
        public StudentDto Student { get; set; }

    }
    public class AddressResponseModel : BaseResponse 
    {
        public AddressDto Data { get; set; }
    }
    public class AddresssResponseModel : BaseResponse 
    {
        public ICollection<AddressDto> Data { get; set; } = new HashSet<AddressDto>();
    }
    public class AddressUpdateModel
    {
        public string HouseNumber { get; set; }
        public string StreetName { get; set; }
        public string LGAOfResidence { get; set; }
        public string City { get; set; }
       
    }
    public class AddressRequestModel
    {
        public string HouseNumber { get; set; }
        public string StreetName { get; set; }
        public string LGAOfResidence { get; set; }
        public string City { get; set; }
       
    }
}
