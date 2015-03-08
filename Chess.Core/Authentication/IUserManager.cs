namespace Chess.Core.Authentication
{
    using Microsoft.Owin.Security;

    using Chess.Core.Models;

    public interface IUserManager
    {
        User Validate(string username, string password);

        AuthenticationTicket GetAuthenticationTicket(User user, string authenticationType);
    }
}
