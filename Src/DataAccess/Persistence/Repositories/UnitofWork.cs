using System;
using DataAccess.Domain.Repositories;
using System.Threading.Tasks;
using DataAccess.Persistence.Contexts;

namespace DataAccess.Persistence.Repositories
{
    public class UnitofWork : IUnitofWork
    {
        private readonly AppDbContext _dbContext;

        public UnitofWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            Likes = new LikesRepository(dbContext);


        }

        public ILikesRepository Likes
        {
            get; private set;
        }

        public async Task<int> CompleteAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
