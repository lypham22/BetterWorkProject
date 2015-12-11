using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BW.Repository.Data.Infrastructure
{
    public interface IDatabaseFactory : IDisposable
    {
        BWDataContext Get();
    }
}
