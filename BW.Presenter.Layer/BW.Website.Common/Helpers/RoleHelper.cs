using BW.Data.Contract.DTOs;
using System;
using System.Collections.Generic;
using System.Net.Http;


namespace BW.Website.Common.Helpers
{
    public class RoleHelper
    {
        public static List<RoleListView> GetAllRole()
        {
            List<RoleListView> roleDTO = new List<RoleListView>();
            HttpResponseMessage reponse = ApiServiceUtilities.GetReponse("api/RoleApi/GetAllRole");
            if (reponse.IsSuccessStatusCode)
            {
                var roles = reponse.Content.ReadAsAsync<List<RoleDTO>>().Result;
                foreach (var s in roles)
                {
                    roleDTO.Add(new RoleListView
                    {
                        RoleId = s.RoleId,
                        RoleName = s.RoleName,
                    });
                }
            }
            return roleDTO;
        }

    }
}
