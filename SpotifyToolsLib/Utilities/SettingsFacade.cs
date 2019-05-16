using Newtonsoft.Json;

namespace SpotifyToolsLib.Utilities
{
    public class SettingsFacade : ISettingsFacade
    {
        ISettingsFileAccess _SettingsFileAccess;

        public SettingsFacade(ISettingsFileAccess settingsFileAccess)
        {
            _SettingsFileAccess = settingsFileAccess;
        }

        public SettingsFacade()
        {
            _SettingsFileAccess = new SettingsFileAccess();
        }

        public Settings GetSettings()
        {
            string settingsJson = _SettingsFileAccess.Load();
            Settings toReturn = JsonConvert.DeserializeObject<Settings>(settingsJson);

            //TODO: error handling
            return toReturn;
        }
    }
}
