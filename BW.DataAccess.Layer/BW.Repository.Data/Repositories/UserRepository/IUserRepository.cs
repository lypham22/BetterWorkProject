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
        List<UserDTO> GetAllUser();
        List<BW_Role> GetAllRole();
        //UserDetailsDTO GetUserById(int userId);
        bool CreateUser(UserCreateDTO user);
        bool UpdateUser(BW_User user);
        bool DeleteUser(int userId);
        List<BW_User> SPGetAllUser();
        List<BW_UserInRole> GetAllUserAndRole();
    }   

}
