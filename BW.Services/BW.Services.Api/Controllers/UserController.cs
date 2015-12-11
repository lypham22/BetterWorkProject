using BW.Data.Contract.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BW.Services.Api.Controllers
{
    public class UserController : ApiController
    {
        public List<UserDTO> GetAllUser()
        {
            List<UserDTO> ListUser = new List<UserDTO>();
            ListUser.Add(new UserDTO { UserId = 1, UserName = "user1" });
            ListUser.Add(new UserDTO { UserId = 2, UserName = "user2" });

            return ListUser;
        }
        [HttpGet]
        public UserDTO SearchUser(int id)
        {
            UserDTO user = new UserDTO { UserId = 3, UserName = "user3" };

            return user;
        }

        [HttpGet]
        public UserDTO AddUser(UserDTO newuser)
        {
            UserDTO user = new UserDTO { UserId = 3, UserName = "user4" };

            return user;
        }
    }
}
