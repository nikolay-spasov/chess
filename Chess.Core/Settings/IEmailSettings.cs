namespace Chess.Core.Settings
{
    public interface IEmailSettings
    {
        string Host { get; }

        string Username { get; }

        string Password { get; }

        int Port { get; }
    }
}
