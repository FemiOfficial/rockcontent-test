using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Communication.Request;
using Api.Communication.Response;
using DataAccess.Domain.Models;
using DataAccess.Domain.Queries;

namespace Api.Services

{
    public interface ILikeService
    {

        Task<ApiResponse<LikeResponseDto>> LikePost(LikeDto likeRequest);
        Task<ApiResponse<LikeResponseDto>> DisLikePost(DisLikeRequestDto disLikeRequest);

        Task<ApiResponse<LikesQueryResult<Likes>>> GetAllLikesForPost(LikesQuery query);
        



    }
}
