namespace Chess.Infrastructure.Repository
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using AutoMapper;
    using Dapper;

    using Chess.Core.Authentication;
    using Chess.Core.Models;
    using Chess.Core.Repository;
    using Chess.Infrastructure.Database;
    using Chess.Infrastructure.Database.Entities;

    public class UserRepository : IUserRepository
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly ISaltGenerator _saltGenerator;
        private readonly IDatabaseConnectionProvider _connectionProvider;

        public UserRepository(IDatabaseConnectionProvider connectionProvider,
            IPasswordHasher passwordHasher, ISaltGenerator saltGenerator)
        {
            if (connectionProvider == null)
            {
                throw new ArgumentNullException("connectionProvider");
            }

            if (passwordHasher == null)
            {
                throw new ArgumentNullException("passwordHasher");
            }

            if (saltGenerator == null)
            {
                throw new ArgumentNullException("saltGenerator");
            }

            _connectionProvider = connectionProvider;
            _passwordHasher = passwordHasher;
            _saltGenerator = saltGenerator;
        }

        public User GetByUsernameAndPassword(string username, string password)
        {
            DbUser user = null;
            using (var connection = _connectionProvider.GetOpenConnection())
            {
                user = connection.Query<DbUser>(
                    @"
                    SELECT * 
                    FROM Users
                    WHERE Username = @username", new { username = username })
                    .FirstOrDefault();
            }

            if (user == null)
            {
                return null;
            }

            var hashedPassword = _passwordHasher.HashPassword(password + user.PasswordSalt);
            if (hashedPassword == user.Password)
            {
                return Mapper.Map<User>(user);
            }

            return null;
        }

        public User GetByUsername(string username)
        {
            DbUser user = null;
            using (var connection = _connectionProvider.GetOpenConnection())
            {
                user = connection.Query<DbUser>(
                    "SELECT * " +
                    "FROM Users " +
                    "WHERE Username = @username", new
                    {
                        username = username,
                    }).FirstOrDefault();

                return Mapper.Map<User>(user);
            }
        }

        public User CreateUser(string username, string password, string email)
        {
            var salt = _saltGenerator.GenerateSalt(64);
            var userId = 0;
            DbUser user = null;
            using (var connection = _connectionProvider.GetOpenConnection())
            {
                using (var tran = connection.BeginTransaction())
                {
                    try
                    {
                        userId = connection.Query<int>(
                            "INSERT INTO Users(Username, Email, Password, PasswordSalt) VALUES(" +
                            "@username, @email, @password, @passwordSalt) " + 
                            "SELECT @@SCOPE_IDENTITY()", new
                            {
                                username = username,
                                email = email,
                                passwordSalt = salt,
                                password = _passwordHasher.HashPassword(salt + password)
                            }, tran).First();

                        var dbUser = connection.Query<DbUser>(
                            "SELECT * FROM Users WHERE Id = @id", new { id = userId }, tran);

                        tran.Commit();
                    }
                    catch (Exception)
                    {
                        tran.Rollback();
                        throw;
                    }
                }
            }

            return Mapper.Map<User>(user);
        }

        public bool IsEmailExists(string email)
        {
            using (var connection = _connectionProvider.GetOpenConnection())
            {
                var mailCount = connection.Query<int>("SELECT COUNT(*) FROM Users WHERE Email = @email",
                    new { email = email }).First();

                return mailCount > 0;
            }
        }

        public IEnumerable<User> GetAll()
        {
            using (var connection = _connectionProvider.GetOpenConnection())
            {
                var dbUsers = connection.Query<DbUser>("SELECT * FROM Users");
                return Mapper.Map<IEnumerable<User>>(dbUsers);
            }
        }

        public User GetById(object id)
        {
            using (var connection = _connectionProvider.GetOpenConnection())
            {
                var dbUser = connection.Query<DbUser>(
                    "SELECT * FROM Users " +
                    "WHERE Id = @id", new { id = (int)id }).First();
                return Mapper.Map<User>(dbUser);
            }
        }
    }
}
