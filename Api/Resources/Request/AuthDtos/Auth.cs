using System;
namespace Api.Resources.Request.AuthDtos
{
    public class UserRegistrationDto
    {
        public string Username { get; set; }
        public string Fullname { get; set; }
        public string Password { get; set; }
    }

    public class UserSignInDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}


