using BW.Repository.Data;
using BW.Repository.Data.Repositories;
using System.Collections.Generic;
using System.Web.Http;
using System.Linq;
using BW.Data.Contract.DTOs;
using BW.Data.Contract;
using BW.Common.Enums;

namespace BW.Services.Api.Controllers
{
    public class RoleInPermissionApiController : ApiController
    {
        private readonly IRoleInPermissonRepository roleInPermissonRepository;
        public RoleInPermissionApiController(IRoleInPermissonRepository roleInPermissonRepository)
        {
            this.roleInPermissonRepository = roleInPermissonRepository;
        }
        public ResponeMessage<List<RoleInPermissonDTO>> GetAllRoleInPermisson()
        {
            return roleInPermissonRepository.GetAllRoleInPermission();
        }
        public ResponeMessage<List<RoleInPermissonDTO>> GetRoleInPermissionByRoleId(int id)
        {
            return roleInPermissonRepository.GetRoleInPermissionByRoleId(id);
        }
        public ResponeMessage<RoleInPermissonDTO> GetRoleInPermissionById(int id)
        {
            return roleInPermissonRepository.GetRoleInPermissionById(id);
        }
        public ResponeMessageBaseType<bool> UpdateRoleInPermission(RoleInPermissonDTO roleInPerm)
        {
            return roleInPermissonRepository.UpdateRoleInPermission(roleInPerm);
        }
    }
}
