using System;

namespace SpotifyTools
{
    class Program
    {
        static void Main(string[] args)
        {

            try
            {
                Runner runner = new Runner();
                runner.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            Console.WriteLine("Done");
            Console.ReadLine();
        }
    }
}
