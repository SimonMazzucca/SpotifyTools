using iTunesLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpotifyToolsLib.iTunes;
using System.Collections.Generic;
using System.Diagnostics;

namespace SpotifyToolsTests
{
    [TestClass]
    //[TestFixture]
    public class iTunesAdapterTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            iTunesAdapter ita = new iTunesAdapter();
            IList<string> playlists = ita.GetPlaylists2();

            Assert.IsTrue(true);
        }

    }
}
