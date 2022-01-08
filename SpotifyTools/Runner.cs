using log4net;
using SpotifyToolsLib.iTunes;
using SpotifyToolsLib.Spotify;
using SpotifyToolsLib.Spotify.SpotifyModel;
using SpotifyToolsLib.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SpotifyTools
{
    public class Runner
    {
        private readonly ILog _Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        SpotifyAdapter _spotify = new SpotifyAdapter(new SettingsFacade());
        iTunesAdapter _iTunes = new iTunesAdapter();

        public void CreatePlaylist(string playlistName)
        {
            // Abort if playlist already exists
            if (_spotify.PlaylistExists(playlistName) != null)
            {
                _Log.InfoFormat("Playlist already exists,{0}", playlistName);
                return;
            }

            // Get playlist from iTunes
            IList<Playlist> playlists = _iTunes.GetPlaylists();
            Playlist songsToImport = playlists.FirstOrDefault(p => p.Name == playlistName);
            _iTunes.LoadPlaylist(songsToImport);

            // Create Spotify playlist and populate it
            SpotifyPlaylist spotifyPlaylist = _spotify.CreatePlaylist(playlistName, false).Result;
            _spotify.AddSongsToPlaylist(spotifyPlaylist, songsToImport.Songs);
        }

        public void SortPlaylist(string playlistName)
        {
            SpotifyPlaylist spotifyPlaylist = _spotify.PlaylistExists(playlistName);

            if (spotifyPlaylist == null)
            {
                // Abort if playlist already exists
                _Log.InfoFormat("Playlist does not exist,{0}", playlistName);
            }
            else
            {
                // Sort
                _spotify.SortPlaylist(spotifyPlaylist);
            }
        }



    }
}
