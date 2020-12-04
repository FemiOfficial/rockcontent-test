using System;
using DataAccess.Domain.Models;
using DataAccess.Domain.Repositories;
using DataAccess.Persistence.Contexts;

namespace DataAccess.Persistence.Repositories
{
    public class UsersRepository : BaseRepository<Users>, IUsersRepository
    {
        public UsersRepository(AppDbContext context) : base(context) { }

    }
}
