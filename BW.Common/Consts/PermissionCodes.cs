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
        public const string AllowAccess = "AllowAccess";
        public const string NotAllow = "NotAllow";

        #region View Permissions
        public const string ViewManageUser = "ViewManageUser";
        public const string ViewManageRole = "ViewManageRole";
        #endregion

        #region Add Permissions
        public const string AddManageUser = "AddManageUser";
        public const string AddManageRole = "AddManageRole";
        #endregion

        #region Edit Permissions
        public const string EditManageUser = "EditManageUser";
        public const string EditManageRole = "EditManageRole";
        #endregion

        #region Delete Permissions
        public const string DeleteManageUser = "DeleteManageUser";
        public const string DeleteManageRole = "DeleteManageRole";
        #endregion
    }
}
