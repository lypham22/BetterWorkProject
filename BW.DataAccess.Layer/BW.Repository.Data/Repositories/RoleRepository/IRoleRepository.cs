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
    public interface IRoleRepository : IRepository<BW_Role>
    {
        ResponeMessage<List<RoleDTO>> GetRoleActive();
        ResponeMessage<List<RoleDTO>> GetAllRoleMore();
        ResponeMessage<RoleDTO> GetRoleById(int roleId);
        ResponeMessageBaseType<bool> CreateRole(RoleCreateDTO role);
        ResponeMessageBaseType<bool> UpdateRole(RoleCreateDTO role);
        ResponeMessageBaseType<bool> DeleteRole(int roleId);
    }

}
