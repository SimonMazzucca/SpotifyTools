using System;
using System.Collections.Generic;

namespace SpotifyToolsLib.Spotify
{

    //TODO: either consolidate playlists or name accordingly
    public class Playlist
    {
        public Playlist(string name)
        {
            Name = name;
            Songs = new List<Song>();
        }

        public int iTunesPlaylistId { get; set; }
        public int iTunesSourceId { get; set; }
        public int iTunesCount { get; set; }

        public int Count {
            get
            {
                return iTunesCount;
            }
        }

        public string Name { get; set; }
        public IList<Song> Songs { get; set; }

        internal void AddSong(string name, string artist)
        {
            Song song = new Song(name, artist);
            Songs.Add(song);
        }
    }
}
