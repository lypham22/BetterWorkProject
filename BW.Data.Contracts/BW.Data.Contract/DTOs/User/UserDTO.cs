
using System;
using System.Collections.Generic;
namespace BW.Data.Contract.DTOs
{
    public class UserDTO
    {
        public UserDTO()
        {
            RoleDTOs = new List<RoleDTO>();
        }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool IsActive { get; set; }
        public string RoleName { get; set; }
        public List<RoleDTO> RoleDTOs { get; set; }
    }
}
