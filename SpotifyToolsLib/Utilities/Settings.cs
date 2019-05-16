using System;

namespace SpotifyToolsLib.Utilities
{
    public class Settings
    {
        public SpotifySettings Spotify { get; set; }
        public ShazamSettings Shazam { get; set; }

        public class SpotifySettings
        {
            public AuthToken AuthToken { get; set; }
            public string ApiBaseUrl { get; set; }
            public string AuthKeyUrl { get; set; }
            public string UserId { get; set; }
            public string ClientId { get; set; }
        }

        public class AuthToken
        {
            public string AccessToken { get; set; }
            public DateTime ExpiresOn { get; set; }
            public string RefreshToken { get; set; }
            public string TokenType { get; set; }
        }

        public class ShazamSettings
        {
            public string BaseUrl { get; set; }
        }

    }
}
