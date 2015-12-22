
using System;
using System.Collections.Generic;
namespace BW.Data.Contract.DTOs
{
    public class RoleInPermListView
    {
        public List<RoleInPermissonView> RoleInPermisson { get; set; }
        public List<RoleListView> Role { get; set; }
    }
}
