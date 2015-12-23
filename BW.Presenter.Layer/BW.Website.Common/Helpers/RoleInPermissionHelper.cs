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
        public static ResponeMessage<List<RoleInPermissonView>> GetAllRoleInPermisson()
        {
            var response = new ResponeMessage<List<RoleInPermissonView>> { Code = ErrorCodeEnum.SUCCESS, Data = new List<RoleInPermissonView>() };
            List<RoleInPermissonView> roleInPermView = new List<RoleInPermissonView>();
            HttpResponseMessage reponse = ApiServiceUtilities.GetReponse("api/RoleInPermissionApi/GetAllRoleInPermisson");
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
            return response;
        }

        public static ResponeMessage<RoleInPermissonView> GetRoleInPermissionById(string roleInPermIdEnc)
        {
            var response = new ResponeMessage<RoleInPermissonView> { Code = ErrorCodeEnum.SUCCESS, Data = new RoleInPermissonView() };
            RoleInPermissonView roleInPermView = new RoleInPermissonView();
            if (!string.IsNullOrEmpty(roleInPermIdEnc))
            {
                int roleInPermId = int.Parse(roleInPermIdEnc);
                HttpResponseMessage reponse = ApiServiceUtilities.GetReponse("api/RoleInPermissionApi/GetRoleInPermissionById/" + roleInPermId);
                if (reponse.IsSuccessStatusCode)
                {
                    var roleInPerm = reponse.Content.ReadAsAsync<ResponeMessage<RoleInPermissonDTO>>().Result;
                    roleInPermView.RoleInPermissionId = roleInPerm.Data.RoleInPermissionId;
                    roleInPermView.PAdd = roleInPerm.Data.PAdd;
                    roleInPermView.PEdit = roleInPerm.Data.PEdit;
                    roleInPermView.PDelete = roleInPerm.Data.PDelete;
                    roleInPermView.PView = roleInPerm.Data.PView;

                    response.Data = roleInPermView;
                }
            }
            return response;
        }

        public static ResponeMessageBaseType<bool> UpdateRoleInPermission(RoleInPermissonView roleInPermView)
        {
            var response = new ResponeMessageBaseType<bool> { Code = ErrorCodeEnum.SUCCESS, Data = true };
            if (roleInPermView != null)
            {
                RoleInPermissonDTO roleInPerm = new RoleInPermissonDTO();
                roleInPerm.RoleInPermissionId = roleInPermView.RoleInPermissionId;
                roleInPerm.PAdd = roleInPermView.PAdd;
                roleInPerm.PEdit = roleInPermView.PEdit;
                roleInPerm.PDelete = roleInPermView.PDelete;
                roleInPerm.PView = roleInPermView.PView;

                ApiServiceUtilities.PostJson("api/RoleInPermissionApi/UpdateRoleInPermission/", roleInPerm);
                return response;
            }
            else
            {
                return response;
            }
        }

        //public static ResponeMessage<List<RoleView>> GetAllRoleMoreInfo()
        //{
        //    var response = new ResponeMessage<List<RoleView>> { Code = ErrorCodeEnum.SUCCESS, Data = new List<RoleView>() };
        //    List<RoleView> roleDTO = new List<RoleView>();
        //    HttpResponseMessage reponse = ApiServiceUtilities.GetReponse("api/RoleApi/GetAllRole");
        //    if (reponse.IsSuccessStatusCode)
        //    {
        //        var roles = reponse.Content.ReadAsAsync<ResponeMessage<List<RoleDTO>>>().Result;
        //        foreach (var s in roles.Data)
        //        {
        //            roleDTO.Add(new RoleView
        //            {
        //                RoleId = s.RoleId,
        //                RoleName = s.RoleName,
        //                RoleDescription = s.RoleDescription,
        //                IsActive = s.IsActive,
        //                CreatedDate = s.CreatedDate
        //            });
        //        }
        //        response.Code = roles.Code;
        //        response.Data = roleDTO;
        //    }
        //    return response;
        //}

        //public static ResponeMessage<RoleView> GetRoleById(string roleIdEnc)
        //{
        //    var response = new ResponeMessage<RoleView> { Code = ErrorCodeEnum.SUCCESS, Data = new RoleView() };
        //    RoleView roleView = new RoleView();
        //    if (!string.IsNullOrEmpty(roleIdEnc))
        //    {
        //        int roleId = int.Parse(roleIdEnc);
        //        HttpResponseMessage reponse = ApiServiceUtilities.GetReponse("api/RoleApi/GetRoleById/" + roleId);
        //        if (reponse.IsSuccessStatusCode)
        //        {
        //            var role = reponse.Content.ReadAsAsync<ResponeMessage<RoleDTO>>().Result;
        //            roleView.RoleId = role.Data.RoleId;
        //            roleView.RoleName = role.Data.RoleName;
        //            roleView.RoleDescription = role.Data.RoleDescription;
        //            roleView.IsActive = role.Data.IsActive;
        //            roleView.CreatedDate = role.Data.CreatedDate;
        //            response.Data = roleView;
        //        }
        //    }
        //    return response;
        //}

        //public static ResponeMessageBaseType<bool> InsertRole(RoleCreateView roleCreateView)
        //{
        //    var response = new ResponeMessageBaseType<bool> { Code = ErrorCodeEnum.SUCCESS, Data = true };
        //    if (roleCreateView != null)
        //    {
        //        RoleCreateDTO role = new RoleCreateDTO();
        //        role.RoleId = roleCreateView.RoleId;
        //        role.RoleName = roleCreateView.RoleName;
        //        role.RoleDescription = roleCreateView.RoleDescription;

        //        ApiServiceUtilities.PostJson("api/RoleApi/InsertRole/", role);
        //        return response;
        //    }
        //    else
        //    {
        //        return response;
        //    }
        //}
        

        //public static ResponeMessageBaseType<bool> DeleteRole(int roleId)
        //{
        //    var response = new ResponeMessageBaseType<bool> { Code = ErrorCodeEnum.SUCCESS, Data = true };
        //    RoleDTO role = new RoleDTO();
        //    role.RoleId = roleId;
        //    ApiServiceUtilities.PostJson("api/RoleApi/RemoveRole/", role);
        //    return response;
        //}
    }
}
