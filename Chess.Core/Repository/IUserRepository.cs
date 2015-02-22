namespace Chess.Core.Repository
{
    using Chess.Core.Models;

    public interface IUserRepository : IRepository<User>
    {
        User GetByUsernameAndPassword(string username, string password);
    }
}
