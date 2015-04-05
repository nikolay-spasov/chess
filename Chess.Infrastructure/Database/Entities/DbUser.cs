﻿namespace Chess.Infrastructure.Database.Entities
{
    public class DbUser
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get;  set; }
        public string PasswordSalt { get; set; }
        public string Email { get;  set; }
    }
}
