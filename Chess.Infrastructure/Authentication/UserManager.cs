namespace Chess.Infrastructure.Authentication
{
    using System;

    using Chess.Core.Authentication;
    using Chess.Core.Repository;
    using Chess.Core.Models;

    public class UserManager : IUserManager
    {
        private readonly IUserRepository _userRepository;

        public UserManager(IUserRepository userRepository)
        {
            if (userRepository == null)
            {
                throw new ArgumentNullException("userRepository");
            }

            _userRepository = userRepository;
        }

        public User Validate(string username, string password)
        {
            return _userRepository.GetByUsernameAndPassword(username, password);
        }
    }
}
