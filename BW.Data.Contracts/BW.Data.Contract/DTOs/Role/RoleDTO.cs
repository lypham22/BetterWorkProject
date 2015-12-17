
using System;
namespace BW.Data.Contract.DTOs
{
    public class RoleDTO
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string RoleDescription { get; set; }
    }
}
