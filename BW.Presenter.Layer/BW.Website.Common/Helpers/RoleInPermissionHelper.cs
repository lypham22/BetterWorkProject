using BW.Common.Enums;
using BW.Data.Contract;
using BW.Data.Contract.DTOs;
using BW.Website.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Net.Http;


namespace BW.Website.Common.Helpers
{
    public class RoleInPermissionHelper
    {
        public static ResponeMessage<List<RoleInPermissonView>> GetRoleInPermissionByRoleId(string roleIdEnc)
        {
            var response = new ResponeMessage<List<RoleInPermissonView>> { Code = ErrorCodeEnum.SUCCESS, Data = new List<RoleInPermissonView>() };
            List<RoleInPermissonView> roleInPermView = new List<RoleInPermissonView>();
            if (!string.IsNullOrEmpty(roleIdEnc))
            {
                int roleId = int.Parse(roleIdEnc);
                HttpResponseMessage reponse = ApiServiceUtilities.GetReponse("api/RoleInPermissionApi/GetRoleInPermissionByRoleId/" + roleId);
                if (reponse.IsSuccessStatusCode)
                {
                    var roleInPerms = reponse.Content.ReadAsAsync<ResponeMessage<List<RoleInPermissonDTO>>>().Result;
                    foreach (var s in roleInPerms.Data)
                    {
                        roleInPermView.Add(new RoleInPermissonView
                        {
                            RoleInPermissionId = s.RoleInPermissionId,
                            RoleId = s.RoleId,
                            ModuleId = s.ModuleId,
                            PAdd = s.PAdd,
                            PEdit = s.PEdit,
                            PDelete = s.PDelete,
                            PView = s.PView,
                            CreatedDate = s.CreatedDate,
                            RoleName = s.RoleName,
                            ModuleName = s.ModuleName
                        });
                    }
                    response.Code = roleInPerms.Code;
                    response.Data = roleInPermView;
                }
            }
            return response;
        }

        public static ResponeMessageBaseType<bool> UpdateRoleInPermission(List<RoleInPermissonView> roleInPermView)
        {
            var response = new ResponeMessageBaseType<bool> { Code = ErrorCodeEnum.SUCCESS, Data = true };
            if (roleInPermView != null)
            {
                RoleInPermissonDTO roleInPerm = new RoleInPermissonDTO();
                foreach (var item in roleInPermView)
                {
                    roleInPerm.RoleId = item.RoleId;
                    roleInPerm.ModuleId = item.ModuleId;
                    roleInPerm.PAdd = item.PAdd;
                    roleInPerm.PEdit = item.PEdit;
                    roleInPerm.PDelete = item.PDelete;
                    roleInPerm.PView = item.PView;
                    ApiServiceUtilities.PostJson("api/RoleInPermissionApi/UpdateRoleInPermission/", roleInPerm);
                }

                string curentEmail = AuthorizationHelper.Email;
                if (!string.IsNullOrEmpty(curentEmail))
                {
                    AuthorizationHelper.AutoUpdatePermForUser(curentEmail);
                }

                return response;
            }
            else
            {
                return response;
            }
        }

        
    }
}
