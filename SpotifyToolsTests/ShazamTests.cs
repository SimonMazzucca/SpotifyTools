using NUnit.Framework;
using SpotifyToolsLib.Shazam;
using SpotifyToolsLib.Spotify;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace SpotifyToolsTests
{
    [TestFixture]
    public class ShazamTests
    {

        [Test]
        public void TestShazamAdapter_GetMyShazamSongs()
        {
            string json = GetEmbeddedResourceFile("MyShazamSongs.json");
            ShazamAdapter shazam = new ShazamAdapter();
            IList<Song> songs = shazam.GetMyShazamSongs(json);

            Assert.IsTrue(songs.Count > 0);
        }

        private string GetEmbeddedResourceFile(string file)
        {
            string result;

            Assembly assembly = Assembly.GetExecutingAssembly();
            string resourceName = "SpotifyToolsTests.Resources." + file;

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                result = reader.ReadToEnd();
            }

            return result;
        }
    }
}
