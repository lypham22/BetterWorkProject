using BW.Repository.Data;
using BW.Repository.Data.Repositories;
using System.Collections.Generic;
using System.Web.Http;
using System.Linq;
using BW.Data.Contract.DTOs;

namespace BW.Services.Api.Controllers
{
    public class UserApiController : ApiController
    {
        private readonly IUserRepository userRepository;
        public UserApiController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public List<UserDTO> GetAllUser()
        {
            var data = userRepository.GetAllUser();
            return data;
        }

        public BW_User GetUserById(int id)
        {
            var data = userRepository.GetById(id);
            return data;
        }
        public bool InsertUser(UserCreateDTO user)
        {
            return userRepository.CreateUser(user);
        }
        //public bool UpdateUser(UserCreateView user)
        //{
        //    return userRepository.UpdateUser(user);
        //}

        //public bool RemoveUser(User user)
        //{
        //    return userRepository.DeleteUser(user.UserId);
        //}
    }
}
