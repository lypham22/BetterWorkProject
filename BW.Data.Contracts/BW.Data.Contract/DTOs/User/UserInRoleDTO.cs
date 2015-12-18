
using System;
using System.Collections.Generic;
namespace BW.Data.Contract.DTOs
{
    public class UserInRoleDTO
    {
        public int UserId { get; set; }

        public int RoleID { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}
