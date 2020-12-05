using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Exceptions;
using Api.Resources.Request.LikesRequest;
using Api.Resources.Response.LikesResponse;
using AutoMapper;
using DataAccess.Domain.Repositories;
using DataAccess.Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Api.Resources.Response;
using DataAccess.Domain.Queries;

namespace Api.Services
{
    public class LikeService : ILikeService
    {

        private readonly IConfiguration _configuration;

        private readonly IUnitofWork _unitOfWork;

        private readonly ILikesRepository _likeRepository;

        private readonly ILogger _logger;

        private readonly IMapper _mapper;

        public LikeService(ILogger<LikeService> logger, IMapper mapper,IUnitofWork unitofWork,
            ILikesRepository likesRepository, IConfiguration configuration)
        {
            _configuration = configuration;
            _unitOfWork = unitofWork;
            _likeRepository = likesRepository;
            _logger = logger;
            _mapper = mapper;


        }

        public async Task<ApiResponse<LikeResponseDto>> LikePost(LikeRequestDto likeRequest)
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
                    likeRequest.RequestUserAgent, likeRequest.PostId);

                var isLikedFromUsername = await LikedExistFromUsername(likeRequest.RequestUsername, likeRequest.PostId);

             

                if (isLikedFromClient || isLikedFromUsername)
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


        public Task<string> DisLikePost(DisLikeRequestDto disLikeRequest)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse<LikesQueryResult<Likes>>> GetAllLikesForPost(LikesQuery query)
        {

            ApiResponse<LikesQueryResult<Likes>> response = new ApiResponse<LikesQueryResult<Likes>>();
            var resultMessage = "";


            try
            {
                QueryResult<Likes> result = await _likeRepository.ListAsync(query);

                if(result == null)
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


        private async Task<bool> LikedExistFromClient(string IpAddress, string UserAgent, string postId)
        {
            if (await _likeRepository.GetLikeWithIpAddressAndUserAgentForPost(IpAddress, UserAgent, postId) == null)
            {
                return false;
            }

            return true;
        }

        private async Task<bool> LikedExistFromUsername(string username, string postId)
        {
            if (await _likeRepository.GetLikeWithUsernameForPost(username, postId) == null)
            {
                return false;
            }

            return true;
        }
    }
}
