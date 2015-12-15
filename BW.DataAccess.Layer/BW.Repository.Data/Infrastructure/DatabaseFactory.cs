using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BW.Repository.Data.Infrastructure
{
public class DatabaseFactory : Disposable, IDatabaseFactory
{
    private BetterWorkEntities dataContext;
    public BetterWorkEntities Get()
    {
        return new BetterWorkEntities();
    }
    protected override void DisposeCore()
    {
        if (dataContext != null)
            dataContext.Dispose();
    }
}
}
