using BW.Repository.Data;
using BW.Repository.Data.Repositories;
using System.Collections.Generic;
using System.Web.Http;

namespace BW.Services.Api.Controllers
{
    public class UserController : ApiController
    {
        private readonly IUserRepository userRepository;
        public UserController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public List<User> GetAllUser()
        {
            var data = userRepository.GetAllUser();
            //List<UserDTO> ListUser = new List<UserDTO>();
            //ListUser.Add(new UserDTO { UserId = 1, UserName = "user1111" });
            //ListUser.Add(new UserDTO { UserId = 2, UserName = "user2" });

            return data;
        }
        [HttpGet]
        public User SearchUser(int id)
        {
            User user = userRepository.GetById(id);          
            return user;
        }

        //[HttpGet]
        public User AddUser(User newuser)
        {
           // UserDTO user = new UserDTO { UserId = 3, UserName = "user4" };
            userRepository.CreateUser(newuser);
            return newuser;
        }
    }
}
