namespace Chess.Core.Models
{
    using System;

    using Chess.Core.Authentication;

    public class User
    {
        private readonly ITokenGenerator _tokenGenerator;

        public User(ITokenGenerator tokenGenerator)
        {
            if (tokenGenerator == null)
            {
                throw new ArgumentNullException("tokenGenerator");
            }

            _tokenGenerator = tokenGenerator;
        }

        public string GetAuthenticationToken()
        {
            return _tokenGenerator.GetAuthenticationToken(this);
        }

        public string Username { get; private set; }

        public string Password { get; private set; }
    }
}
