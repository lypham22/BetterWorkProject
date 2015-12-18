using BW.Repository.Data;
using BW.Repository.Data.Repositories;
using System.Collections.Generic;
using System.Web.Http;
using System.Linq;
using BW.Data.Contract.DTOs;

namespace BW.Services.Api.Controllers
{
    public class RoleApiController : ApiController
    {
        private readonly IRoleRepository roleRepository;
        public RoleApiController(IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }
        public List<RoleDTO> GetAllRole()
        {
            var data = roleRepository.GetAllRole();
            return data;
        }

    }
}
