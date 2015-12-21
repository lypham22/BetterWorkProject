using BW.Repository.Data;
using BW.Repository.Data.Repositories;
using System.Collections.Generic;
using System.Web.Http;
using System.Linq;
using BW.Data.Contract.DTOs;
using BW.Data.Contract;

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
            return roleInPermissonRepository.GetAllRoleInPermission(); ;
        }

        //public ResponeMessage<RoleDTO> GetRoleById(int id)
        //{
        //    return roleRepository.GetRoleById(id);
        //}
        //public ResponeMessageBaseType<bool> InsertRole(RoleCreateDTO role)
        //{
        //    return roleRepository.CreateRole(role);
        //}
        //public ResponeMessageBaseType<bool> UpdateRole(RoleCreateDTO role)
        //{
        //    return roleRepository.UpdateRole(role);
        //}

        //public ResponeMessageBaseType<bool> RemoveRole(RoleDTO role)
        //{
        //    return roleRepository.DeleteRole(role.RoleId);
        //}
    }
}
