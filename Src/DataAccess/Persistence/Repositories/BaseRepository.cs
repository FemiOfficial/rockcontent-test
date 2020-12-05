using DataAccess.Domain.Repositories;
using DataAccess.Persistence.Contexts;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using DataAccess.Domain.Queries;


namespace DataAccess.Persistence.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected AppDbContext _context;

        public BaseRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
        }

        public async Task<TEntity> Get(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> ListAsync(Query query)
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public void Remove(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);

        }
    }
}