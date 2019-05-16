using SpotifyToolsLib.Utilities;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace SpotifyToolsTests.Mocks
{
    class MockFileAccess : ISettingsFileAccess
    {
        private const string SETTINGS_FILE = "Settings.json";

        public string Load()
        {
            string[] resources = GetType().Assembly.GetManifestResourceNames();
            string resource = resources.FirstOrDefault(r => r.EndsWith("." + SETTINGS_FILE));
            string toReturn = "";

            if (string.IsNullOrEmpty(resource))
            {
                string message = string.Format("Missing resource: {0} (Ensure BuildAction is set to 'Embedded Resource')", SETTINGS_FILE);
                throw new Exception(message);
            }
            else
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                using (Stream stream = assembly.GetManifestResourceStream(resource))
                using (StreamReader reader = new StreamReader(stream))
                {
                    toReturn = reader.ReadToEnd();
                }

                return toReturn;
            }
        }
    }
}
