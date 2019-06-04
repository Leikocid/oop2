using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Lab11.DAL {
    interface IRepository<TEntity> where TEntity : class {
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");
        TEntity FindById(int id);
        void Create(TEntity item);
        void Update(TEntity item);
        void Delete(int id);
        void Delete(TEntity item);
    }
}
