using System;
using Api.Resources.Response;
using Api.Resources.Request.AuthDtos;
using System.Threading.Tasks;

namespace Api.Services.Auth
{
    public interface IAuthService
    {
        Task<ApiResponse<UserRegistrationDto>> Register(UserRegistrationDto user, string password);
        Task<ApiResponse<UserSignInDto>> Login(UserSignInDto signincredentials);
    }
}
