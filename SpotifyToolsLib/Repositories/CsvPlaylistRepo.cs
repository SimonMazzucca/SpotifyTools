using CsvHelper;
using SpotifyToolsLib.Spotify;
using System.Collections.Generic;
using System.IO;

namespace SpotifyToolsLib.Repositories
{

    public class CsvPlaylistRepo : IPlaylistRepo
    {

        public virtual string Delimiter => ",";

        public Playlist GetSongList(string source)
        {
            Playlist toReturn = new Playlist(source);

            using (StreamReader reader = new StreamReader(source))
            using (CsvReader csv = new CsvReader(reader))
            {
                csv.Configuration.Delimiter = Delimiter;

                Song songRecord = new Song();
                IEnumerable<Song> records = csv.EnumerateRecords(songRecord);

                foreach (Song song in records)
                {
                    toReturn.Songs.Add(song);
                }
            }

            return toReturn;
        }

    }
}
