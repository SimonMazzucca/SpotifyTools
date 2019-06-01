using NUnit.Framework;
using SpotifyToolsLib.Utilities;

namespace SpotifyToolsTests
{
    [TestFixture]
    public class SettingsTests : BaseTester
    {

        [Test]
        public void SettingsTest_ReadSettings()
        {
            ISettingsFileAccess fileAccess = new SettingsFileAccess();  //Do I need mock version ?
            SettingsFacade facade = new SettingsFacade(fileAccess);
            Settings settings = facade.GetSettings();

            Assert.AreEqual("https://api.spotify.com/v1", settings.Spotify.ApiBaseUrl);
            Assert.AreEqual("https://accounts.spotify.com/api/token", settings.Spotify.AuthKeyUrl);
            Assert.AreEqual("simonmazzucca@hotmail.com", settings.Spotify.UserId);
            Assert.AreEqual("773d6796930f47ccbbc2a0b7a248f666", settings.Spotify.ClientId);
            Assert.AreEqual("https://www.shazam.com", settings.Shazam.BaseUrl);

            Assert.IsNotEmpty(settings.Spotify.AuthToken.AccessToken);
            Assert.IsNotEmpty(settings.Spotify.AuthToken.RefreshToken);
            Assert.IsNotEmpty(settings.Spotify.AuthToken.TokenType);

        }

    }
}


