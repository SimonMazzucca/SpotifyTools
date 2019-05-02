using System.Collections.Generic;

namespace SpotifyToolsLib.iTunes
{
    public class Playlist
    {
        public string Name { get; set; }
        public int Count { get; set; }
        public int PlaylistId { get; set; }
        public int SourceId { get; set; }

        public IList<Track> Tracks { get; set; }

        public Playlist()
        {
            Tracks = new List<Track>();
        }
    }
}
