namespace SpotifyToolsLib.Spotify.SpotifyModel
{
    public class SpotifyPlaylist
    {
        public bool collaborative { get; set; }
        public string description { get; set; }
        public string href { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public SpotifyUser owner { get; set; }
        public bool? @public { get; set; }
        public SpotifyPage<SpotifyPlaylistTrack> tracks { get; set; }
        public string type { get; set; }
        public string uri { get; set; }

    }
}
