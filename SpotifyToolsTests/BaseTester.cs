﻿using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace SpotifyToolsTests
{
    public abstract class BaseTester
    {

        protected string GetResourceFileContent(string filename)
        {
            string[] resources = GetType().Assembly.GetManifestResourceNames();
            string resource = resources.FirstOrDefault(r => r.EndsWith("." + filename));
            string toReturn = "";

            if (string.IsNullOrEmpty(resource))
            {
                string message = string.Format("Missing resource: {0} (Ensure BuildAction is set to 'Embedded Resource')", filename);
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
