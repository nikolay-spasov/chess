namespace Chess.Core.Repository
{
    using Chess.Core.Models;

    public interface IUserRepository : IRepository<User>
    {
        User GetByUsernameAndPassword(string username, string password);

        User GetByUsername(string username);

        User CreateUser(string username, string password, string email);

        bool IsEmailExists(string email);
    }
}
