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
        public void TestSpotifyAdapter_CreatePlaylistFromCsvFileAsync()
        {
            string playlistFile = GetFullPath("Basic_Playlist.txt");
            CsvPlaylistRepo repo = new CsvPlaylistRepo();
            SongList songList = repo.GetSongList(playlistFile);

            SpotifyAdapter spotify = new SpotifyAdapter(new SettingsFacade());
            Task<playlist> result = spotify.CreatePlaylist("Test", false);
            playlist p = result.Result;

            Assert.AreEqual("Test", p.name);
        }

    }
}
