using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace RepositoryPatternSample.Core.Repositories
{
    public interface IRepository<TEntity> where TEntity : class, IPersistentEntity
    {
        TEntity Get(int id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> expression);
        
        void Add(TEntity entity);
        void Remove(TEntity entity);
    }
}
