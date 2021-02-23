using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;

namespace QuinelaWeb.EntityFramework.Repositories
{
    public abstract class QuinelaWebRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<QuinelaWebDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected QuinelaWebRepositoryBase(IDbContextProvider<QuinelaWebDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //add common methods for all repositories
    }

    public abstract class QuinelaWebRepositoryBase<TEntity> : QuinelaWebRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected QuinelaWebRepositoryBase(IDbContextProvider<QuinelaWebDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //do not add any method here, add to the class above (since this inherits it)
    }
}
