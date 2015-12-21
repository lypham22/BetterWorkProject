
using System;
using System.Collections.Generic;
namespace BW.Data.Contract.DTOs
{
    public class RoleCreateView
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
