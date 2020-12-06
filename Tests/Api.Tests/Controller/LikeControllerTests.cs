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
using Api.Resources.Request;
using Api.Tests.Resources;
using System.Collections.Generic;
using Api.Resources.Response;
using DataAccess.Domain.Models;

namespace Api.Tests.Controller
{
    public class LikeControllerTests : IntegrationTest
    {
        private readonly ILikeService _mocklikeService;
        private readonly Mock<ILikesRepository> _mocklikerepository;
        private readonly LikesController _likesController;
        private readonly Dictionary<string, string> headerMap;

        private IMapper _mapper;


        public LikeControllerTests()
        {
            _likesController = new LikesController(_mocklikeService, _mapper);
            headerMap = new Dictionary<string, string>();
            headerMap.Add("Content-Type", "application/json");

        }


        [Fact]
        public async void LikePostFeature_HttpCall_ShouldbeSuccessful()
        {

            // Arrange
            var like = new LikeRequestDto()
            {
                RequestIpAddress = "127.0.0.1",
                RequestUserAgent = "Unit-Test",
                RequestUsername = "tester",
                PostId = new Guid().ToString("N"),
                ClientReferenceId = new Guid().ToString()
            };

            // Act
            var response = await DoPostAsync(ApiRoutes.Likes._likePosturl, headerMap, like);
            var jsondata = JsonConvert.DeserializeObject<ApiResponse<LikeResponseDto>>(response);

            // Asset
            Assert.Equal(Helpers.ApiReponseStatusCodes.Created, jsondata.Status);

            Assert.Equal("Successfully liked post", jsondata.Message);
            Assert.Equal(like.RequestUserAgent, jsondata.Data.RequestUserAgent);
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
