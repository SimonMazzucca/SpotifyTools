using System.Collections.Generic;

namespace SpotifyToolsLib.Repositories
{
    public class Song
    {
        public string Name { get; set; }
        public string Artist { get; set; }
    }

    //TODO: use other playlist ?
    public class SongList : List<Song>
    {
    }
}
