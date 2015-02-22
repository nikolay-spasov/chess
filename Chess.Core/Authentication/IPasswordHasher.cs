namespace Chess.Core.Authentication
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);
    }
}
