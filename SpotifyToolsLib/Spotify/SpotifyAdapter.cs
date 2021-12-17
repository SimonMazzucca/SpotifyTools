using Newtonsoft.Json;
using SpotifyToolsLib.Spotify.SpotifyModel;
using SpotifyToolsLib.Utilities;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpotifyToolsLib.Spotify
{

    /// <summary>
    /// https://developer.spotify.com/console/playlists/
    /// </summary>
    public class SpotifyAdapter : BaseAdapter
    {
        public SpotifyAdapter() : base()
        {
        }

        public SpotifyAdapter(ISettingsFacade settingsFacade) : base(settingsFacade)
        {
        }

        //TODO: use Settings directly
        public AuthenticationToken AuthenticationToken
        {
            get
            {
                return new AuthenticationToken()
                {
                    AccessToken = Settings.Spotify.AuthToken.AccessToken,
                    ExpiresOn = Settings.Spotify.AuthToken.ExpiresOn,
                    RefreshToken = Settings.Spotify.AuthToken.RefreshToken,
                    TokenType = Settings.Spotify.AuthToken.TokenType,
                };
            }
            set
            {
                Settings.Spotify.AuthToken.AccessToken = value.AccessToken;
                Settings.Spotify.AuthToken.ExpiresOn = value.ExpiresOn;
                Settings.Spotify.AuthToken.RefreshToken = value.RefreshToken;
                Settings.Spotify.AuthToken.TokenType = value.TokenType;
            }
        }

        /// <summary>
        /// TODO:
        /// - Logic to get track info and build URI array
        /// </summary>
        public async Task<bool> AddSongsToPlaylist(SpotifyPlaylist playlist, IList<Song> songs)
        {
            dynamic postData = new System.Dynamic.ExpandoObject();
            postData.uris = new string[] { "spotify:track:4iV5W9uYEdYUVa79Axb7Rh", "spotify:track:1301WleyT98MSxVHPZCA6M", "spotify:episode:512ojhOuo1ktJprKbVcKyQ" };

            string header = JsonConvert.SerializeObject(postData);
            string url = string.Format("https://api.spotify.com/v1/playlists/{0}/tracks", playlist.id);
            string json = await HttpHelper.Post(url, this.AuthenticationToken, header);
            CheckForResponseErrors(json);

            return true;
        }

        public bool PlaylistExists(string name)
        {
            SpotifyPlaylist found = GetPlaylist(name);
            return found != null;
        }

        public SpotifyPlaylist GetPlaylist(string name)
        {
            List<SpotifyPlaylist> all = GetAllPlaylists().Result;
            SpotifyPlaylist toReturn = all.FirstOrDefault(p => p.name.Equals(name));

            return toReturn;
        }

        /// <summary>
        /// https://developer.spotify.com/console/get-current-user-playlists/
        /// 
        /// TODO: add iteration for accounts with more than 50 playlists
        /// </summary>
        public async Task<List<SpotifyPlaylist>> GetAllPlaylists()
        {
            string json = await HttpHelper.Get("https://api.spotify.com/v1/me/playlists", this.AuthenticationToken, true);
            CheckForResponseErrors(json);

            Console.WriteLine(json);

            SpotifyPlaylistCollection partial = JsonConvert.DeserializeObject<SpotifyPlaylistCollection>(json, new JsonSerializerSettings());
            List<SpotifyPlaylist> toReturn = new List<SpotifyPlaylist>(partial.items);

            Console.WriteLine("Playlists found: {0}", toReturn.Count);
            return toReturn;
        }

        public async Task<SpotifyPlaylist> CreatePlaylist(string name, bool isPublic)
        {
            dynamic postData = new System.Dynamic.ExpandoObject();
            postData.name = name;
            postData.@public = isPublic;

            string header = JsonConvert.SerializeObject(postData);
            string json = await HttpHelper.Post("https://api.spotify.com/v1/me/playlists", this.AuthenticationToken, header);
            CheckForResponseErrors(json);

            SpotifyPlaylist toReturn = JsonConvert.DeserializeObject<SpotifyPlaylist>(json, new JsonSerializerSettings());

            return toReturn;
        }

        //TODO: refactor 
        private static void CheckForResponseErrors(string json)
        {
            ResponseError responseError = JsonConvert.DeserializeObject<ResponseError>(json);

            if (responseError != null && responseError.error != null)
            {
                string message = string.Format("{0}: {1}", responseError.error.status, responseError.error.message);
                throw new Exception(message);
            };

        }


        public async Task<User> GetCurrentUser()
        {
            User user = await User.GetCurrentUserProfile(this.AuthenticationToken);

            return user;
        }

    }
}
