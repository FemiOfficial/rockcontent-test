using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess.Domain.Models;
using DataAccess.Domain.Queries;

namespace DataAccess.Domain.Repositories
{
    public interface ILikesRepository : IBaseRepository<Likes>
    {
        Task<QueryResult<Likes>> ListAsync(LikesQuery query);
        Task<Likes> GetLikeWithIpAddressAndUserAgentForPost(string IpAddress, string UserAgent, string postId);
        Task<Likes> GetLikeWithUsernameForPost(string username, string postId);
            
    }
}
