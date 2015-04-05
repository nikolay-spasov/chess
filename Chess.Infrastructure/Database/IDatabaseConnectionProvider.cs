namespace Chess.Infrastructure.Database
{
    using System.Data.Common;

    public interface IDatabaseConnectionProvider
    {
        DbConnection GetOpenConnection();
    }
}
