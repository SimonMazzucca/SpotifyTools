using SpotifyToolsLib.Spotify;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SpotifyToolsLib.Shazam
{
    public class ShazamAdapter
    {
        public IList<Song> GetMyShazamSongs(string jsonResponse)
        {
            MyShazamSongs myShazam = Newtonsoft.Json.JsonConvert.DeserializeObject<MyShazamSongs>(jsonResponse);
            Console.WriteLine("");

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
