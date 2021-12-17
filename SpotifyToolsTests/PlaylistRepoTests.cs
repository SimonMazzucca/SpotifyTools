using NUnit.Framework;
using SpotifyToolsLib.Repositories;
using SpotifyToolsLib.Spotify;
using System.IO;

namespace SpotifyToolsTests
{
    [TestFixture]
    public class PlaylistRepoTests : BaseTester
    {
        //ITunes_Playlist_1995.txt

        /// <summary>
        /// Basic Playlist
        /// Separator: comma
        /// Header: Name, Artist
        /// </summary>
        [Test]
        public void ParseBasicPlaylistCreatedManually()
        {
            string playlistFile = GetFullPath("Basic_Playlist.txt");

            CsvPlaylistRepo repo = new CsvPlaylistRepo();
            Playlist songList = repo.GetSongList(playlistFile);

            Assert.AreEqual(3, songList.Songs.Count);

        }

        /// <summary>
        /// Basic Playlist
        /// Separator: tab
        /// Header: many
        /// </summary>
        [Test]
        public void ParseITunesPlaylist()
        {
            string playlistFile = GetFullPath("ITunes_Playlist.txt");

            ITunesExportPlaylistRepo repo = new ITunesExportPlaylistRepo();
            Playlist songList = repo.GetSongList(playlistFile);

            Assert.AreEqual(9, songList.Songs.Count);

        }

    }
}
