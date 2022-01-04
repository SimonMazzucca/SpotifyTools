using System;
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

        public string JustName { 
            get {
                int pPos = Name.ToLower().IndexOf("(with");
                int bPos = Name.IndexOf("[");

                string toReturn = "";

                if (pPos >= 0 && bPos >= 0)
                    toReturn = Name.Substring(0, Math.Min(pPos, bPos));
                else if (pPos >= 0)
                    toReturn = Name.Substring(0, pPos);
                else if (bPos >= 0)
                    toReturn = Name.Substring(0, bPos);
                else
                    toReturn = Name;

                return toReturn.Trim();
            }
        }
        public string Artist { get; set; }

        public string Query
        {
            get
            {
                const string URI_TEMPLATE = "track:{0}%20artist:{1}";
                string uri = string.Format(
                    URI_TEMPLATE,
                    HttpUtility.UrlEncode(JustName),
                    HttpUtility.UrlEncode(Artist)
                    );

                return uri;
            }
        }
    }
}
