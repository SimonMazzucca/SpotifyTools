namespace SpotifyToolsLib.Shazam
{
    public class MyShazamSongs
    {
        public Tag[] tags { get; set; }
        public string token { get; set; }
    }

    public class Tag
    {
        public string tagid { get; set; }
        public string key { get; set; }
        public long timestamp { get; set; }
        public string timezone { get; set; }
        public Geolocation geolocation { get; set; }
        public string type { get; set; }
        public Track track { get; set; }
    }

    public class Geolocation
    {
        public float altitude { get; set; }
        public float longitude { get; set; }
        public float latitude { get; set; }
    }

    public class Track
    {
        public string type { get; set; }
        public string key { get; set; }
        public Heading heading { get; set; }
        public Images images { get; set; }
        public Stores stores { get; set; }
        public Streams streams { get; set; }
        public object[] modules { get; set; }
        public Content content { get; set; }
        public Artist[] artists { get; set; }
        public Share share { get; set; }
        public Footnote[] footnotes { get; set; }
        public string alias { get; set; }
        public string url { get; set; }
        public Action4[] actions { get; set; }
        public Urlparams urlparams { get; set; }
    }

    public class Heading
    {
        public string title { get; set; }
        public string subtitle { get; set; }
    }

    public class Images
    {
        public string _default { get; set; }
        public string blurred { get; set; }
        public string play { get; set; }
    }

    public class Stores
    {
        public Apple apple { get; set; }
        public Itunes itunes { get; set; }
        public Google google { get; set; }
        public Claromusicasearch claromusicasearch { get; set; }
    }

    public class Apple
    {
        public Action[] actions { get; set; }
        public Images1 images { get; set; }
        public bool _explicit { get; set; }
        public string previewurl { get; set; }
        public string trackid { get; set; }
        public string productid { get; set; }
    }

    public class Images1
    {
        public string _default { get; set; }
        public string blurred { get; set; }
        public string play { get; set; }
    }

    public class Action
    {
        public string type { get; set; }
        public string uri { get; set; }
    }

    public class Itunes
    {
        public Action1[] actions { get; set; }
        public Images2 images { get; set; }
        public bool _explicit { get; set; }
        public string previewurl { get; set; }
        public string trackid { get; set; }
        public string productid { get; set; }
    }

    public class Images2
    {
        public string _default { get; set; }
        public string blurred { get; set; }
        public string play { get; set; }
    }

    public class Action1
    {
        public string type { get; set; }
        public string uri { get; set; }
    }

    public class Google
    {
        public Action2[] actions { get; set; }
        public string previewurl { get; set; }
        public string trackid { get; set; }
        public string productid { get; set; }
    }

    public class Action2
    {
        public string type { get; set; }
        public string uri { get; set; }
    }

    public class Claromusicasearch
    {
        public Action3[] actions { get; set; }
    }

    public class Action3
    {
        public string name { get; set; }
        public string type { get; set; }
        public string uri { get; set; }
    }

    public class Streams
    {
    }

    public class Content
    {
        public Recommendations recommendations { get; set; }
        public Videos videos { get; set; }
        public Artiststoptracks artiststoptracks { get; set; }
    }

    public class Recommendations
    {
        public string key { get; set; }
        public string href { get; set; }
    }

    public class Videos
    {
        public string href { get; set; }
    }

    public class Artiststoptracks
    {
        public string href { get; set; }
    }

    public class Share
    {
        public string subject { get; set; }
        public string text { get; set; }
        public string href { get; set; }
        public string image { get; set; }
        public string twitter { get; set; }
        public string html { get; set; }
        public string avatar { get; set; }
        public string snapchat { get; set; }
    }

    public class Urlparams
    {
        public string tracktitle { get; set; }
        public string trackartist { get; set; }
    }

    public class Artist
    {
        public Follow follow { get; set; }
        public string alias { get; set; }
        public string id { get; set; }
    }

    public class Follow
    {
        public string followkey { get; set; }
    }

    public class Footnote
    {
        public string title { get; set; }
        public string value { get; set; }
    }

    public class Action4
    {
        public string name { get; set; }
        public string type { get; set; }
        public string id { get; set; }
    }

}
