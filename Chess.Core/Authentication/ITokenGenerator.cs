namespace Chess.Core.Authentication
{
    using Chess.Core.Models;

    public interface ITokenGenerator
    {
        string GetAuthenticationToken(User user);
    }
}
