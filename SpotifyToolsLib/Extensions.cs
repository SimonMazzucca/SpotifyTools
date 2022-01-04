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


    }
}
