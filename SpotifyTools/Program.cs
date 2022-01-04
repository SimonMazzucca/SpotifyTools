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
                await runner.CreatePlaylist("!Tonight");
            }
            catch (Exception ex)
            {
                string cleanedEx = ex.Message.Replace("\r\n", " ");
                if (ex.InnerException == null)
                    cleanedEx = ex.Message.Replace("\r\n", " ");
                else if (ex.InnerException.InnerException == null)
                    cleanedEx = ex.InnerException.Message.Replace("\r\n", " ");
                else
                    cleanedEx = ex.InnerException.InnerException.Message.Replace("\r\n", " ");
                _Log.ErrorFormat("Exception occurred,{0}", cleanedEx);
            }

            _Log.InfoFormat("Donzo. This was fun.");
        }
    }
}
