using System;
using System.Security.Cryptography;
using System.Text;

using Chess.Core.Authentication;

namespace Chess.Infrastructure.Authentication
{
    public class PasswordHasher : IPasswordHasher
    {
        public string HashPassword(string password)
        {
            var hasher = new SHA512Managed();
            var hashedBytes = hasher.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }
    }
}
