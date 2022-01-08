using log4net;
using SpotifyToolsLib;
using System;
using System.Reflection;

namespace SpotifyTools
{
    class Program
    {

        private static readonly ILog _Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        static void Main(string[] args)
        {

            try
            {
                _Log.InfoFormat("SpotifyTool Version,{0}", Assembly.GetExecutingAssembly().GetName().Version.ToString());
                Runner runner = new Runner();
                //runner.CreatePlaylist("!Tonight");
                runner.SortPlaylist("New Releases");
            }
            catch (Exception ex)
            {
                _Log.ErrorInnerException("Exception occurred,{0}", ex);
            }

            _Log.InfoFormat("Donzo. This was fun.");
        }
    }
}
