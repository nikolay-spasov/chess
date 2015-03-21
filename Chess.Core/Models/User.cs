namespace Chess.Core.Models
{
    using System;

    using Chess.Core.Authentication;

    public class User
    {
        public User(int id, string username, string password, string email)
        {
            this.Id = id;
            this.Username = username;
            this.Password = password;
            this.Email = email;
        }

        public void UpdateEmail(string email)
        {
            this.Username = email;
        }

        public int Id { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public string Email { get; private set; }
    }
}
