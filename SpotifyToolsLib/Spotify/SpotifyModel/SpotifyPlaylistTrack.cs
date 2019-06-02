using Newtonsoft.Json;
using System;

namespace SpotifyToolsLib.Spotify.SpotifyModel
{
    [JsonObject]
    public class SpotifyPlaylistTrack
    {
        public string added_at { get; set; }
        public SpotifyUser added_by { get; set; }
        public SpotifyTrack track { get; set; }

        //public SpotifyPlaylistTrack ToPOCO()
        //{
        //    SpotifyPlaylistTrack pt = new SpotifyPlaylistTrack();

        //    DateTime addedAt;

        //    if (DateTime.TryParse(this.added_at, out addedAt))
        //        pt.AddedAt = addedAt;
        //    else
        //        pt.AddedAt = DateTime.Now;

        //    if (this.added_by != null)
        //        pt.AddedBy = this.added_by.ToPOCO();

        //    pt.Track = this.track.ToPOCO();

        //    return pt;
        //}
    }
}
