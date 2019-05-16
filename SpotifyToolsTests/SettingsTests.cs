using NUnit.Framework;
using SpotifyToolsLib.Utilities;
using SpotifyToolsTests.Mocks;

namespace SpotifyToolsTests
{
    [TestFixture]
    public class SettingsTests : BaseTester
    {

        [Test]
        public void SettingsTest_ReadSettings()
        {
            string settingsJson = GetResourceFileContent("Settings.json");
            ISettingsFileAccess mockFileAccess = new MockFileAccess();

            SettingsFacade facade = new SettingsFacade(mockFileAccess);
            Settings settings = facade.GetSettings();

            Assert.AreEqual("https://api.spotify.com/v1", settings.Spotify.ApiBaseUrl);
            Assert.AreEqual("https://accounts.spotify.com/api/token", settings.Spotify.AuthKeyUrl);
            Assert.AreEqual("XYZ", settings.Spotify.AuthToken);
            Assert.AreEqual("simonmazzucca@hotmail.com", settings.Spotify.UserId);
            Assert.AreEqual("773d6796930f47ccbbc2a0b7a248f666", settings.Spotify.ClientId);
            Assert.AreEqual("https://www.shazam.com", settings.Shazam.BaseUrl);
        }

    }
}


