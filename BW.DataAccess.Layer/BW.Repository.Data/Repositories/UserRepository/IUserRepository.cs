using BW.Data.Contract.DTOs;
using BW.Repository.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BW.Repository.Data.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        List<User> GetAllUser();
        bool CreateUser(User user);
        bool UpdateUser(User user);
        bool DeleteUser(int userId);
        List<User> SPGetAllUser();
        List<User> GetAllUserAndRole();
    }   

}
