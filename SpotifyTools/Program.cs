using log4net;
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
                _Log.InfoFormat("SpotifyTool Version {0}", Assembly.GetExecutingAssembly().GetName().Version.ToString());
                Runner runner = new Runner();
                runner.Run();
            }
            catch (Exception ex)
            {
                _Log.Error("Exception occurred", ex);
            }

            _Log.InfoFormat("Closing");
            //Console.ReadLine();
        }
    }
}
