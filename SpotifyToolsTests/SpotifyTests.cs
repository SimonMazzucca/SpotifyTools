using NUnit.Framework;
using SpotifyToolsLib.Repositories;
using SpotifyToolsLib.Spotify;
using SpotifyToolsLib.Spotify.SpotifyModel;
using SpotifyToolsLib.Utilities;
using System.IO;
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

        //TODO: Share among test classes
        private static string GetFullPath(string filename)
        {
            string binFolder = Path.GetDirectoryName(TestContext.CurrentContext.TestDirectory);
            string playlistFile = binFolder.Replace(@"\bin", @"\Resources\" + filename);
            return playlistFile;
        }

    }
}
