using NUnit.Framework;
using SpotifyToolsLib.Repositories;
using SpotifyToolsLib.Spotify;
using SpotifyToolsLib.Spotify.SpotifyModel;
using SpotifyToolsLib.Utilities;
using System.Threading.Tasks;

namespace SpotifyToolsTests
{
    [TestFixture]
    public class SpotifyTests : BaseTester
    {

        [Test]
        public void TestSpotifyAdapter_BasicConnectivity()
        {
            SpotifyAdapter spotify = new SpotifyAdapter(new SettingsFacade());
            Task<User> user = spotify.GetCurrentUser();

            Assert.AreEqual("Simon Mazzucca", user.Result.DisplayName);
        }

        [Test]
        [Ignore("")]
        public void TestSpotifyAdapter_CreatePlaylistFromCsvFileAsync()
        {
            string playlistFile = GetFullPath("Basic_Playlist.txt");
            CsvPlaylistRepo repo = new CsvPlaylistRepo();
            Playlist songsToImport = repo.GetSongList(playlistFile);

            SpotifyAdapter spotify = new SpotifyAdapter(new SettingsFacade());
            Task<SpotifyPlaylist> playlistCreated = spotify.CreatePlaylist("Test", false);
            //SpotifyPlaylist p = playlistCreated.Result;

            Assert.AreEqual("Test", playlistCreated.Result.name);
        }

    }
}
