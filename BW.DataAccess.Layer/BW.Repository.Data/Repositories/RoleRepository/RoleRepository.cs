using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BW.Data.Contract.DTOs;
using BW.Repository.Data.Infrastructure;
using System.Data.SqlClient;
using System.Data;

namespace BW.Repository.Data.Repositories
{
    public class RoleRepository : RepositoryBase<BW_Role>, IRoleRepository
    {
        public RoleRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        public List<RoleDTO> GetAllRole()
        {
            var result = this.GetAll()
                          .Select(u => new RoleDTO
                          {
                              RoleId = u.RoleId,
                              RoleName = u.RoleName
                          }).ToList();
            return result;
        }
    }
}
