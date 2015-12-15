using BW.Data.Contract.DTOs;
using BW.Data.Contract.DTOViews;
using System;
using System.Collections.Generic;
using System.Net.Http;


namespace BW.Website.Common.Helpers
{
    public class ProductHelper
    {
        public static List<UserInfo> GetAllUser()
        {
            List<UserInfo> userInfo = new List<UserInfo>();

            HttpResponseMessage reponse = HelpClient.GetReponse("api/user/getalluser");
            if (reponse.IsSuccessStatusCode)
            {
                var users = reponse.Content.ReadAsAsync<List<User>>().Result;
                foreach (var s in users)
                {
                    userInfo.Add(new UserInfo { UserId = s.UserId, UserName = s.UserName, Email = s.Email });
                }
            }
            return userInfo;
        }

        public static UserView GetUserById(string userIdEnc)
        {
            UserView userView = new UserView();
            if (!string.IsNullOrEmpty(userIdEnc))
            {
                int userId = int.Parse(userIdEnc);
                HttpResponseMessage reponse = HelpClient.GetReponse("api/user/SearchUser/" + userId);
                if (reponse.IsSuccessStatusCode)
                {
                    User user = reponse.Content.ReadAsAsync<User>().Result;
                    userView.UserId = user.UserId;
                    userView.UserName = user.UserName;
                    userView.Email = user.Email;
                    userView.Password = user.Password;
                }
            }
            return userView;
        }

        public static bool UpdateUser(UserView userView)
        {
            if (userView != null)
            {
                // Convert UserInfo to User.
                User user = new User();
                user.UserId = userView.UserId;
                user.UserName = userView.UserName;
                user.Email = userView.Email;
                user.Password = userView.Password;
                user.CreatedDate = DateTime.Now;
                // Post data
                HelpClient.PostUserInfo("api/user/UpdateUser/", user);
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
