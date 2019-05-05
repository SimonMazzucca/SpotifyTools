﻿using iTunesLib;
using SpotifyToolsLib.Spotify;
using System.Collections.Generic;
using System.Diagnostics;


using iTunesLib;
using System.Collections.Generic;

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

        public Spotify.Playlist GetPlaylist(string name)
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
