using SpotifyToolsLib.Spotify;
using SpotifyToolsLib.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SpotifyToolsLib.Shazam
{
    public class ShazamAdapter : BaseAdapter
    {
        public ShazamAdapter() : base()
        {
        }

        public ShazamAdapter(ISettingsFacade settingsFacade) : base(settingsFacade)
        {
        }

        public IList<Song> GetMyShazamSongs()
        {
            string url = Settings.Shazam.MyShazam;

            IDictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("cookie", Settings.Shazam.Cookie);
            Task<string> myShazamSongsJson = HttpHelper.Get(url, headers);
            IList<Song> toReturn = GetMyShazamSongs(myShazamSongsJson.Result);

            return toReturn;
        }

        public IList<Song> GetMyShazamSongs(string jsonResponse)
        {
            MyShazamSongs myShazam = Newtonsoft.Json.JsonConvert.DeserializeObject<MyShazamSongs>(jsonResponse);

            if (myShazam == null)
                return new List<Song>();


            IList<Song> toReturn = new List<Song>();

            foreach (Tag tag in myShazam.tags)
            {
                Song song = new Song(tag.track.heading.title, tag.track.heading.subtitle);
                toReturn.Add(song);

                Debug.WriteLine("{0} - {1}", song.Artist, song.Name);
            }

            return toReturn;
        }
    }
}
