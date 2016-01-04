using BW.Common.Enums;
using BW.Data.Contract;
using BW.Data.Contract.DTOs;
using BW.Data.Contract.DTOs;
using BW.Website.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Net.Http;


namespace BW.Website.Common.Helpers
{
    public class UserHelper
    {
        public static ResponeMessage<List<UserView>> GetAllUser()
        {
            List<UserView> userDTO = new List<UserView>();
            var response = new ResponeMessage<List<UserView>> { Code = ErrorCodeEnum.SUCCESS, Data = new List<UserView>() };
            HttpResponseMessage reponse = ApiServiceUtilities.GetResponse("api/UserApi/getalluser/");
            if (reponse.IsSuccessStatusCode)
            {
                var users = reponse.Content.ReadAsAsync<ResponeMessage<List<UserDTO>>>().Result;
                foreach (var s in users.Data)
                {
                    userDTO.Add(new UserView
                    {
                        UserId = s.UserId.ToString(),
                        FirstName = s.FirstName,
                        LastName = s.LastName,
                        Email = s.Email,
                        CreatedDate = s.CreatedDate,
                        RoleName = s.RoleName,
                        IsActive = s.IsActive,
                    });
                }
                response.Code = users.Code;
                response.Data = userDTO;
            }
            return response;
        }

        public static ResponeMessage<UserView> GetUserById(string userIdEnc)
        {
            var response = new ResponeMessage<UserView> { Code = ErrorCodeEnum.SUCCESS, Data = new UserView() };
            UserView userView = new UserView();
            if (!string.IsNullOrEmpty(userIdEnc))
            {
                int userId = int.Parse(ApiServiceUtilities.Decrypt(userIdEnc));
                HttpResponseMessage reponse = ApiServiceUtilities.PostParram("api/UserApi/GetUserById/", userId);
                if (reponse.IsSuccessStatusCode)
                {
                    try
                    {
                        var user = reponse.Content.ReadAsAsync<ResponeMessage<UserDTO>>().Result;
                        userView.UserId = user.Data.UserId.ToString();
                        userView.FirstName = user.Data.FirstName;
                        userView.LastName = user.Data.LastName;
                        userView.Email = user.Data.Email;
                        userView.CreatedDate = user.Data.CreatedDate;
                        userView.IsActive = user.Data.IsActive;
                        userView.RoleName = user.Data.RoleName;
                        userView.RoleDTOs = user.Data.RoleDTOs;
                        response.Code = user.Code;
                        response.Data = userView;
                    }
                    catch (Exception e)
                    { }
                }
            }
            return response;
        }

        public static ResponeMessageBaseType<bool> UpdateUser(UserView userView, string[] groupRole)
        {
            var response = new ResponeMessageBaseType<bool> { Code = ErrorCodeEnum.SUCCESS, Data = true };
            if (userView != null)
            {
                // Convert UserInfo to User.
                UserCreateDTO user = new UserCreateDTO();
                user.UserId = int.Parse(ApiServiceUtilities.Decrypt(userView.UserId.ToString()));
                user.FirstName = userView.FirstName;
                user.LastName = userView.LastName;
                user.IsActive = userView.IsActive;
                if (groupRole != null)
                {
                    foreach (var item in groupRole)
                    {
                        user.roles.Add(item);
                    }
                }
                // Update data
                var result = ApiServiceUtilities.PostJson("api/UserApi/UpdateUser/", user);

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

        public static ResponeMessageBaseType<bool> EditProfile(UserProfileView userProfileView)
        {
            var response = new ResponeMessageBaseType<bool> { Code = ErrorCodeEnum.SUCCESS, Data = true };
            if (userProfileView != null)
            {
                // Convert UserInfo to User.
                UserProfileDTO user = new UserProfileDTO();
                user.UserId = AuthorizationHelper.UserId.Value;
                user.FirstName = userProfileView.FirstName;
                user.LastName = userProfileView.LastName;
                
                // Update data
                var result = ApiServiceUtilities.PostJson("api/UserApi/UpdateProfile/", user);

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

        public static ResponeMessageBaseType<bool> InsertUser(UserCreateView userCreateView, string[] groupRole)
        {
            var response = new ResponeMessageBaseType<bool> { Code = ErrorCodeEnum.SUCCESS, Data = true };
            if (userCreateView != null)
            {
                // Convert UserInfo to User.
                UserCreateDTO user = new UserCreateDTO();
                user.UserId = userCreateView.UserId;
                user.FirstName = userCreateView.FirstName;
                user.LastName = userCreateView.LastName;
                user.Email = userCreateView.Email;
                user.Password = ApiServiceUtilities.MD5Hash(userCreateView.Password);
                if (groupRole != null)
                {
                    foreach (var item in groupRole)
                    {
                        user.roles.Add(item);
                    }
                }
                // Update data
                ApiServiceUtilities.PostJson("api/UserApi/InsertUser/", user);
                return response;
            }
            else
            {
                return response;
            }
        }

        public static ResponeMessageBaseType<bool> DeleteUser(string userId)
        {
            var response = new ResponeMessageBaseType<bool> { Code = ErrorCodeEnum.SUCCESS, Data = true };
            UserDTO user = new UserDTO();

            user.UserId = int.Parse(ApiServiceUtilities.Decrypt(userId));

            // Delete data
            ApiServiceUtilities.PostJson("api/UserApi/RemoveUser/", user);
            return response;
        }

        public static ResponeMessageBaseType<bool> UpdatePassword(UserPasswordView userPassView)
        {
            var response = new ResponeMessageBaseType<bool> { Code = ErrorCodeEnum.SUCCESS, Data = true };
            if (userPassView != null)
            {
                // Convert UserInfo to User.
                UserDTO user = new UserDTO();
                user.UserId = AuthorizationHelper.UserId.Value;
                user.Password = ApiServiceUtilities.MD5Hash(userPassView.NewPassword);

                // Update data
                var result = ApiServiceUtilities.PostJson("api/UserApi/UpdatePassword/", user);
                return response;
            }
            else
            {
                return response;
            }
        }

        public static bool CheckOldPassword(string Email, string OldPassword)
        {
            HttpResponseMessage response = ApiServiceUtilities.GetResponse(string.Format("api/UserApi/login/?email={0}&password={1}", Email, OldPassword));
            var result = new ResponeMessage<AuthenticationInfoDTO> { Code = ErrorCodeEnum.SUCCESS, Data = new AuthenticationInfoDTO() };
            if (response.IsSuccessStatusCode)
            {
                result = response.Content.ReadAsAsync<ResponeMessage<AuthenticationInfoDTO>>().Result;

                if (result.Data.UserId != 0)
                {
                    return true;
                }


            }
            return false;
        }
        public static ResponeMessageBaseType<bool> CheckUnitEmail(string email)
        {
            var response = new ResponeMessageBaseType<bool> { Code = ErrorCodeEnum.SUCCESS, Data = true };

            ApiServiceUtilities.PostJson("api/UserApi/CheckUnitEmail?email=", email);
            return response;
        }

    }
}
