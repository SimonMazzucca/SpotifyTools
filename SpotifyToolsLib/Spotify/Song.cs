namespace SpotifyToolsLib.Spotify
{
    public class Song
    {
        public Song(string name, string artist)
        {
            Name = name;
            Artist = artist;
        }

        public string Name { get; set; }
        public string Artist { get; set; }
    }
}
