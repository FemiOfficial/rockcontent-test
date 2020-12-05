using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.HttpOverrides;
using System.Threading.Tasks;
using Api.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Api.Resources.Request.LikesRequest;
using Api.Helpers;
using Api.Resources.Response;
using Api.Exceptions;
using DataAccess.Domain.Queries;


namespace Api.Controllers
{
    [Route("api/likefeature")]
    [ApiController]
    public class LikesController : ControllerBase
    {

        private readonly ILikeService _likeService;

        private readonly IConfiguration _config;
        private readonly ILogger _logger;
        private IMapper _mapper;

        public LikesController(ILikeService likeService, ILogger<LikesController> logger,
            IConfiguration config, IMapper mapper)
        {
            _config = config;
            _logger = logger;
            _mapper = mapper;
            _likeService = likeService;
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

                likeRequest.RequestIpAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();

                likeRequest.RequestUserAgent = Request.Headers["User-Agent"].ToString();

                var response = await _likeService.LikePost(likeRequest);

                return Ok(response);


            }
            catch (AppException ex)
            {
                return BadRequest(new ErrorResponse(ex.Message, ApiReponseStatusCodes.BadRequest));
            } 

        }

        [HttpGet("Likes")]
        public async Task<IActionResult> GetLikes([FromQuery] LikesQueryRequestDto likeQueryRequest)
        {

            try
            {
                var likesQuery = _mapper.Map<LikesQueryRequestDto, LikesQuery>(likeQueryRequest);

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
