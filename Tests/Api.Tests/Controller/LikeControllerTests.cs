using System;
using System.Net;
using System.Net.Http;
using Xunit;
using Api.Services;
using Api.Controllers;
using AutoMapper;
using DataAccess.Domain.Repositories;
using Moq;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using Api.Communication.Request;
using Api.Tests.Resources;
using System.Collections.Generic;
using Api.Communication.Response;
using Api.Middlewares;
using Api.Helpers;
using Microsoft.Extensions.Configuration;

namespace Api.Tests.Controller
{
    public class LikeControllerTests : IntegrationTest
    {
        private readonly ILikeService _mocklikeService;
        private readonly ICustomValidators _mockcustomValidators;
        private readonly Mock<ILikesRepository> _mocklikerepository;
        private readonly LikesController _likesController;
        private readonly Dictionary<string, string> headerMap;
        
        private readonly IConfiguration configuration;


        private IMapper _mapper;


        public LikeControllerTests()
        {
            _likesController = new LikesController(_mocklikeService, _mapper, _mockcustomValidators);
            headerMap = new Dictionary<string, string>();

            configuration = InitConfiguration();

        }


        [Fact]
        public async void LikePostFeature_HttpCall_ShouldbeSuccessful()
        {

            // Arrange
            var like = new LikeRequestDto()
            {
                RequestUsername = "tester",
                PostId = new Guid().ToString("N"),
                ClientReferenceId = new Guid().ToString()
            };

            


            var secretkey = configuration.GetSection("AppSettings:SECRET_KEY").Value;




            var accessToken = Utilities.CreateHmacToken(like.ClientReferenceId, secretkey);

            headerMap.Add("Token", accessToken);

            // Act
            var response = await DoPostAsync(ApiRoutes.Likes._likePosturl, headerMap, like);
            var jsondata = JsonConvert.DeserializeObject<ApiResponse<LikeResponseDto>>(response);

            // Asset
            Assert.Equal(Helpers.ApiReponseStatusCodes.Created, jsondata.Status);

            Assert.Equal("Successfully liked post", jsondata.Message);
           
            Assert.Equal("00000000-0000-0000-0000-000000000000", jsondata.Data.ClientReferenceId);

        }


        [Fact]
        public void TestPingApp()
        {
            var result = _likesController.Ping();

            Assert.IsType<OkObjectResult>(result);

            var objectResult = result as ObjectResult;

            Assert.Equal(200, objectResult.StatusCode);

        }






    }

}
