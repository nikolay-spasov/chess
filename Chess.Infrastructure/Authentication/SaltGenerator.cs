using System;
using System.Security.Cryptography;

using Chess.Core.Authentication;

namespace Chess.Infrastructure.Authentication
{
    public class SaltGenerator : ISaltGenerator
    {
        public string GenerateSalt(int length = 64)
        {
            var randomArray = new byte[length];

            var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(randomArray);
            var randomString = Convert.ToBase64String(randomArray);

            return randomString;
        }
    }
}
