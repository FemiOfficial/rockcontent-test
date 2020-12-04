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
            Users = new UsersRepository(dbContext);
            Posts = new PostsRepository(dbContext);
            Likes = new LikesRepository(dbContext);


        }

        public ILikesRepository Likes
        {
            get; private set;
        }

        public IPostsRepository Posts
        {
            get; private set;
        }

        public IUsersRepository Users
        {
            get; private set;
        }

        public Task<int> CompleteAsync()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
