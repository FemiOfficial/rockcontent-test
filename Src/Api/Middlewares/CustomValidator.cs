using System;
using Microsoft.Extensions.Configuration;
using Api.Helpers;
using Api.Exceptions;

namespace Api.Middlewares
{

    public interface ICustomValidators
    {
        void validateRequestToken(string accessToken, string clientReferenceId);
    }

    public class CustomValidators : ICustomValidators
    {
        private readonly IConfiguration _configuration;

        private readonly IUtilities _utilities;

        public CustomValidators(IConfiguration configuration, IUtilities utilities)
        {
            _configuration = configuration;
            _utilities = utilities;
        }

        public void validateRequestToken(string accessToken, string clientReferenceId)
        {

            var key = _configuration.GetSection("AppSettings:SECRET_KEY").Value;

            if (accessToken == null || accessToken == "")
            {
                throw new AppException("No Access token provided");
            }
            else if (_utilities.VerifyHmacToken(accessToken, clientReferenceId, key) == false)
            {
                throw new AppException("Invalid Access token provided");

            }
            else
            {
                return;
            }
        }
    }
}
