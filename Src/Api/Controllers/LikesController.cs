﻿using System.Threading.Tasks;
using Api.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

using Api.Resources.Request;
using Api.Helpers;
using Api.Resources.Response;
using Api.Exceptions;
using DataAccess.Domain.Queries;
using Api.Middlewares;


namespace Api.Controllers
{
    [Route("api/likefeature")]
    [ApiController]
    public class LikesController : ControllerBase
    {

        private readonly ILikeService _likeService;

        private IMapper _mapper;

        private readonly ICustomValidators _customValidators;

        public LikesController(ILikeService likeService, IMapper mapper,
            ICustomValidators customValidators)
        {
            _mapper = mapper;
            _likeService = likeService;
            _customValidators = customValidators;
        }

        [HttpGet("Ping")]
        public ActionResult Ping()
        {
           return Ok("App is running");

        }


        [HttpPost("LikePost")]
        public async Task<IActionResult> LikePost([FromBody]LikeRequestDto likeRequest)
        {

            try
            {


                // Checking if both values a null because at runtime while running unit/integration test
                // Request.HttpContext.Connection.RemoteIpAddress is null, hence it will throw a System.NullReferenceException
                // Exception

                // Checking for the AccessToken only when request is from an external client (not from in-app integration test)

                if (likeRequest.RequestIpAddress == null && likeRequest.RequestUserAgent == null)
                {
                    likeRequest.RequestIpAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();

                    likeRequest.RequestUserAgent = Request.Headers["User-Agent"].ToString();

                    _customValidators.validateRequestToken(Request.Headers["Token"].ToString(), likeRequest.ClientReferenceId);
                }


                var response = await _likeService.LikePost(likeRequest);

                return Ok(response);


            }
            catch (AppException ex)
            {
                return BadRequest(new ErrorResponse(ex.Message, ApiReponseStatusCodes.BadRequest));
            } 

        }

        [HttpDelete("DislikePost/{postId}")]
        public async Task<ActionResult> DislikePost(int postId, [FromBody]DisLikeRequestDto dislikeRequest)
        {
            try
            {

                dislikeRequest.PostId = postId.ToString();

                var response = await _likeService.DisLikePost(dislikeRequest);

                return Ok(response);

            } catch(AppException ex)
            {
                return BadRequest(new ErrorResponse(ex.Message, ApiReponseStatusCodes.BadRequest));

            }
        }

        [HttpGet("LikesByPost")]
        public async Task<IActionResult> GetLikesForPost([FromQuery] LikesQueryByPostRequestDto likeQueryRequest)
        {

            try
            {
                var likesQuery = _mapper.Map<LikesQueryByPostRequestDto, LikesQuery>(likeQueryRequest);

                var response = await _likeService.GetAllLikesForPost(likesQuery);

                return Ok(response);

            }
            catch (AppException ex)
            {
                return BadRequest(new ErrorResponse(ex.Message, ApiReponseStatusCodes.BadRequest));
            }

        }

        [HttpGet("LikesByClientReferenceId")]
        public async Task<IActionResult> GetLikesForPClientReferenceId([FromQuery]
            LikesQueryByClientReferenceIdRequestDto likeQueryRequest)
        {

            try
            {
                var likesQuery = _mapper.Map<LikesQueryByClientReferenceIdRequestDto,
                    LikesQuery>(likeQueryRequest);

                var response = await _likeService.GetAllLikesForPost(likesQuery);

                return Ok(response);

            }
            catch (AppException ex)
            {
                return BadRequest(new ErrorResponse(ex.Message, ApiReponseStatusCodes.BadRequest));
            }

        }



    }
}
