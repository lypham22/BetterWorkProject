﻿
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BW.Website.Resource;
namespace BW.Data.Contract.DTOs
{
    public class RoleView
    {
        public RoleView()
        {
            ModuleDTOs = new List<ModuleDTO>();
        }
        public string RoleId { get; set; }
        [Required(ErrorMessageResourceType = typeof(GlobalResource), ErrorMessageResourceName = "validRoleNameRequire")]
        public string RoleName { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string RoleDescription { get; set; }
        public string ModuleName { get; set; }

        public List<ModuleDTO> ModuleDTOs { get; set; }

    }
}
