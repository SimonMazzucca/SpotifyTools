using CsvHelper;
using SpotifyToolsLib.Spotify;
using System.Collections.Generic;
using System.IO;

namespace SpotifyToolsLib.Repositories
{

    public class CsvPlaylistRepo : IPlaylistRepo
    {

        public virtual string Delimiter => ",";

        /// <summary>
        /// Note: this below did not work. Return same (last) record multiple times.
        /// IEnumerable<Song> records = csv.EnumerateRecords(songRecord);
        /// </summary>
        public Playlist GetSongList(string source)
        {
            Playlist toReturn = new Playlist(source);

            using (StreamReader reader = new StreamReader(source))
            using (CsvReader csv = new CsvReader(reader))
            {
                csv.Configuration.Delimiter = Delimiter;

                IEnumerable<Song> songs = csv.GetRecords<Song>();

                foreach (Song song in songs)
                {
                    toReturn.Songs.Add(song);
                }
            }

            return toReturn;
        }
    }
}
