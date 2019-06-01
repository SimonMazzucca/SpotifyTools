using NUnit.Framework;
using SpotifyToolsLib.Spotify;
using SpotifyToolsLib.Utilities;
using System.Threading.Tasks;

namespace SpotifyToolsTests
{
    [TestFixture]
    public class SpotifyTests
    {

        [Test]
        public void TestSpotifyAdapter_BasicConnectivity()
        {
            SpotifyAdapter spotify = new SpotifyAdapter(new SettingsFacade());
            Task<User> user = spotify.GetCurrentUser();

            Assert.AreEqual("Simon Mazzucca", user.Result.DisplayName);
        }

    }
}
