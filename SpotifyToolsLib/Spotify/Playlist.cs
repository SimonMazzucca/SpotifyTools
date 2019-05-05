using System;
using System.Collections.Generic;

namespace SpotifyToolsLib.Spotify
{
    public class Playlist
    {
        public Playlist(string name)
        {
            Name = name;
            Songs = new List<Song>();
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
