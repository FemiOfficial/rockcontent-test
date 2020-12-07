using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Api.Helpers
{

    public interface IUtilities
    {
        string CreateHmacToken(string message, string key);
        bool VerifyHmacToken(string clientHmacToken, string clearRefId, string key);
    }

    public class Utilities : IUtilities
    {
       
        public Utilities() { }


        public string CreateHmacToken(string message, string secret)
        {

            var encoding = new System.Text.ASCIIEncoding();
            byte[] keyByte = encoding.GetBytes(secret);
            byte[] messageBytes = encoding.GetBytes(message);
            using (var hmacsha256 = new HMACSHA256(keyByte))
            {
                byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
                return Convert.ToBase64String(hashmessage);
            }
        }

        public bool VerifyHmacToken(string clientHmacToken, string clearRefId, string key)
        {
            if(CreateHmacToken(clearRefId, key) == clientHmacToken)
            {
                return true;
            }

            return false;
        }
    }
}
