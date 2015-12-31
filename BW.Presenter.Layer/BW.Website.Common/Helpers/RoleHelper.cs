using BW.Common.Enums;
using BW.Data.Contract;
using BW.Data.Contract.DTOs;
using BW.Website.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Net.Http;


namespace BW.Website.Common.Helpers
{
    public class RoleHelper
    {
        public static ResponeMessage<List<RoleListView>> GetAllRole()
        {
            var response = new ResponeMessage<List<RoleListView>> { Code = ErrorCodeEnum.SUCCESS, Data = new List<RoleListView>() };
            List<RoleListView> roleDTO = new List<RoleListView>();
            HttpResponseMessage reponse = ApiServiceUtilities.GetResponse("api/RoleApi/GetAllRoleMore");
            if (reponse.IsSuccessStatusCode)
            {
                var roles = reponse.Content.ReadAsAsync<ResponeMessage<List<RoleDTO>>>().Result;
                foreach (var s in roles.Data)
                {
                    roleDTO.Add(new RoleListView
                    {
                        RoleId = s.RoleId,
                        RoleName = s.RoleName,
                    });
                }
                response.Code = roles.Code;
                response.Data = roleDTO;
            }
            return response;
        }
        public static ResponeMessage<List<RoleListView>> GetRoleActive()
        {
            var response = new ResponeMessage<List<RoleListView>> { Code = ErrorCodeEnum.SUCCESS, Data = new List<RoleListView>() };
            List<RoleListView> roleDTO = new List<RoleListView>();
            HttpResponseMessage reponse = ApiServiceUtilities.GetResponse("api/RoleApi/GetRoleActive");
            if (reponse.IsSuccessStatusCode)
            {
                var roles = reponse.Content.ReadAsAsync<ResponeMessage<List<RoleDTO>>>().Result;
                foreach (var s in roles.Data)
                {
                    roleDTO.Add(new RoleListView
                    {
                        RoleId = s.RoleId,
                        RoleName = s.RoleName,
                    });
                }
                response.Code = roles.Code;
                response.Data = roleDTO;
            }
            return response;
        }

        public static ResponeMessage<List<RoleView>> GetAllRoleMoreInfo()
        {
            var response = new ResponeMessage<List<RoleView>> { Code = ErrorCodeEnum.SUCCESS, Data = new List<RoleView>() };
            List<RoleView> roleDTO = new List<RoleView>();
            HttpResponseMessage reponse = ApiServiceUtilities.GetResponse("api/RoleApi/GetAllRoleMore");
            if (reponse.IsSuccessStatusCode)
            {
                var roles = reponse.Content.ReadAsAsync<ResponeMessage<List<RoleDTO>>>().Result;
                foreach (var s in roles.Data)
                {
                    roleDTO.Add(new RoleView
                    {
                        RoleId = s.RoleId,
                        RoleName = s.RoleName,
                        RoleDescription = s.RoleDescription,
                        IsActive = s.IsActive,
                        CreatedDate = s.CreatedDate,
                        ModuleName = s.ModuleName
                    });
                }
                response.Code = roles.Code;
                response.Data = roleDTO;
            }
            return response;
        }

        public static ResponeMessage<RoleView> GetRoleById(string roleIdEnc)
        {
            var response = new ResponeMessage<RoleView> { Code = ErrorCodeEnum.SUCCESS, Data = new RoleView() };
            RoleView roleView = new RoleView();
            if (!string.IsNullOrEmpty(roleIdEnc))
            {
                int roleId = int.Parse(roleIdEnc);
                HttpResponseMessage reponse = ApiServiceUtilities.GetResponse("api/RoleApi/GetRoleById/" + roleId);
                if (reponse.IsSuccessStatusCode)
                {
                    var role = reponse.Content.ReadAsAsync<ResponeMessage<RoleDTO>>().Result;
                    roleView.RoleId = role.Data.RoleId;
                    roleView.RoleName = role.Data.RoleName;
                    roleView.RoleDescription = role.Data.RoleDescription;
                    roleView.IsActive = role.Data.IsActive;
                    roleView.CreatedDate = role.Data.CreatedDate;
                    roleView.ModuleName = role.Data.ModuleName;
                    response.Data = roleView;
                }
            }
            return response;
        }

        public static ResponeMessageBaseType<bool> InsertRole(RoleCreateView roleCreateView)
        {
            var response = new ResponeMessageBaseType<bool> { Code = ErrorCodeEnum.SUCCESS, Data = true };
            if (roleCreateView != null)
            {
                RoleCreateDTO role = new RoleCreateDTO();
                role.RoleId = roleCreateView.RoleId;
                role.RoleName = roleCreateView.RoleName;
                role.RoleDescription = roleCreateView.RoleDescription;

                ApiServiceUtilities.PostJson("api/RoleApi/InsertRole/", role);
                return response;
            }
            else
            {
                return response;
            }
        }
        public static ResponeMessageBaseType<bool> UpdateRole(RoleCreateView roleCreateView)
        {
            var response = new ResponeMessageBaseType<bool> { Code = ErrorCodeEnum.SUCCESS, Data = true };
            if (roleCreateView != null)
            {
                RoleCreateDTO role = new RoleCreateDTO();
                role.RoleId = roleCreateView.RoleId;
                role.RoleName = roleCreateView.RoleName;
                role.RoleDescription = roleCreateView.RoleDescription;
                role.IsActive = roleCreateView.IsActive;
                ApiServiceUtilities.PostJson("api/RoleApi/UpdateRole/", role);
                return response;
            }
            else
            {
                return response;
            }
        }

        public static ResponeMessageBaseType<bool> DeleteRole(int roleId)
        {
            var response = new ResponeMessageBaseType<bool> { Code = ErrorCodeEnum.SUCCESS, Data = true };
            RoleDTO role = new RoleDTO();
            role.RoleId = roleId;
            ApiServiceUtilities.PostJson("api/RoleApi/RemoveRole/", role);
            return response;
        }
    }
}
