using iTunesLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyToolsLib.iTunes
{
    public class iTunesAdapter
    {
        /// <summary>
        /// Gets all playlists that are present in iTunes library.
        /// </summary>
        /// <remarks>
        /// The playlist will also be indexed in playlistLookupTable with IPlaylist as key and IITPlaylist as value for easy retrieval of iTunes COM objects.
        /// </remarks>
        /// <returns>An enumeration of all playlists in iTunes. </returns>
        public IEnumerable<string> GetPlaylists2()
        {
            iTunesApp app = new iTunesApp();
            IITSource library = app.Sources.ItemByName["Library"];

            foreach (IITPlaylist item in library.Playlists)
            {
                Console.WriteLine(item);
                yield return item.Name;
            }
        }

        public void Test2(string playlistName)
        {
            var files = new iTunesApp()
                .LibrarySource
                .Playlists
                .ItemByName[playlistName]
                .Tracks
                .Cast<IITFileOrCDTrack>();

            var tracksByArtist = files
                .GroupBy(file => file.Artist)
                .OrderBy(group => group.Key);

            foreach (var artistGroup in tracksByArtist)
            {
                var albumGroups = artistGroup
                    .GroupBy(track => track.Album);

                // The artist name has to be normalized, so that it doesn't contain any characters that are invalid for a path
                string artistName = artistGroup.Key;

                //var albumDirectories = albumGroups
                //    .Select(album => new { Album = album, AlbumName = album.Key })
                //    .Select
                //    (
                //        directory =>
                //        new ITunesDirectoryInfo
                //        (
                //            directory.AlbumName,
                //            directory.Album
                //                .Where(track => track.Location != null)
                //                .Select
                //                (
                //                    track => (IFileInfo)new LocalFileInfo(new FileInfo(track.Location))
                //                ),
                //            null
                //        )
                //    )
                //    .ToList(); //Execute the query immediately, because a streaming causes weird bugs

                //var artistDirectory = new ITunesDirectoryInfo(artistName, albumDirectories, root);

                //foreach (ITunesDirectoryInfo albumDirectory in artistDirectory.GetDirectories())
                //{
                //    albumDirectory.Parent = artistDirectory;
                //}

                //artistDirectories.Add(artistDirectory);
            }
        }
    }
}
