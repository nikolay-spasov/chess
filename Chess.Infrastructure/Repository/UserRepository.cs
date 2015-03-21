namespace Chess.Infrastructure.Repository
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using AutoMapper;

    using Chess.Core.Models;
    using Chess.Core.Repository;
    using DB = Chess.Infrastructure.Database;
    using Chess.Infrastructure.Authentication;

    public class UserRepository : IUserRepository
    {
        private readonly DB.ChessDbEntities _db;
        private readonly PasswordHasher _passwordHasher;
        private readonly SaltGenerator _saltGenerator;

        public UserRepository(DB.ChessDbEntities db, PasswordHasher passwordHasher, SaltGenerator saltGenerator)
        {
            if (db == null)
            {
                throw new ArgumentNullException("db");
            }

            if (passwordHasher == null)
            {
                throw new ArgumentNullException("passwordHasher");
            }

            if (saltGenerator == null)
            {
                throw new ArgumentNullException("saltGenerator");
            }

            _db = db;
            _passwordHasher = passwordHasher;
            _saltGenerator = saltGenerator;
        }

        public User GetByUsernameAndPassword(string username, string password)
        {
            var dbUser = _db.Users.FirstOrDefault(u => u.Username == username);
            if (dbUser == null)
            {
                return null;
            }

            var hashedPassword = _passwordHasher.HashPassword(password + dbUser.PasswordSalt);
            if (hashedPassword != dbUser.Password)
            {
                return null;
            }

            return Mapper.Map<User>(dbUser);
        }

        public IEnumerable<User> GetAll()
        {
            var dbUsers = _db.Users.AsEnumerable();

            return Mapper.Map<IEnumerable<User>>(dbUsers);
        }

        public User GetById(object id)
        {
            var dbUser = _db.Users.FirstOrDefault(x => x.Id == (int) id);

            return Mapper.Map<User>(dbUser);
        }

        public User GetByUsername(string username)
        {
            var dbUser = _db.Users.FirstOrDefault(x => x.Username == username);

            return Mapper.Map<User>(dbUser);
        }

        public User CreateUser(string username, string password, string email)
        {
            var salt = _saltGenerator.GenerateSalt();

            var user = new DB.User
            {
                Username = username,
                Password = _passwordHasher.HashPassword(password + salt),
                Email = email,
                PasswordSalt = salt,
            };

            _db.Users.Add(user);
            _db.SaveChanges();

            return Mapper.Map<User>(user);
        }

        public bool IsEmailExists(string email)
        {
            return _db.Users.Any(u => u.Email == email);
        }
    }
}
