namespace Chess.Core.Settings
{
    public interface ISettingsRetreiver
    {
        string GetSetting(string settingName);
    }
}
