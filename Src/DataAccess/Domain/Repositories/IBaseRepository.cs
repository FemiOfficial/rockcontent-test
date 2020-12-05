using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess.Domain.Queries;

namespace DataAccess.Domain.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task AddAsync(TEntity entity);
        Task<TEntity> Get(int id);
        void Update(TEntity entity);
        Task<IEnumerable<TEntity>> ListAsync(Query query);
        void Remove(TEntity entity);
    }
}
