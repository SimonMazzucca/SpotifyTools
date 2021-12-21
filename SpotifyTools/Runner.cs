using log4net;
using SpotifyToolsLib.Spotify;
using SpotifyToolsLib.Utilities;
using System.Reflection;

namespace SpotifyTools
{
    public class Runner
    {
        private readonly ILog _Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public void Run()
        {
            _Log.Debug("Test");
            SpotifyAdapter spotify = new SpotifyAdapter(new SettingsFacade());
            var test = spotify.PlaylistExists("Test");
        }

    }
}
