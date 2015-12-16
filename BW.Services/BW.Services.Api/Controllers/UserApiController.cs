using BW.Repository.Data;
using BW.Repository.Data.Repositories;
using System.Collections.Generic;
using System.Web.Http;
using System.Linq;

namespace BW.Services.Api.Controllers
{
    public class UserApiController : ApiController
    {
        private readonly IUserRepository userRepository;
        public UserApiController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public List<User> GetAllUser()
        {
            var data = userRepository.GetAllUser();
            return data;
        }

        public User GetUserById(int id)
        {
            var data = userRepository.GetById(id);
            return data;
        }

        public bool UpdateUser(User user)
        {
            var result = false;
            if (user.UserId == 0)
            {
                result = userRepository.CreateUser(user);
            }
            else
            {
                result = userRepository.UpdateUser(user);
            }

            return result;
        }

        public bool RemoveUser(User user)
        {
            return userRepository.DeleteUser(user.UserId);
        }
    }
}
