using CsvHelper;
using System.Collections.Generic;
using System.IO;

namespace SpotifyToolsLib.Repositories
{

    public class ITunesExportPlaylistRepo : CsvPlaylistRepo
    {

        public override string Delimiter => "\t";

    }
}
