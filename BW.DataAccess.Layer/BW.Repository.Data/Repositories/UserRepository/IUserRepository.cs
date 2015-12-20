using BW.Data.Contract;
using BW.Data.Contract.DTOs;
using BW.Repository.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BW.Repository.Data.Repositories
{
    public interface IUserRepository : IRepository<BW_User>
    {
        ResponeMessage<List<UserDTO>> GetAllUser();
        ResponeMessage<UserDTO> GetUserById(int userId);
        ResponeMessageBaseType<bool> CreateUser(UserCreateDTO user);
        ResponeMessageBaseType<bool> UpdateUser(BW_User user);
        ResponeMessageBaseType<bool> DeleteUser(int userId);
        ResponeMessage<AuthenticationInfoDTO> Login(string email, string password);
    }   

}
