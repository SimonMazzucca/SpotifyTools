using NUnit.Framework;
using System.Linq;
using SpotifyToolsLib.iTunes;
using System.Collections.Generic;
using SpotifyToolsLib.Spotify;

namespace SpotifyToolsTests
{
    [TestFixture]
    public class ITunesAdapterTests
    {
        private const string TEST_PLAYLIST = "TestPlaylist";

        [Test]
        public void TestiTunesAdapter_GetPlaylists()
        {
            iTunesAdapter ita = new iTunesAdapter();
            IList<Playlist> playlists = ita.GetPlaylists();

            Playlist testPlaylist = playlists.FirstOrDefault(p => p.Name == TEST_PLAYLIST);
            Assert.IsTrue(true);
        }

        [Test]
        public void TestiTunesAdapter_GetPlaylistDetails()
        {
            iTunesAdapter ita = new iTunesAdapter();

            IList<Playlist> playlists = ita.GetPlaylists();
            Playlist testPlaylist = playlists.FirstOrDefault(p => p.Name == TEST_PLAYLIST);
            ita.LoadPlaylist(testPlaylist);

            Assert.IsNotNull(testPlaylist.Name);
            Assert.IsTrue(testPlaylist.Songs.Count > 0);
        }

        [Test]
        public void TestiTunesAdapter_GetPlaylistByName()
        {
            iTunesAdapter ita = new iTunesAdapter();
            Playlist playlist = ita.GetPlaylistByName(TEST_PLAYLIST);

            Assert.IsTrue(playlist != null);
        }


    }
}
