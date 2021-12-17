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
        public async Task TestSpotifyAdapter_CreatePlaylistFromCsvFileAsync()
        {
            string playlistFile = GetFullPath("Basic_Playlist.txt");
            CsvPlaylistRepo repo = new CsvPlaylistRepo();
            Playlist songsToImport = repo.GetSongList(playlistFile);

            SpotifyAdapter spotify = new SpotifyAdapter(new SettingsFacade());
            SpotifyPlaylist playlist = spotify.GetPlaylist(PLAYLIST_TO_CREATE);

            if (playlist == null)
            {
                playlist = spotify.CreatePlaylist(PLAYLIST_TO_CREATE, false).Result;
                Assert.AreEqual(PLAYLIST_TO_CREATE, playlist.name);
            }

            await spotify.AddSongsToPlaylist(playlist, songsToImport.Songs);

            SpotifyPlaylist playlistNew = spotify.GetPlaylist(PLAYLIST_TO_CREATE);

            Assert.IsNotNull(playlistNew);
            Assert.IsNotNull(playlistNew.tracks);
            Assert.AreEqual(songsToImport.Songs.Count, playlistNew.tracks.total);
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
