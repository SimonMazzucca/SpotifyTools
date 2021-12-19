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

        public async Task<bool> AddSongsToPlaylist(SpotifyPlaylist playlist, IList<Song> songs)
        {
            List<string> songUris = new List<string>();

            foreach (Song song in songs)
            {
                string songUri = GetSongUri(song).Result;
                if (string.IsNullOrEmpty(songUri))
                {
                    Console.WriteLine("Song not found: {0} - {1}", song.Artist, song.Name);
                }
                else
                {
                    songUris.Add(songUri);
                }
            }
            dynamic postData = new System.Dynamic.ExpandoObject();
            postData.uris = songUris.ToArray();

            string header = JsonConvert.SerializeObject(postData);
            string url = string.Format("https://api.spotify.com/v1/playlists/{0}/tracks", playlist.id);
            string json = await HttpHelper.Post(url, this.AuthenticationToken, header);
            CheckForResponseErrors(json);

            return true;
        }

        private async Task<string> GetSongUri(Song song)
        {
            string url = string.Format("https://api.spotify.com/v1/search?q={0}&type=track", song.Query);
            string json = await HttpHelper.Get(url, this.AuthenticationToken, true);
            CheckForResponseErrors(json);

            Console.WriteLine(json);

            SpotifyTracks response = JsonConvert.DeserializeObject<SpotifyTracks>(json, new JsonSerializerSettings());

            // Assuming item 0 is the one with highest popularity
            Item[] songsFound = response?.tracks?.items;
            if (songsFound == null || songsFound.Length == 0)
                return "";
            else 
                return songsFound[0].uri;
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
