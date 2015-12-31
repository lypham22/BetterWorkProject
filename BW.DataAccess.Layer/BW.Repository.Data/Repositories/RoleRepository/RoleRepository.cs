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
    public class RoleRepository : RepositoryBase<BW_Role>, IRoleRepository
    {
        public RoleRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        private RoleInPermissonRepository roleInPermission;
        private ModuleRepository moduleRepository;

        public ResponeMessage<List<RoleDTO>> GetRoleActive()
        {
            var response = new ResponeMessage<List<RoleDTO>> { Code = ErrorCodeEnum.SUCCESS, Data = new List<RoleDTO>() };
            var result = this.GetMany(r => r.IsActive == true)
                          .Select(u => new RoleDTO
                          {
                              RoleId = u.RoleId,
                              RoleName = u.RoleName,
                              RoleDescription = u.RoleDescription,
                              IsActive = u.IsActive,
                              CreatedDate = (DateTime)u.CreatedDate
                          }).ToList();
            response.Data = result;
            response.Message = "GetAllRole Success";
            return response;
        }

        public ResponeMessage<List<RoleDTO>> GetAllRoleMore()
        {
            var response = new ResponeMessage<List<RoleDTO>> { Code = ErrorCodeEnum.SUCCESS, Data = new List<RoleDTO>() };
            var result = this.GetAll()
                          .Select(u => new RoleDTO
                          {
                              RoleId = u.RoleId,
                              RoleName = u.RoleName,
                              RoleDescription = u.RoleDescription,
                              IsActive = u.IsActive,
                              CreatedDate = (DateTime)u.CreatedDate
                          }).ToList();

            roleInPermission = new RoleInPermissonRepository(this.DatabaseFactory);
            List<ModuleDTO> module;
            StringBuilder builder;
            foreach (var item in result)
            {
                module = roleInPermission.GetMany(r => r.RoleId == item.RoleId && r.BW_Module.IsActive == true 
                    && (r.PAdd == true || r.PView == true || r.PEdit == true || r.PDelete == true)).Select(u => new ModuleDTO
                {
                    ModuleName = u.BW_Module.ModuleName
                }).ToList();
                builder = new StringBuilder();
                foreach (var r in module)
                {
                    builder.Append(r.ModuleName).Append(", ");
                }

                item.ModuleName = !string.IsNullOrEmpty(builder.ToString()) ? builder.Remove(builder.Length - 2, 1).ToString() : string.Empty;
            }
            response.Data = result;
            response.Message = "GetAllRoleMore Success";
            return response;
        }
        public ResponeMessage<RoleDTO> GetRoleById(int roleId)
        {
            var response = new ResponeMessage<RoleDTO> { Code = ErrorCodeEnum.SUCCESS, Data = new RoleDTO() };
            var result = this.GetById(roleId);
            if (result != null)
            {
                RoleDTO roleDTOs = this.ToRoleDTO(result);
                roleInPermission = new RoleInPermissonRepository(this.DatabaseFactory);
                List<ModuleDTO> modules = roleInPermission.GetMany(r => r.RoleId == roleId && r.BW_Module.IsActive == true).Select(u => new ModuleDTO
                {
                    ModuleId = u.BW_Module.ModuleId,
                    ModuleName = u.BW_Module.ModuleName   
                }).ToList();

                response.Data = roleDTOs;
                response.Data.ModuleDTOs = modules;
                response.Data.ModuleName = string.Join(", ", roleDTOs.ModuleDTOs.Select(x => x.ModuleName).ToArray());
                response.Message = "GetUserById Success";
                return response;
            }
            return response;
        }

        public ResponeMessageBaseType<bool> CreateRole(RoleCreateDTO role)
        {
            var response = new ResponeMessageBaseType<bool> { Code = ErrorCodeEnum.SUCCESS, Data = true };
            BW_Role r = new BW_Role();
            r.RoleName = role.RoleName;
            r.RoleDescription = role.RoleDescription;
            r.CreatedDate = DateTime.Now;
            r.IsActive = true;
            this.Add(r);
            this.DataContext.SaveChanges();

            moduleRepository = new ModuleRepository(this.DatabaseFactory);
            foreach (var item in moduleRepository.GetAll().ToList())
            {
                string sql = @"INSERT INTO BW_RoleInPermission(ModuleId, RoleId, PAdd, PEdit, PDelete, PView, CreatedDate) VALUES({0}, {1}, {2}, {3}, {4}, {5}, {6})";
                DataContext.Database.ExecuteSqlCommand(sql, item.ModuleId, r.RoleId, false, false, false, false, DateTime.Now);
            }

            response.Data = true;

            return response;
        }
        public ResponeMessageBaseType<bool> UpdateRole(RoleCreateDTO role)
        {
            var response = new ResponeMessageBaseType<bool> { Code = ErrorCodeEnum.SUCCESS, Data = true };
            var roleData = this.GetById(role.RoleId);
            if (roleData != null)
            {
                roleData.RoleName = role.RoleName;
                roleData.RoleDescription = role.RoleDescription;
                roleData.UpdatedDate = DateTime.Now;
                roleData.IsActive = role.IsActive;
                this.Update(roleData);
                this.DataContext.SaveChanges();
                return response;
            }
            else
            {
                response.Data = false;
                return response;
            }
        }

        public ResponeMessageBaseType<bool> DeleteRole(int roleId)
        {
            var response = new ResponeMessageBaseType<bool> { Code = ErrorCodeEnum.SUCCESS, Data = true };
            var roleData = this.GetById(roleId);
            if (roleData != null)
            {
                string delete = "DELETE FROM BW_UserInRole WHERE RoleId = " + roleId;
                DataContext.Database.ExecuteSqlCommand(delete);

                string delete1 = "DELETE FROM BW_RoleInPermission WHERE RoleId = " + roleId;
                DataContext.Database.ExecuteSqlCommand(delete1);

                this.Delete(roleData);
                this.DataContext.SaveChanges();

                return response;
            }
            else
            {
                response.Data = false;
                return response;
            }

        }
        private RoleDTO ToRoleDTO(BW_Role obj)
        {
            return new RoleDTO()
            {
                RoleId = obj.RoleId,
                RoleName = obj.RoleName,
                RoleDescription = obj.RoleDescription,
                CreatedDate = (DateTime)obj.CreatedDate,
                IsActive = obj.IsActive
            };
        }
    }
}
