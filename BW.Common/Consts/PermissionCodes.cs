using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BW.Common.Consts
{
    public static class PermissionCodes
    {
        public const string AllowAnonymous = "AllowAnonymous";

        #region View Permissions
        public const string ViewUser = "ViewUser";
        public const string ViewRole = "ViewRole";
        #endregion

        #region Add Permissions
        public const string AddUser = "AddUser";
        public const string AddRole = "AddRole";
        #endregion

        #region Edit Permissions
        public const string EditUser = "EditUser";
        public const string EditRole = "EditRole";
        #endregion

        #region Delete Permissions
        public const string DeleteUser = "DeleteUser";
        public const string DeleteRole = "DeleteRole";
        #endregion
    }
}
