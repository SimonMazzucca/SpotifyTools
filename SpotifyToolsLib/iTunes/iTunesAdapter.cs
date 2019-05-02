using iTunesLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SpotifyToolsLib.iTunes
{

    /// <summary>
    /// More shit: https://csharp.hotexamples.com/examples/-/iTunesApp/-/php-itunesapp-class-examples.html
    /// </summary>
    public class iTunesAdapter
    {
        private readonly iTunesApp _iTunesSucks;

        public iTunesAdapter()
        {
            _iTunesSucks = new iTunesApp();
        }

        public IList<Playlist> GetPlaylists()
        {
            //Why does returning IEnumerable kill the Unit Test?
            IList<Playlist> toReturn = new List<Playlist>();
            IITSource library = _iTunesSucks.Sources.ItemByName["Library"];

            foreach (IITPlaylist item in library.Playlists)
            {
                Playlist playlist = new Playlist()
                {
                    SourceId = item.sourceID,
                    PlaylistId = item.playlistID,
                    Name = item.Name,
                    Count = item.Tracks.Count,
                };
                toReturn.Add(playlist);
            }

            return toReturn;
        }

        //Change to load method?
        public Playlist GetPlaylistDetails(Playlist playlist)
        {
            IITObject playlistObject = _iTunesSucks.GetITObjectByID(playlist.SourceId, playlist.PlaylistId, 0, 0);
            IITPlaylist iTunesPlaylist = (IITPlaylist)playlistObject;

            foreach (IITTrack iTunesTrack in iTunesPlaylist.Tracks)
            {
                Track track = new Track()
                {
                    Name = iTunesTrack.Name,
                    Artist = iTunesTrack.Artist,
                };

                playlist.Tracks.Add(track);
            }
            return playlist;
        }
    }
}
