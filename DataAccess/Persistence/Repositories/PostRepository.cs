using System;
using DataAccess.Domain.Models;
using DataAccess.Domain.Repositories;
using DataAccess.Persistence.Contexts;

namespace DataAccess.Persistence.Repositories
{
    public class PostsRepository : BaseRepository<Posts>, IPostsRepository
    {
        public PostsRepository(AppDbContext context) : base(context) { }

    }
}
