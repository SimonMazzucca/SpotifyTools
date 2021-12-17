using NUnit.Framework;
using SpotifyToolsLib.Shazam;
using SpotifyToolsLib.Spotify;
using System.Collections.Generic;

namespace SpotifyToolsTests
{
    [TestFixture]
    public class ShazamTests : BaseTester
    {

        [Test]
        [Ignore("Not needed anymore")]

        public void TestShazamAdapter_GetMyShazamSongs()
        {
            //string json = GetResourceFileContent("MyShazamSongs.json");
            ShazamAdapter shazam = new ShazamAdapter();
            IList<Song> songs = shazam.GetMyShazamSongs();

            Assert.IsTrue(songs.Count > 0);
        }

    }
}
