using log4net;
using System;

namespace SpotifyToolsLib
{
    public static class Extensions
    {

        public static string ToLogSafe(this string str)
        {
            if (str.IndexOf(",") >= 0)
                return "\"" + str + "\"";
            else
                return str;
        }

        public static string TrimThe(this string str)
        {
            if (str.ToLower().StartsWith("the "))
                return str.Substring(4);
            else
                return str;
        }

        public static void ErrorInnerException(this ILog log, string format, Exception ex)
        {
            string cleanedEx = ex.Message.Replace("\r\n", " ");
            if (ex.InnerException == null)
                cleanedEx = ex.Message.Replace("\r\n", " ");
            else if (ex.InnerException.InnerException == null)
                cleanedEx = ex.InnerException.Message.Replace("\r\n", " ");
            else
                cleanedEx = ex.InnerException.InnerException.Message.Replace("\r\n", " ");

            log.ErrorFormat("Exception occurred,{0}", cleanedEx);
        }

    }
}
