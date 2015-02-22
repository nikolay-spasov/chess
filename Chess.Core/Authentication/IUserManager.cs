namespace Chess.Core.Authentication
{
    using Chess.Core.Models;

    public interface IUserManager
    {
        User Validate(string username, string password);
    }
}
