using iTunesLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyTools
{
    class Program
    {
        static void Main(string[] args)
        {
            iTunesApp app = new iTunesApp();
            IITSource library = app.Sources.ItemByName["Library"];

            foreach (IITPlaylist item in library.Playlists)
            {
                Debug.WriteLine(item.Name);
                //yield return item.Name;
            }

        }
    }
}
