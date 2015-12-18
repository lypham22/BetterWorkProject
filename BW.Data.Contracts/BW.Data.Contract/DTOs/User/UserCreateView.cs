using System;
using System.Collections.Generic;

namespace BW.Data.Contract.DTOs
{
    public class UserCreateView
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool IsActive { get; set; }
        public string RoleId { get; set; }

        public List<RoleListView> roles { get; set; }
    }
}
