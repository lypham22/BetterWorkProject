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
    
    public partial class BW_UserInRole
    {
        public int UserInRoleId { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
    
        public virtual BW_Role BW_Role { get; set; }
        public virtual BW_User BW_User { get; set; }
    }
}