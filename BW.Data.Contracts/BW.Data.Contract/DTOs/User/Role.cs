
using System;
namespace BW.Data.Contract.DTOs
{
    public class Role
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

    }
}
