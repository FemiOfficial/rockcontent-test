using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Api.Helpers
{

    public interface IUtilities
    {
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
    }

    public class Utilities : IUtilities
    {
        private readonly IHttpContextAccessor _httContextAccessor;

        public Utilities(IHttpContextAccessor httpContextAccessor)
        {
            _httContextAccessor = httpContextAccessor;
        }

        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            }
        }

        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        public int GetUserIdFromJWToken()
        {
            return int.Parse(_httContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        }

        public string GetFullNameFromJWToken()
        {
            return _httContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
        }
    }
}
