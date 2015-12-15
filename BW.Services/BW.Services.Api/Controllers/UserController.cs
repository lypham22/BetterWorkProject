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
            return data;
        }

        public bool UpdateUser(User user)
        {
            if (user.UserId == 0)
            {
                userRepository.CreateUser(user);
            }
            else
            {
                userRepository.Update(user);
            }

            return true;
        }

        public void DeleteUser(User user)
        {
            userRepository.DeleteUser(user);

        }
    }
}
