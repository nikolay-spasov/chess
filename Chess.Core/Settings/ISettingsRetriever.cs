namespace Chess.Core.Settings
{
    public interface ISettingsRetriever
    {
        string GetSetting(string settingName);
    }
}
