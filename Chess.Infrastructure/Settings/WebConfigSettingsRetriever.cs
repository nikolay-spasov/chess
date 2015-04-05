using System.Web.Configuration;

using Chess.Core.Settings;

namespace Chess.Infrastructure.Settings
{
    public class WebConfigSettingsRetriever : ISettingsRetriever
    {
        public string GetSetting(string settingName)
        {
            return WebConfigurationManager.AppSettings.Get(settingName);
        }

        public string GetConnectionString(string connectionStringName)
        {
            return WebConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
        }
    }
}
