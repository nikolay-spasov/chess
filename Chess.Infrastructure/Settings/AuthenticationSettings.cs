using System;

using Chess.Core.Settings;

namespace Chess.Infrastructure.Settings
{
    public class AuthenticationSettings : IAuthenticationSettings
    {
        private readonly ISettingsRetriever _settingsRetriever;

        public AuthenticationSettings(ISettingsRetriever settingsRetriever)
        {
            if (settingsRetriever == null)
            {
                throw new ArgumentNullException("settingsRetriever");
            }

            _settingsRetriever = settingsRetriever;
        }

        public TimeSpan TokenExpirationLength
        {
            get
            {
                return TimeSpan.FromSeconds(Convert.ToDouble(_settingsRetriever.GetSetting("auth_TokenExpirationLengthSeconds")));
            }
        }
    }
}
