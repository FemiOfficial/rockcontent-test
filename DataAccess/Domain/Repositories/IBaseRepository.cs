using System;
using System.Threading.Tasks;

namespace DataAccess.Domain.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task AddAsync(TEntity entity);
        Task<TEntity> Get(int id);
        void Update(TEntity entity);
        void Remove(TEntity entity);
    }
}
