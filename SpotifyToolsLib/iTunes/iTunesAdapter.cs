using iTunesLib;
using System.Collections.Generic;
using System.Diagnostics;

namespace SpotifyToolsLib.iTunes
{

    /// <summary>
    /// More shit: https://csharp.hotexamples.com/examples/-/iTunesApp/-/php-itunesapp-class-examples.html
    /// </summary>
    public class iTunesAdapter
    {
        /// <summary>
        /// Gets all playlists that are present in iTunes library.
        /// </summary>
        /// <remarks>
        /// The playlist will also be indexed in playlistLookupTable with IPlaylist as key and IITPlaylist as value for easy retrieval of iTunes COM objects.
        /// </remarks>
        /// <returns>An enumeration of all playlists in iTunes. </returns>
        public IList<string> GetPlaylists()
        {
            //Why does returning IEnumerable kill the Unit Test?
            IList<string> toReturn = new List<string>();
            iTunesApp app = new iTunesApp();
            IITSource library = app.Sources.ItemByName["Library"];

            foreach (IITPlaylist item in library.Playlists)
            {
                Debug.WriteLine(item.Name);
                toReturn.Add(item.Name);
            }

            return toReturn;
        }

    }
}
