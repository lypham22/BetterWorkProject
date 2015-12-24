using BW.Repository.Data;
using BW.Repository.Data.Repositories;
using System.Collections.Generic;
using System.Web.Http;
using System.Linq;
using BW.Data.Contract.DTOs;
using BW.Data.Contract;
using BW.Common.Enums;
using Hmac.Api.Filters;

namespace BW.Services.Api.Controllers
{
    [Authenticate]
    public class UserApiController : ApiController
    {
        private readonly IUserRepository userRepository;
        public UserApiController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public ResponeMessage<List<UserDTO>> GetAllUser()
        {
            var response = new ResponeMessage<List<UserDTO>> { Code = ErrorCodeEnum.SUCCESS, Data = new List<UserDTO>() };
            var result = userRepository.GetAllUser();
            response.Data = result.Data;
            return response;
        }

        [HttpPost]
        public ResponeMessage<UserDTO> GetUserById([FromBody]int id)
        {
            return  userRepository.GetUserById(id);
        }
        public ResponeMessageBaseType<bool> InsertUser(UserCreateDTO user)
        {
            return userRepository.CreateUser(user);
        }
        public ResponeMessageBaseType<bool> UpdateUser(UserCreateDTO user)
        {
            return userRepository.UpdateUser(user);
        }

        public ResponeMessageBaseType<bool> RemoveUser(UserDTO user)
        {
            return userRepository.DeleteUser(user.UserId);
        }
        [HttpGet]
        public ResponeMessage<AuthenticationInfoDTO> Login(string email, string password)
        {
            return userRepository.Login(email, password);
        }
    }
}
