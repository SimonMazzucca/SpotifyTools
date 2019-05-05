using iTunesLib;
using SpotifyToolsLib.Spotify;
using System.Collections.Generic;
using System.Diagnostics;

namespace SpotifyToolsLib.iTunes
{

    /// <summary>
    /// More shit: https://csharp.hotexamples.com/examples/-/iTunesApp/-/php-itunesapp-class-examples.html
    /// </summary>
    public class iTunesAdapter
    {
        public Playlist GetPlaylist(string name)
        {
            iTunesApp app = new iTunesApp();
            IITSource library = app.Sources.ItemByName["Library"];
            Playlist toReturn = null;

            foreach (IITPlaylist item in library.Playlists)
            {
                if (item.Name == name)
                {
                    toReturn = new Playlist(name);
                    foreach (IITTrack song in item.Tracks)
                    {
                        toReturn.AddSong(song.Name, song.Artist);
                    }
                }
            }

            return toReturn;
        }

        /// <summary>
        /// Gets all playlists that are present in iTunes library.
        /// </summary>
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
