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

        public ResponeMessage<List<RoleInPermissonDTO>> GetRoleInPermissionByRoleId(int roleId)
        {
            var response = new ResponeMessage<List<RoleInPermissonDTO>> { Code = ErrorCodeEnum.SUCCESS, Data = new List<RoleInPermissonDTO>() };
            roleRepository = new RoleRepository(this.DatabaseFactory);
            moduleRepository = new ModuleRepository(this.DatabaseFactory);
            var result = this.GetMany(r => r.RoleId == roleId)
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

        public ResponeMessage<RoleInPermissonDTO> GetRoleInPermissionById(int roleInPermissionId)
        {
            var response = new ResponeMessage<RoleInPermissonDTO> { Code = ErrorCodeEnum.SUCCESS, Data = new RoleInPermissonDTO() };
            var result = this.GetById(roleInPermissionId);
            if (result != null)
            {
                response.Data = this.ToRoleInPermissonDTO(result);
                return response;
            }
            return response;
        }
        public ResponeMessageBaseType<bool> UpdateRoleInPermission(RoleInPermissonDTO roleInPerm)
        {
            var response = new ResponeMessageBaseType<bool> { Code = ErrorCodeEnum.SUCCESS, Data = true };
            var roleInPermData = this.GetById(roleInPerm.RoleInPermissionId);
            if (roleInPermData != null)
            {
                roleInPermData.PAdd = roleInPerm.PAdd;
                roleInPermData.PEdit = roleInPerm.PEdit;
                roleInPermData.PDelete = roleInPerm.PDelete;
                roleInPermData.PView = roleInPerm.PView;
                roleInPermData.UpdatedDate = DateTime.Now;

                this.Update(roleInPermData);
                this.DataContext.SaveChanges();
                return response;
            }
            else
            {
                response.Data = false;
                return response;
            }
        }
        private RoleInPermissonDTO ToRoleInPermissonDTO(BW_RoleInPermission obj)
        {
            return new RoleInPermissonDTO()
            {
                RoleInPermissionId = obj.RoleInPermissionId,
                ModuleId = obj.ModuleId,
                RoleId = obj.RoleId,
                PAdd = obj.PAdd,
                PEdit = obj.PEdit,
                PDelete = obj.PDelete,
                PView = obj.PView,
            };
        }
    }
}
