using System;
using DataAccess.Domain.Models;
using DataAccess.Domain.Repositories;
using DataAccess.Persistence.Contexts;

namespace DataAccess.Persistence.Repositories
{
    public class LikesRepository : BaseRepository<Likes>, ILikesRepository
    {
        public LikesRepository(AppDbContext context) : base(context) { }

    }
}
