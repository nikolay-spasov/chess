namespace Chess.Infrastructure.Database
{
    using System;
    using System.Data.Common;

    using Chess.Core.Settings;

    public class DatabaseConnectionProvider : IDatabaseConnectionProvider
    {
        DbConnection conn;
        string connectionString;
        DbProviderFactory factory;

        // Constructor that retrieves the connectionString from the config file
        public DatabaseConnectionProvider(ISettingsRetriever settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException("settings");
            }

            factory = DbProviderFactories
                .GetFactory("System.Data.SqlClient");
            this.connectionString = settings.GetConnectionString("ChessDb");
        }

        // Constructor that accepts the connectionString and Database ProviderName i.e SQL or Oracle
        public DatabaseConnectionProvider(string connectionString, string connectionProviderName)
        {
            this.connectionString = connectionString;
            factory = DbProviderFactories.GetFactory(connectionProviderName);
        }

        // Only inherited classes can call this.
        public DbConnection GetOpenConnection()
        {
            conn = factory.CreateConnection();
            conn.ConnectionString = this.connectionString;
            conn.Open();

            return conn;
        }
    }
}
