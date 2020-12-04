using System;
using System.Threading.Tasks;

namespace DataAccess.Domain.Repositories
{
    public interface IUnitofWork : IDisposable
    {
        ILikesRepository Likes { get; }
        IPostsRepository Posts { get; }
        IUsersRepository Users { get; }

        Task<int> CompleteAsync();
    }
}
