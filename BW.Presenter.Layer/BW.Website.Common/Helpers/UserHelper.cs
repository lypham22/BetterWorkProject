﻿using BW.Common.Enums;
using BW.Data.Contract;
using BW.Data.Contract.DTOs;
using System;
using System.Collections.Generic;
using System.Net.Http;


namespace BW.Website.Common.Helpers
{
    public class UserHelper
    {
        public static ResponeMessage<List<UserDTO>> GetAllUser()
        {
            List<UserDTO> userDTO = new List<UserDTO>();
            var response = new ResponeMessage<List<UserDTO>> { Code = ErrorCodeEnum.SUCCESS, Data = new List<UserDTO>() };
            HttpResponseMessage reponse = ApiServiceUtilities.GetReponse("api/UserApi/getalluser");
            if (reponse.IsSuccessStatusCode)
            {
                var users = reponse.Content.ReadAsAsync <ResponeMessage<List<UserDTO>>>().Result;
                foreach (var s in users.Data)
                {
                    userDTO.Add(new UserDTO
                    {
                        UserId = s.UserId,
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

        public static ResponeMessage<UserDTO> GetUserById(string userIdEnc)
        {
            var response = new ResponeMessage<UserDTO> { Code = ErrorCodeEnum.SUCCESS, Data = new UserDTO() };
            UserDTO userView = new UserDTO();
            if (!string.IsNullOrEmpty(userIdEnc))
            {
                int userId = int.Parse(userIdEnc);
                HttpResponseMessage reponse = ApiServiceUtilities.GetReponse("api/UserApi/GetUserById/" + userId);
                if (reponse.IsSuccessStatusCode)
                {
                    var user = reponse.Content.ReadAsAsync<ResponeMessage<UserDTO>>().Result;
                    userView.UserId = user.Data.UserId;
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
            }
            return response;
        }

        public static ResponeMessageBaseType<bool> UpdateUser(UserCreateView userView, string[] groupRole)
        {
            var response = new ResponeMessageBaseType<bool> { Code = ErrorCodeEnum.SUCCESS, Data = true };
            if (userView != null)
            {
                // Convert UserInfo to User.
                UserCreateDTO user = new UserCreateDTO();
                user.UserId = userView.UserId;
                user.Password = userView.Password;
                user.FirstName = userView.FirstName;
                user.LastName = userView.LastName;
                user.Email = userView.Email;
                user.CreatedDate = userView.CreatedDate;
                user.IsActive = userView.IsActive;
                if (groupRole.Length > 0)
                {
                    foreach (var item in groupRole)
                    {
                        user.roles.Add(item);
                    }
                }
                // Update data
                var result = ApiServiceUtilities.PostJson("api/UserApi/UpdateUser/", user);
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
                user.Password = userCreateView.Password;
                if (groupRole.Length > 0)
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

        public static ResponeMessageBaseType<bool> DeleteUser(int userId)
        {
            var response = new ResponeMessageBaseType<bool> { Code = ErrorCodeEnum.SUCCESS, Data = true };
            UserDTO user = new UserDTO();
            user.UserId = userId;
            // Delete data
            ApiServiceUtilities.PostJson("api/UserApi/RemoveUser/", user);
            return response;
        }
    }
}
