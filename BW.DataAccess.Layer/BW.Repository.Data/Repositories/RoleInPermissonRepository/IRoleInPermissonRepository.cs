﻿using BW.Data.Contract;
using BW.Data.Contract.DTOs;
using BW.Repository.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BW.Repository.Data.Repositories
{
    public interface IRoleInPermissonRepository : IRepository<BW_RoleInPermission>
    {
        ResponeMessage<List<RoleInPermissonDTO>> GetAllRoleInPermission();
        //ResponeMessage<RoleDTO> GetRoleById(int roleId);
        //ResponeMessageBaseType<bool> CreateRole(RoleCreateDTO role);
        //ResponeMessageBaseType<bool> UpdateRole(RoleCreateDTO role);
        //ResponeMessageBaseType<bool> DeleteRole(int roleId);
    }

}