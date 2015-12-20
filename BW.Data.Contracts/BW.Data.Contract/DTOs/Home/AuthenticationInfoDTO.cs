
using System;
using System.Collections.Generic;
namespace BW.Data.Contract.DTOs
{
    public class AuthenticationInfoDTO
    {
        public AuthenticationInfoDTO()
        {
            modules = new List<ModuleDTO>();
        }

        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool IsActive { get; set; }
        public List<ModuleDTO> modules { get; set; }
    }
}
