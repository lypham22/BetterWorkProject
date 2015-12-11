using BW.Data.Contract.DTOs;
using BW.Data.Contract.DTOs_View;
using System;
using System.Collections.Generic;
using System.Net.Http;


namespace BW.Website.Common.Helpers
{
    class ProductHelper 
    {
        HelpClient Hc = new HelpClient();
        public List<UserInfo> GetAllUser()
        {
            // Call service
           // Convert data model to view model
           // Encrypt ID
            List<UserInfo> UserInfoList = new List<UserInfo>();

            HttpResponseMessage reponse = Hc.GetReponse("api/user");
            if (reponse.IsSuccessStatusCode)
            {

                List<UserDTO> Usertlist = new List<UserDTO>();
                Usertlist = reponse.Content.ReadAsAsync<List<UserDTO>>().Result;
                foreach (UserDTO s in Usertlist)
                {
                    UserInfoList.Add(new UserInfo { UserName = s.UserName, Email = "Email test" });
                }
            }
            else
            {
               
            }
            return UserInfoList;
        }

        public UserViewModels GetUser()
        {
            UserViewModels UVD = new UserViewModels();
            HttpResponseMessage reponse = Hc.GetReponse("api/user");
            if (reponse.IsSuccessStatusCode)
            {

                UserDTO User = new UserDTO();
                User = reponse.Content.ReadAsAsync<UserDTO>().Result;

                UVD.UserName = User.UserName;
                UVD.Id = User.UserId;
                UVD.UserAddress = "test address" ;
                
            }
            else
            {

            }
            return UVD;
        }
    }
}
