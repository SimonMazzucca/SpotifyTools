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
        private const string PLAYLIST_TO_CREATE = "FromUnitTests";

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
            Playlist songsToImport = repo.GetSongList(playlistFile);

            SpotifyAdapter spotify = new SpotifyAdapter(new SettingsFacade());

            if (spotify.PlaylistExists(PLAYLIST_TO_CREATE))
            {
                Assert.Inconclusive();
            }
            else
            {
                Task<SpotifyPlaylist> playlistCreated = spotify.CreatePlaylist(PLAYLIST_TO_CREATE, false);
                SpotifyPlaylist p = playlistCreated.Result;

                Assert.AreEqual(PLAYLIST_TO_CREATE, playlistCreated.Result.name);
            }
        }

        [Test]
        public void TestSpotifyAdapter_CheckIfPlaylistExists()
        {
            SpotifyAdapter spotify = new SpotifyAdapter(new SettingsFacade());
            bool exists = spotify.PlaylistExists("Podcasts");

            Assert.IsTrue(exists);
        }

    }
}
