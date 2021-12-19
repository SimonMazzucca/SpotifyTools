using System.Web;

namespace SpotifyToolsLib.Spotify
{
    public class Song
    {
        public Song()
        {

        }
        public Song(string name, string artist)
        {
            Name = name;
            Artist = artist;
        }

        public string Name { get; set; }
        public string Artist { get; set; }

        public string Query
        {
            get
            {
                const string URI_TEMPLATE = "track:{0}%20artist:{1}";
                string uri = string.Format(
                    URI_TEMPLATE,
                    HttpUtility.UrlEncode(Name),
                    HttpUtility.UrlEncode(Artist)
                    );

                return uri;
            }
        }
    }
}
