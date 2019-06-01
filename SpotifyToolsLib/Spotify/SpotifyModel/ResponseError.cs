namespace SpotifyToolsLib.Spotify.SpotifyModel
{
    public class ResponseError
    {
        public Error error { get; set; }
    }

    public class Error
    {
        public int status { get; set; }
        public string message { get; set; }
    }
}
