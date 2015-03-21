namespace Chess.Infrastructure.Authentication
{
    using System;
    using System.Security.Claims;
    using Microsoft.Owin.Security;

    using Chess.Core.Authentication;
    using Chess.Core.Models;
    using Chess.Core.Repository;
    using Chess.Core.Settings;

    public class UserManager : IUserManager
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthenticationSettings _authenticationSettings;

        public UserManager(IUserRepository userRepository, IAuthenticationSettings authenticationSettings)
        {
            if (userRepository == null)
            {
                throw new ArgumentNullException("userRepository");
            }

            if (authenticationSettings == null)
            {
                throw new ArgumentNullException("authenticationSettings");
            }

            _userRepository = userRepository;
            _authenticationSettings = authenticationSettings;
        }

        public User Validate(string username, string password)
        {
            return _userRepository.GetByUsernameAndPassword(username, password);
        }

        public AuthenticationTicket GetAuthenticationTicket(User user, string authenticationType)
        {
            var identity = new ClaimsIdentity(authenticationType);
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));


            var ticket = new AuthenticationTicket(identity, new AuthenticationProperties());

            var currentUtc = DateTime.UtcNow;
            ticket.Properties.IssuedUtc = currentUtc;
            ticket.Properties.ExpiresUtc = currentUtc.Add(_authenticationSettings.TokenExpirationLength);

            return ticket;
        }
    }
}
