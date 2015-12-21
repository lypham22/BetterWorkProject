
using System;
namespace BW.Data.Contract.DTOs
{
    public class RoleInPermissonView
    {
        public int RoleInPermissionId { get; set; }
        public int ModuleId { get; set; }
        public int RoleId { get; set; }
        public bool PAdd { get; set; }
        public bool PEdit { get; set; }
        public bool PDelete { get; set; }
        public bool PView { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public string ModuleName { get; set; }
        public string RoleName { get; set; }
    }
}
