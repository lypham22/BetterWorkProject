
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BW.Website.Resource;

namespace BW.Data.Contract.DTOs
{
    public class RoleCreateView
    {
        public int RoleId { get; set; }
        [Required(ErrorMessageResourceType = typeof(GlobalResource), ErrorMessageResourceName = "validRoleNameRequire")]
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
