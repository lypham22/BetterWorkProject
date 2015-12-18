
using System;
using System.Collections.Generic;
namespace BW.Data.Contract.DTOs
{
    public class UserDetailsDTO
    {
        public UserDTO user { get; set; }
        public List<RoleDTO> roles { get; set; }
    }
}
