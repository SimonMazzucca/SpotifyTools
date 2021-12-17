using Newtonsoft.Json;

namespace SpotifyToolsLib.Spotify.SpotifyModel
{
    [JsonObject]
    public class SpotifyPage<T>
    {
        public string href { get; set; }

        public T[] items { get; set; }

        public int limit { get; set; }

        public string next { get; set; }

        public int offset { get; set; }

        public string previous { get; set; }

        public int total { get; set; }

    }
}
