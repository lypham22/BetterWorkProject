using BW.Data.Contract.DTOs;
using BW.Data.Contract.DTOs_View;
using System;
using System.Collections.Generic;
using System.Net.Http;


namespace BW.Website.Common.Helpers
{
    public class ProductHelper 
    {
        public static List<UserInfo> GetAllUser()
        {
            // Call service
           // Convert data model to view model
           // Encrypt ID
            List<UserInfo> UserInfoList = new List<UserInfo>();

            HttpResponseMessage reponse = HelpClient.GetReponse("api/user/getalluser");
            if (reponse.IsSuccessStatusCode)
            {

                //List<User> Usertlist = new List<User>();
                var Usertlist = reponse.Content.ReadAsAsync<List<User>>().Result;
                foreach (var s in Usertlist)
                {
                    UserInfoList.Add(new UserInfo { Name = s.Name, Email = "Email test" });
                }
            }
            else
            {
               
            }
            return UserInfoList;
        }

        public static UserViewModels GetUser()
        {
            UserViewModels UVD = new UserViewModels();
            HttpResponseMessage reponse = HelpClient.GetReponse("api/user");
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
