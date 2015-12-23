using BW.Repository.Data;
using BW.Repository.Data.Repositories;
using System.Collections.Generic;
using System.Web.Http;
using System.Linq;
using BW.Data.Contract.DTOs;
using BW.Data.Contract;

namespace BW.Services.Api.Controllers
{
    public class RoleApiController : ApiController
    {
        private readonly IRoleRepository roleRepository;
        public RoleApiController(IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }
        public ResponeMessage<List<RoleDTO>> GetRoleActive()
        {
            return roleRepository.GetRoleActive();;
        }
        public ResponeMessage<List<RoleDTO>> GetAllRoleMore()
        {
            return roleRepository.GetAllRoleMore();
        }

        public ResponeMessage<RoleDTO> GetRoleById(int id)
        {
            return roleRepository.GetRoleById(id);
        }
        public ResponeMessageBaseType<bool> InsertRole(RoleCreateDTO role)
        {
            return roleRepository.CreateRole(role);
        }
        public ResponeMessageBaseType<bool> UpdateRole(RoleCreateDTO role)
        {
            return roleRepository.UpdateRole(role);
        }

        public ResponeMessageBaseType<bool> RemoveRole(RoleDTO role)
        {
            return roleRepository.DeleteRole(role.RoleId);
        }
    }
}

