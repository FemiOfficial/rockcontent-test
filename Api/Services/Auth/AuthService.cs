using System;
using System.Threading.Tasks;
using Api.Resources.Request.AuthDtos;
using Api.Resources.Response;
using DataAccess.Domain.Repositories;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Microsoft.Extensions.Configuration;

namespace Api.Services.Auth
{
    public class AuthService : IAuthService
    {

        private readonly IUsersRepository _userRepository;
        private readonly IUnitofWork _unitofWork;

        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;


        public AuthService(IUsersRepository usersRepository, IUnitofWork unitofWork,
            ILogger<AuthService> logger, IConfiguration configuration)
        {



        }

        public Task<ApiResponse<UserSignInDto>> Login(UserSignInDto signincredentials)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<UserRegistrationDto>> Register(UserRegistrationDto user, string password)
        {
            throw new NotImplementedException();
        }
    }
}
