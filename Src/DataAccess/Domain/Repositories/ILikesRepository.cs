using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using DataAccess.Domain.Models;
using DataAccess.Domain.Queries;

namespace DataAccess.Domain.Repositories
{
    public interface ILikesRepository : IBaseRepository<Likes>
    {
        Task<Likes> GetLikesByPostId(string postId);
        Task<QueryResult<Likes>> ListAsync(LikesQuery query);
        Task<Likes> GetLikeWithIpAddressAndUserAgentForPost(string IpAddress, string UserAgent, string clientRefId, string postId, string username);
        Task<Likes> GetLikeWithUsernameForPost(string username, string postId);
        Task<Likes> GetLikeWithUsernameAndClientReferenceIdForPost(string username,
            string postId, string clientReferenceId);

    }
}
