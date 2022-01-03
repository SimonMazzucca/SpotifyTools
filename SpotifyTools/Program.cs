using log4net;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace SpotifyTools
{
    class Program
    {

        private static readonly ILog _Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        static async Task Main(string[] args)
        {

            try
            {
                _Log.InfoFormat("SpotifyTool Version,{0}", Assembly.GetExecutingAssembly().GetName().Version.ToString());
                Runner runner = new Runner();
                await runner.CreatePlaylist("TestPlaylist");
            }
            catch (Exception ex)
            {
                _Log.Error("Exception occurred", ex);
            }

            _Log.InfoFormat("Donzo. This was fun.");
        }
    }
}
