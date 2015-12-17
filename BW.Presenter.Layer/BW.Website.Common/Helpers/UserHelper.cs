using BW.Data.Contract.DTOs;
using System;
using System.Collections.Generic;
using System.Net.Http;


namespace BW.Website.Common.Helpers
{
    public class UserHelper
    {
        public static List<UserDTO> GetAllUser()
        {
            List<UserDTO> userDTO = new List<UserDTO>();

            HttpResponseMessage reponse = ApiServiceUtilities.GetReponse("api/UserApi/getalluser");
            if (reponse.IsSuccessStatusCode)
            {
                var users = reponse.Content.ReadAsAsync<List<UserDTO>>().Result;
                foreach (var s in users)
                {
                    userDTO.Add(new UserDTO
                    {
                        UserId = s.UserId,
                        FirstName = s.FirstName,
                        LastName = s.LastName, 
                        Email = s.Email, 
                        CreatedDate = s.CreatedDate, 
                        RoleName = s.RoleName, 
                        IsActive = s.IsActive 
                    });
                }
            }
            return userDTO;
        }

        //public static UserView GetUserById(string userIdEnc)
        //{
        //    UserView userView = new UserView();
        //    if (!string.IsNullOrEmpty(userIdEnc))
        //    {
        //        int userId = int.Parse(userIdEnc);
        //        HttpResponseMessage reponse = ApiServiceUtilities.GetReponse("api/UserApi/GetUserById/" + userId);
        //        if (reponse.IsSuccessStatusCode)
        //        {
        //            User user = reponse.Content.ReadAsAsync<User>().Result;
        //            userView.UserId = user.UserId;
        //            userView.UserName = user.UserName;
        //            userView.Email = user.Email;
        //            userView.Password = user.Password;
        //        }
        //    }
        //    return userView;
        //}

        //public static bool UpdateUser(UserEditView userView)
        //{
        //    if (userView != null)
        //    {
        //        // Convert UserInfo to User.
        //        User user = new User();
        //        user.UserId = userView.UserId;
        //        user.UserName = userView.UserName;
        //        user.Email = userView.Email;
        //        user.UpdatedDate = DateTime.Now;
        //        // Post data
        //        var response = ApiServiceUtilities.PostJson("api/UserApi/UpdateUser/", user);
        //        return response.IsSuccessStatusCode;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        public static bool InsertUser(UserCreateView userCreateView)
        {
            if (userCreateView != null)
            {
                // Convert UserInfo to User.
                UserCreateDTO user = new UserCreateDTO();
                user.UserId = userCreateView.UserId;
                user.FirstName = userCreateView.FirstName;
                user.LastName = userCreateView.LastName;
                user.Email = userCreateView.Email;
                user.Password = userCreateView.Password;
                user.RoleId = userCreateView.RoleId;
                // Post data
                var response = ApiServiceUtilities.PostJson("api/UserApi/InsertUser/", user);
                return response.IsSuccessStatusCode;
            }
            else
            {
                return false;
            }
        }

        //public static bool DeleteUser(int userId)
        //{
        //    if (userId != null)
        //    {
        //        // Convert UserInfo to User.
        //        User user = new User();
        //        user.UserId = userId;
        //        // Post data
        //        ApiServiceUtilities.PostJson("api/UserApi/RemoveUser/", user);
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }

        //}
    }
}
