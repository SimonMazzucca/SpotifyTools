using CsvHelper;
using System.Collections.Generic;
using System.IO;

namespace SpotifyToolsLib.Repositories
{

    public class CsvPlaylistRepo : IPlaylistRepo
    {

        public virtual string Delimiter => ",";

        public SongList GetSongList(string source)
        {
            SongList toReturn = new SongList();

            using (StreamReader reader = new StreamReader(source))
            using (CsvReader csv = new CsvReader(reader))
            {
                csv.Configuration.Delimiter = Delimiter;

                Song songRecord = new Song();
                IEnumerable<Song> records = csv.EnumerateRecords(songRecord);

                foreach (Song song in records)
                {
                    toReturn.Add(song);
                }
            }

            return toReturn;
        }

    }
}
