using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Resources.Request.LikesRequest;
using Api.Resources.Response;
using Api.Resources.Response.LikesResponse;
using DataAccess.Domain.Models;
using DataAccess.Domain.Queries;

namespace Api.Services

{
    public interface ILikeService
    {

        Task<ApiResponse<LikeResponseDto>> LikePost(LikeRequestDto likeRequest);
        Task<string> DisLikePost(DisLikeRequestDto disLikeRequest);

        Task<ApiResponse<LikesQueryResult<Likes>>> GetAllLikesForPost(LikesQuery query);
        



    }
}
