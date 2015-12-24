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
    public class ModuleRepository : RepositoryBase<BW_Module>, IModuleRepository
    {
        public ModuleRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}
