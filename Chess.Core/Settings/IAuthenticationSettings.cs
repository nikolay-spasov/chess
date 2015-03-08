namespace Chess.Core.Settings
{
    using System;

    public interface IAuthenticationSettings
    {
        TimeSpan TokenExpirationLength { get; }
    }
}
