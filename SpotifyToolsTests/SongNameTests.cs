using NUnit.Framework;
using SpotifyToolsLib.Spotify;

namespace SpotifyToolsTests
{
    [TestFixture]
    public class SongNameTests : BaseTester
    {

        [Test]
        public void TestSongName_WithParens()
        {
            Song song = new Song("Via la Cippa (with Antonio)", "Simon");
            Assert.AreEqual("Via la Cippa", song.JustName);
        }

        [Test]
        public void TestSongName_WithBrackets()
        {
            Song song = new Song("Via la Cippa [The Loozers]", "Simon");
            Assert.AreEqual("Via la Cippa", song.JustName);
        }

        [Test]
        public void TestSongName_WithBothParensFirst()
        {
            Song song = new Song("Via la Cippa (with Antonio) [The Loozers]", "Simon");
            Assert.AreEqual("Via la Cippa", song.JustName);
        }

        [Test]
        public void TestSongName_WithBothBracketsFirst()
        {
            Song song = new Song("Via la Cippa [The Loozers] (with Antonio)", "Simon");
            Assert.AreEqual("Via la Cippa", song.JustName);
        }

    }
}
