using BW.Common.Consts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BW.Website.Common.Utilities
{
    public class AuthorizedUserAttribute : Attribute
    {
        public string AccessLevels { get; set; }

        private string[] AccessLevelsArray { get; set; }

        public AuthorizedUserAttribute()
        {
            AccessLevels = string.Empty;
        }

        public AuthorizedUserAttribute(params string[] permissions)
        {
            AccessLevelsArray = permissions;
        }

        public bool IsMatch(string permission)
        {
            if (!string.IsNullOrEmpty(AccessLevels))
            {
                AccessLevelsArray = AccessLevels.Split(',');
            }

            if (AccessLevelsArray != null && AccessLevelsArray.Length > 0)
            {
                if (AccessLevelsArray.Contains(PermissionCodes.AllowAnonymous))
                {
                    return true;
                }
                return AccessLevelsArray.Contains(permission, StringComparer.InvariantCultureIgnoreCase);
            }

            return true;
        }

        public bool IsMatch(List<string> permissions)
        {
            if (permissions == null)
                permissions = new List<string>();

            if (!string.IsNullOrEmpty(AccessLevels))
            {
                AccessLevelsArray = AccessLevels.Split(',');
            }

            if (AccessLevelsArray != null && AccessLevelsArray.Length > 0)
            {
                if (AccessLevelsArray.Contains(PermissionCodes.AllowAnonymous))
                {
                    return true;
                }

                return AccessLevelsArray.Any(p => permissions.Contains(p, StringComparer.InvariantCultureIgnoreCase));
            }

            return true;
        }
    }
}