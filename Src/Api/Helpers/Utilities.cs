using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;

namespace Api.Helpers
{


    public static class Utilities
    {
    
        public static string CreateHmacToken(string message, string secretkey)
        {

            var encoding = new System.Text.ASCIIEncoding();
            byte[] keyByte = encoding.GetBytes(secretkey);
            byte[] messageBytes = encoding.GetBytes(message);
            using (var hmacsha256 = new HMACSHA256(keyByte))
            {
                byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
                return Convert.ToBase64String(hashmessage);
            }
        }

        public static bool VerifyHmacToken(string clientHmacToken, string clearRefId, string key)
        {
            if(CreateHmacToken(clearRefId, key) == clientHmacToken)
            {
                return true;
            }

            return false;
        }
    }
}
