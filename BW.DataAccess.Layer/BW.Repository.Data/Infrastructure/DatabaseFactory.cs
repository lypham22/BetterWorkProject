using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BW.Repository.Data.Infrastructure
{
public class DatabaseFactory : Disposable, IDatabaseFactory
{
    private BWDataContext dataContext;
    public BWDataContext Get()
    {
        return dataContext ?? (dataContext = new BWDataContext("DefaultConnection"));
    }
    protected override void DisposeCore()
    {
        if (dataContext != null)
            dataContext.Dispose();
    }
}
}
