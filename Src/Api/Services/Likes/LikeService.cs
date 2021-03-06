﻿using System;
using System.Threading.Tasks;
using Api.Exceptions;
using Api.Communication.Request;
using Api.Communication.Response;
using AutoMapper;
using DataAccess.Domain.Repositories;
using DataAccess.Domain.Models;
using Microsoft.Extensions.Logging;
using DataAccess.Domain.Queries;

namespace Api.Services
{
    public class LikeService : ILikeService
    {

        private readonly IUnitofWork _unitOfWork;

        private readonly ILikesRepository _likeRepository;

        private readonly ILogger _logger;

        private readonly IMapper _mapper;

        public LikeService(ILogger<LikeService> logger, IMapper mapper,IUnitofWork unitofWork,
            ILikesRepository likesRepository)
        {
            _unitOfWork = unitofWork;
            _likeRepository = likesRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ApiResponse<LikeResponseDto>> LikePost(LikeDto likeRequest)
        {
            // Do a check on the Request Ip Address and User-Agent to ensure a like action is not
            // Initiated twice from the same user on the same device to avoid spam
            // Also do a check on the RequestUsername supplied from the client, to ensure a particular user
            // Does not Like twice (just incase request is sent from same user with the a different device)

            ApiResponse<LikeResponseDto> result = new ApiResponse<LikeResponseDto>();
            var resultMessage = "";



            Likes like = _mapper.Map<Likes>(likeRequest);


            try
            {
                var isLikedFromClient = await LikedExistFromClient(likeRequest.RequestIpAddress,
                    likeRequest.RequestUserAgent, likeRequest.ClientReferenceId, likeRequest.PostId, likeRequest.RequestUsername);


                if (isLikedFromClient)
                {
                    resultMessage = $"Post with Id: {likeRequest.PostId} has be been Liked from {resultMessage} Ip Address:: {likeRequest.RequestIpAddress} by Username:: {likeRequest.RequestUsername} (Action is permitted only once) ";

                    _logger.LogInformation(resultMessage);

                    throw new AppException(resultMessage);
                }

                like.CreatedOn = DateTime.Now;
                await _likeRepository.AddAsync(like);
                await _unitOfWork.CompleteAsync();


                resultMessage = "Successfully liked post";
                result.Status = Helpers.ApiReponseStatusCodes.Created;
                result.Message = resultMessage;

                result.Data = new LikeResponseDto
                {
                    RequestIpAddress = like.RequestIpAddress,
                    RequestUserAgent = like.RequestUserAgent,
                    PostId = like.PostId,
                    ClientReferenceId = like.ClientReferenceId,
                    RequestUsername = like.RequestUsername
                };


                return result;


            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.StackTrace);
                _logger.Log(LogLevel.Error, ex.Message);

                throw new AppException(ex.Message);
            }





        }

        public async Task<ApiResponse<LikeResponseDto>> DisLikePost(DisLikeRequestDto disLikeRequest)
        {
            ApiResponse<LikeResponseDto> result = new ApiResponse<LikeResponseDto>();
            var resultMessage = "";

            // To Dislike a PostId previously liked, the Like record saved to Db is
            // Deleted from Db

            try
            {
                Likes like = await GetLikeToDisLike(disLikeRequest.PostId,
                    disLikeRequest.ClientReferenceId, disLikeRequest.RequestUsername);

                if (like == null)
                {
                    resultMessage = "Could not fetch Like to Dislike";
                    _logger.LogInformation(resultMessage);
                    throw new AppException(resultMessage);
                }

                _likeRepository.Remove(like);
                await _unitOfWork.CompleteAsync();

                resultMessage = $"Successfully dislike PostId: {disLikeRequest.PostId}";

                result.Message = resultMessage;
                result.Status = Helpers.ApiReponseStatusCodes.Success;
                result.Data = null;

                return result;

            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.StackTrace);
                _logger.Log(LogLevel.Error, ex.Message);

                throw new AppException(ex.Message);
            }





        }

        public async Task<ApiResponse<LikesQueryResult<Likes>>> GetAllLikesForPost(LikesQuery query)
        {

            ApiResponse<LikesQueryResult<Likes>> response = new ApiResponse<LikesQueryResult<Likes>>();
            var resultMessage = "";


            try
            {
                QueryResult<Likes> result = await _likeRepository.ListAsync(query);

                if(result == null || result.TotalItems == 0)
                {
                    resultMessage = "Coult not find any likes for specified query";
                    throw new AppException(resultMessage);
                }

                resultMessage = "Data Retrieved";
                response.Message = resultMessage;
                response.Status = Helpers.ApiReponseStatusCodes.Success;

                response.Data = new LikesQueryResult<Likes>
                {
                    TotalLikes = result.TotalItems,
                    Likes = result.Items,
                    ViewPage = result.ViewPage,
                    LikesPerPage = result.ItemsPerPage

                };

                return response;

            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.StackTrace);
                _logger.Log(LogLevel.Error, ex.Message);

                throw new AppException(ex.Message);
            }

            
        }

        private async Task<bool> LikedExistFromClient(string IpAddress, string UserAgent, string clientRefId, string postId, string username)
        {
            if (await _likeRepository.GetLikeWithIpAddressAndUserAgentForPost(IpAddress, UserAgent, clientRefId, postId, username) == null)
            {
                return false;
            }

            return true;
        }

        private async Task<bool> LikedExistFromUsername(string username, string postId, string clientRefId)
        {
            if (await _likeRepository.GetLikeWithUsernameAndClientReferenceIdForPost(username, postId, clientRefId) == null)
            {
                return false;
            }

            return true;
        }

        private async Task<Likes> GetLikeToDisLike(string postId, string clientRefId, string username)
        {
            return await _likeRepository.GetLikeWithUsernameAndClientReferenceIdForPost(username, postId, clientRefId);
        }
    }
}
