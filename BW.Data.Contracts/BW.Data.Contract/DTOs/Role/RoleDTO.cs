
using System;
using System.Collections.Generic;
namespace BW.Data.Contract.DTOs
{
    public class RoleDTO
    {
        public RoleDTO()
        {
            ModuleDTOs = new List<ModuleDTO>();
        }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string RoleDescription { get; set; }
        public string ModuleName { get; set; }

        public List<ModuleDTO> ModuleDTOs { get; set; }
    }
}
