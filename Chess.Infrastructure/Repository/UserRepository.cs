﻿namespace Chess.Infrastructure.Repository
{
    using System;
    using System.Collections.Generic;

    using Chess.Core.Models;
    using Chess.Core.Repository;

    public class UserRepository : IUserRepository
    {
        public User GetByUsernameAndPassword(string username, string password)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public User GetById()
        {
            throw new NotImplementedException();
        }
    }
}
