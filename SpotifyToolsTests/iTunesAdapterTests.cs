using NUnit.Framework;
using SpotifyToolsLib.iTunes;
using SpotifyToolsLib.Spotify;
using System.Collections.Generic;

namespace SpotifyToolsTests
{
    [TestFixture]
    public class iTunesAdapterTests
    {
        [Test]
        public void GetAllPlaylists()
        {
            iTunesAdapter ita = new iTunesAdapter();
            IList<string> playlists = ita.GetPlaylists();

            Assert.IsTrue(playlists.Count > 0);
        }

        [Test]
        public void GetPlaylistByName()
        {
            iTunesAdapter ita = new iTunesAdapter();
            Playlist playlist = ita.GetPlaylist("Library");

            Assert.IsTrue(playlist != null);
        }

    }
}
