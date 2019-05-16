using SpotifyToolsLib.Spotify;
using System;

namespace SpotifyTools
{
    class Program
    {
        static void Main(string[] args)
        {

            try
            {
                SpotifyAdapter spotify = new SpotifyAdapter();
                spotify.CreatePlaylistAsync("Test1");
                Console.WriteLine("Done.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            Console.ReadLine();
        }
    }
}
