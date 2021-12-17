namespace SpotifyToolsLib.Spotify.SpotifyModel
{
    public class SpotifyPlaylistCollection
    {
        public string href { get; set; }
        public SpotifyPlaylist[] items { get; set; }
        public int limit { get; set; }
        public string next { get; set; }
        public int offset { get; set; }
        public object previous { get; set; }
        public int total { get; set; }


    }

}
