using System;
using Xunit;
using Api.Services;
using Api.Controllers;
using AutoMapper;
using DataAccess.Domain.Repositories;
using Moq;
using Microsoft.AspNetCore.Mvc;

namespace Api.Tests.Controller
{
    public class LikeControllerTests
    {
        private readonly ILikeService _mocklikeService;
        private readonly Mock<ILikesRepository> _mocklikerepository;
        private readonly LikesController _likesController;

        private IMapper _mapper;


        public LikeControllerTests()
        { 
            _likesController = new LikesController(_mocklikeService, _mapper);
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
