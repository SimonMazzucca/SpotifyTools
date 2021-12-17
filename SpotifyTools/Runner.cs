using SpotifyToolsLib.Spotify;
using SpotifyToolsLib.Utilities;

namespace SpotifyTools
{
    public class Runner
    {
        public void Run()
        {
            SpotifyAdapter spotify = new SpotifyAdapter(new SettingsFacade());
            var test = spotify.PlaylistExists("Test");
        }

    }
}
