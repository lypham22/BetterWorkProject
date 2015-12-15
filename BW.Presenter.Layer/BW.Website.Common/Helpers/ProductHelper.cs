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
                    UserInfoList.Add(new UserInfo { Name = s.UserName, Email = s.Email });
                }
            }
            else
            {
               
            }
            return UserInfoList;
        }

        public static UserViewModels GetUser(int? userid)
        {
            UserViewModels UVD = new UserViewModels();
            HttpResponseMessage reponse = HelpClient.GetReponse("api/user/SearchUser/"+ userid);
            if (reponse.IsSuccessStatusCode)
            {

                User User = new User();
                User = reponse.Content.ReadAsAsync<User>().Result;

                UVD.UserName = User.UserName;
                UVD.Id = User.UserId;
                UVD.UserAddress = "test address" ;
                
            }
            else
            {

            }
            return UVD;
        }

        public static bool CreateUser(UserInfo user)
        {
            // Convert UserInfo to User.
            User u = new User();
            u.UserName = user.Name;
            u.UserId = user.Id;
            // Post data
           HelpClient.PostUserInfo("api/user/AddUser/", u);

            return true;
        }
    }
}
