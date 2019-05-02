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

            var rating = playlists.FirstOrDefault(p => p.Name == "00 - All");
            Assert.IsTrue(true);
        }

        [Test]
        public void TestiTunesAdapter_GetPlaylistDetails()
        {
            iTunesAdapter ita = new iTunesAdapter();
            Playlist playlist = new Playlist()
            {
                SourceId = 75,
                PlaylistId = 216150,
            };
            playlist = ita.GetPlaylistDetails(playlist);

            Assert.IsTrue(true);
        }

    }
}
