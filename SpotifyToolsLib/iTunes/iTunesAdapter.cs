using iTunesLib;
using SpotifyToolsLib.Spotify;
using SpotifyToolsLib.Utilities;
using System.Collections.Generic;

namespace SpotifyToolsLib.iTunes
{

    /// <summary>
    /// More shit: https://csharp.hotexamples.com/examples/-/iTunesApp/-/php-itunesapp-class-examples.html
    /// </summary>
    public class iTunesAdapter : BaseAdapter
    {
        private readonly iTunesApp _iTunesSucks;

        public iTunesAdapter() : base()
        {
            _iTunesSucks = new iTunesApp();
        }

        public iTunesAdapter(ISettingsFacade settingsFacade) : base(settingsFacade)
        {
            _iTunesSucks = new iTunesApp();
        }

        /// <summary>
        /// Names are not unique, so not use I can use this
        /// </summary>
        public Spotify.Playlist GetPlaylistByName(string name)
        {
            iTunesApp app = new iTunesApp();
            IITSource library = app.Sources.ItemByName["Library"];
            Spotify.Playlist toReturn = null;

            foreach (IITPlaylist item in library.Playlists)
            {
                if (item.Name == name)
                {
                    toReturn = new Spotify.Playlist(name);
                    foreach (IITTrack song in item.Tracks)
                    {
                        toReturn.AddSong(song.Name, song.Artist);
                    }
                }
            }

            return toReturn;
        }

        /// <summary>
        /// Resturns a light version of all playlists
        /// </summary>
        /// <returns></returns>
        public IList<Playlist> GetPlaylists()
        {
            //Why does returning IEnumerable kill the Unit Test?
            IList<Playlist> toReturn = new List<Playlist>();
            IITSource library = _iTunesSucks.Sources.ItemByName["Library"];

            foreach (IITPlaylist item in library.Playlists)
            {
                Playlist playlist = new Playlist(item.Name)
                {
                    iTunesSourceId = item.sourceID,
                    iTunesPlaylistId = item.playlistID,
                    iTunesCount = item.Tracks.Count,
                };
                toReturn.Add(playlist);
            }

            return toReturn;
        }

        public void LoadPlaylist(Playlist playlist)
        {
            IITObject playlistObject = _iTunesSucks.GetITObjectByID(playlist.iTunesSourceId, playlist.iTunesPlaylistId, 0, 0);
            IITPlaylist iTunesPlaylist = (IITPlaylist)playlistObject;

            if (playlistObject != null)
            {
                playlist.Name = iTunesPlaylist.Name;
                foreach (IITTrack iTunesTrack in iTunesPlaylist.Tracks)
                {
                    Song track = new Song()
                    {
                        Name = iTunesTrack.Name,
                        Artist = iTunesTrack.Artist,
                    };

                    playlist.Songs.Add(track);
                }
            }

        }
    }
}
