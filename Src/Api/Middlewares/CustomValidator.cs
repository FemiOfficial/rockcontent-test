using Api.Helpers;
using Api.Exceptions;
using Microsoft.Extensions.Configuration;

namespace Api.Middlewares
{

    public interface ICustomValidators
    {
        void validateRequestToken(string accessToken, string clientReferenceId);
    }

    public class CustomValidators : ICustomValidators
    {

       
        private readonly IConfiguration _configuration;
        private string secretkey;


        public CustomValidators(IConfiguration configuration)
        {

            _configuration = configuration;
            secretkey = _configuration.GetSection("AppSettings:SECRET_KEY").Value;


        }

        public void validateRequestToken(string accessToken, string clientReferenceId)
        {


            if (accessToken == null || accessToken == "")
            {
                throw new AppException("No Access token provided");
            }
            else if (Utilities.VerifyHmacToken(accessToken, clientReferenceId, secretkey) == false)
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
