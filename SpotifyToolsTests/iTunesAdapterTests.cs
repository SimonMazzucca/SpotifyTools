using NUnit.Framework;
using System.Linq;
using SpotifyToolsLib.iTunes;
using System.Collections.Generic;

namespace SpotifyToolsTests
{
    [TestFixture]
    public class ITunesAdapterTests
    {
        [Test]
        public void TestiTunesAdapter_GetPlaylists()
        {
            iTunesAdapter ita = new iTunesAdapter();
            IList<Playlist> playlists = ita.GetPlaylists();

            Playlist testPlaylist = playlists.FirstOrDefault(p => p.Name == "TestPlaylist");
            Assert.IsTrue(true);
        }

        [Test]
        public void TestiTunesAdapter_GetPlaylistDetails()
        {
            iTunesAdapter ita = new iTunesAdapter();
            Playlist playlist = new Playlist()
            {
                SourceId = 77,
                PlaylistId = 153915,
            };
            playlist = ita.GetPlaylistDetails(playlist);

            Assert.IsTrue(true);
        }

        [Test]
        public void GetPlaylistByName()
        {
            iTunesAdapter ita = new iTunesAdapter();
            SpotifyToolsLib.Spotify.Playlist playlist = ita.GetPlaylist("TestPlaylist");

            Assert.IsTrue(playlist != null);
        }


    }
}
