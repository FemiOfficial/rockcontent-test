﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Domain.Models;
using DataAccess.Domain.Repositories;
using DataAccess.Persistence.Contexts;
using DataAccess.Domain.Queries;


namespace DataAccess.Persistence.Repositories
{
    public class LikesRepository : BaseRepository<Likes>, ILikesRepository
    {
        public LikesRepository(AppDbContext context) : base(context) { }


        public async Task<QueryResult<Likes>> ListAsync(LikesQuery query)
        {
            IQueryable<Likes> queryable = _context.Likes.AsNoTracking();

            if (!String.IsNullOrEmpty(query.RequestIpAddress))
            {
                queryable = queryable.Where(w => w.RequestIpAddress == query.RequestIpAddress);
            }

            if (!String.IsNullOrEmpty(query.RequestUserAgent))
            {
                queryable = queryable.Where(w => w.RequestUserAgent == query.RequestUserAgent);
            }

            if (!String.IsNullOrEmpty(query.RequestUsername))
            {
                queryable = queryable.Where(w => w.RequestUsername == query.RequestUsername);
            }

            if (!String.IsNullOrEmpty(query.ClientReferenceId))
            {
                queryable = queryable.Where(w => w.ClientReferenceId == query.ClientReferenceId);
            }

            if (!String.IsNullOrEmpty(query.PostId))
            {
                queryable = queryable.Where(w => w.PostId == query.PostId);
            }

            int totalItems = await queryable.CountAsync();

            List<Likes> likes = await queryable.OrderByDescending(w => w.CreatedOn).Skip((query.ViewPage - 1) * query.ItemsPerPage)
                                                    .Take(query.ItemsPerPage)
                                                    .ToListAsync();

            return new QueryResult<Likes>
            {
                Items = likes,
                TotalItems = totalItems,
                ViewPage = query.ViewPage,
                ItemsPerPage = query.ItemsPerPage,
            };



        }

        public async Task<Likes> GetLikeWithIpAddressAndUserAgentForPost(string IpAddress, string UserAgent,
            string clientRefId, string PostId, string username)
        {
            Likes like = await _context.Likes.AsNoTracking().FirstOrDefaultAsync(x =>
                (x.RequestIpAddress == IpAddress && x.RequestUserAgent == UserAgent && x.PostId == PostId && x.ClientReferenceId == clientRefId) ||
                (x.RequestUsername == username
                && x.PostId == PostId && x.ClientReferenceId == clientRefId));

            return like;
        }

        public async Task<Likes> GetLikeWithUsernameForPost(string username, string postId)
        {
            Likes like = await _context.Likes.AsNoTracking().FirstOrDefaultAsync(x => x.RequestUsername == username && x.PostId == postId);

            return like;
        }

        public async Task<Likes> GetLikeWithUsernameAndClientReferenceIdForPost(string username, string postId, string clientReferenceId)
        {
            Likes like = await _context.Likes.AsNoTracking().FirstOrDefaultAsync(x => x.RequestUsername == username
                && x.PostId == postId && x.ClientReferenceId == clientReferenceId);

            return like;
        }

        public async Task<Likes> GetLikesByPostId(string postId)
        {
            var like = await _context.Likes.AsNoTracking().FirstOrDefaultAsync(x => x.PostId == postId );

            return like;
        }

        
    }
}
