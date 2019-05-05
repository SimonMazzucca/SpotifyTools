using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpotifyToolsLib.iTunes;
using SpotifyToolsLib.Spotify;
using System.Collections.Generic;

namespace SpotifyToolsTests
{
    [TestClass]
    public class iTunesAdapterTests
    {
        [TestMethod]
        public void GetAllPlaylists()
        {
            iTunesAdapter ita = new iTunesAdapter();
            IList<string> playlists = ita.GetPlaylists();

            Assert.IsTrue(playlists.Count > 0);
        }

        [TestMethod]
        public void GetPlaylistByName()
        {
            iTunesAdapter ita = new iTunesAdapter();
            Playlist playlist = ita.GetPlaylist("Library");

            Assert.IsTrue(playlist != null);
        }

    }
}
