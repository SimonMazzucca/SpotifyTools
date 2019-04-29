using iTunesLib;
using NUnit.Framework;
using SpotifyToolsLib.iTunes;
using System.Collections.Generic;
using System.Diagnostics;

namespace SpotifyToolsTests
{
    //[TestClass]
    [TestFixture]
    public class iTunesAdapterTests
    {
        [Test]
        public void TestMethod1()
        {
            iTunesAdapter ita = new iTunesAdapter();
            IEnumerable<string> playlists = ita.GetPlaylists2();

            Assert.AreEqual(1, 1);
        }
        [Test]
        public void TestMethod2()
        {
            iTunesApp app = new iTunesApp();
            IITSource library = app.Sources.ItemByName["Library"];

            foreach (IITPlaylist item in library.Playlists)
            {
                Debug.WriteLine(item.Name);
                //yield return item.Name;
            }

            Assert.AreEqual(1, 1);
        }
    }
}
