//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BW.Repository.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class BW_Module
    {
        public BW_Module()
        {
            this.BW_RoleInPermission = new HashSet<BW_RoleInPermission>();
        }
    
        public int ModuleId { get; set; }
        public string ModuleName { get; set; }
        public string ModuleDescription { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
    
        public virtual ICollection<BW_RoleInPermission> BW_RoleInPermission { get; set; }
    }
}
