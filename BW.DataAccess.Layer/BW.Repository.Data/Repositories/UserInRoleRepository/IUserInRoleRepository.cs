using BW.Data.Contract.DTOs;
using BW.Repository.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BW.Repository.Data.Repositories
{
    public interface IUserInRoleRepository : IRepository<BW_UserInRole>
    {
        bool AddUserInRole(BW_UserInRole userInRole);
    }   

}
