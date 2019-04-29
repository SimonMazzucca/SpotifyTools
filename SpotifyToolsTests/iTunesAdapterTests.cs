using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpotifyToolsLib.iTunes;
using System.Collections.Generic;

namespace SpotifyToolsTests
{
    [TestClass]
    public class iTunesAdapterTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            iTunesAdapter ita = new iTunesAdapter();
            IList<string> playlists = ita.GetPlaylists();

            Assert.IsTrue(true);
        }

    }
}
