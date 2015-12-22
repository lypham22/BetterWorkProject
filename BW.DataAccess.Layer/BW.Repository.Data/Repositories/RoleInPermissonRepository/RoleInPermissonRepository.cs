using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BW.Data.Contract.DTOs;
using BW.Repository.Data.Infrastructure;
using System.Data.SqlClient;
using System.Data;
using BW.Data.Contract;
using BW.Common.Enums;

namespace BW.Repository.Data.Repositories
{
    public class RoleInPermissonRepository : RepositoryBase<BW_RoleInPermission>, IRoleInPermissonRepository
    {
        public RoleInPermissonRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
        private RoleRepository roleRepository;
        private ModuleRepository moduleRepository;
        public ResponeMessage<List<RoleInPermissonDTO>> GetAllRoleInPermission()
        {
            var response = new ResponeMessage<List<RoleInPermissonDTO>> { Code = ErrorCodeEnum.SUCCESS, Data = new List<RoleInPermissonDTO>() };
            roleRepository = new RoleRepository(this.DatabaseFactory);
            moduleRepository = new ModuleRepository(this.DatabaseFactory);
            var result = this.GetAll()
                .Join(roleRepository.GetAll(), l => l.RoleId, r => r.RoleId, (l, r) => new { l, r })
                .Join(moduleRepository.GetAll(), l1 => l1.l.ModuleId, r1 => r1.ModuleId, (l1, r1) => new { l1, r1 })
                .Select(p => new RoleInPermissonDTO
                          {
                              RoleInPermissionId = p.l1.l.RoleInPermissionId,
                              ModuleId = p.l1.l.ModuleId,
                              RoleId = p.l1.l.RoleId,
                              PAdd = p.l1.l.PAdd,
                              PEdit = p.l1.l.PEdit,
                              PDelete = p.l1.l.PDelete,
                              PView = p.l1.l.PView,
                              CreatedDate = (DateTime)p.l1.l.CreatedDate,
                              RoleName = p.l1.r.RoleName,
                              ModuleName = p.r1.ModuleName
                          }).ToList();

            response.Data = result;
            return response;
        }
    }
}
