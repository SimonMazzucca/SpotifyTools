using NUnit.Framework;
using SpotifyToolsLib.Repositories;
using System.IO;

namespace SpotifyToolsTests
{
    [TestFixture]
    public class PlaylistRepoTests : BaseTester
    {
        //ITunes_Playlist_1995.txt

        /// <summary>
        /// Basi Playlist
        /// Separator: comma
        /// Header: Name, Artist
        /// </summary>
        [Test]
        public void ParseBasicPlaylistCreatedManually()
        {
            string playlistFile = GetFullPath("Basic_Playlist.txt");

            CsvPlaylistRepo repo = new CsvPlaylistRepo();
            SongList songList = repo.GetSongList(playlistFile);

            Assert.AreEqual(3, songList.Count);

        }

        /// <summary>
        /// Basi Playlist
        /// Separator: tab
        /// Header: many
        /// </summary>
        [Test]
        public void ParseITunesPlaylist()
        {
            string playlistFile = GetFullPath("ITunes_Playlist.txt");

            ITunesExportPlaylistRepo repo = new ITunesExportPlaylistRepo();
            SongList songList = repo.GetSongList(playlistFile);

            Assert.AreEqual(9, songList.Count);

        }

    }
}
