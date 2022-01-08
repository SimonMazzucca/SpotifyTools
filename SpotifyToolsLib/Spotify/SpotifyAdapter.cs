using log4net;
using Newtonsoft.Json;
using SpotifyToolsLib.Spotify.SpotifyModel;
using SpotifyToolsLib.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SpotifyToolsLib.Spotify
{

    /// <summary>
    /// https://developer.spotify.com/console/playlists/
    /// </summary>
    public class SpotifyAdapter : BaseAdapter
    {
        private readonly ILog _Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private const int SONGS_PER_REQ = 50;

        public SpotifyAdapter() : base()
        {
        }

        public SpotifyAdapter(ISettingsFacade settingsFacade) : base(settingsFacade)
        {
        }

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
        /// API: https://developer.spotify.com/documentation/web-api/reference/#/operations/reorder-or-replace-playlists-tracks
        /// 
        /// Header:
        /// {
        ///     "range_start": 1,
        ///     "insert_before": 3,
        ///     "range_length": 2
        /// }
        /// 
        /// Scope:
        /// Had to add: playlist-modify-public
        /// </summary>
        public void SortPlaylist(SpotifyPlaylist playlist)
        {
            dynamic postData = new System.Dynamic.ExpandoObject();
            postData.range_start = 13;
            postData.insert_before = 0;

            string header = JsonConvert.SerializeObject(postData);
            string url = string.Format("https://api.spotify.com/v1/playlists/{0}/tracks", playlist.id);
            string json = HttpHelper.Put(url, this.AuthenticationToken, header).Result;
            CheckForResponseErrors(json);
        }

        public bool AddSongsToPlaylist(SpotifyPlaylist playlist, IList<Song> songs)
        {
            int requests = (songs.Count - 1) / SONGS_PER_REQ + 1;

            for (int i = 0; i < requests; i++)
            {
                AddSongsToPlaylistPartial(playlist, songs, i);
            }

            return true;
        }

        private void AddSongsToPlaylistPartial(SpotifyPlaylist playlist, IList<Song> songs, int iteration)
        {
            List<string> songUris = new List<string>();
            int fromIndex = iteration * SONGS_PER_REQ;
            int toIndex = Math.Min(songs.Count - 1, fromIndex + SONGS_PER_REQ);

            // TODO: am I missing the last song here?
            for (int i = fromIndex; i < toIndex; i++)
            {
                Song song = songs[i];
                string songUri = GetSongUri(song).Result;
                if (string.IsNullOrEmpty(songUri))
                {
                    _Log.InfoFormat("[{2}/{3}] Song not found,\"{0} - {1}\"", song.Artist.ToLogSafe(), song.Name.ToLogSafe(), i + 1, songs.Count);
                }
                else
                {
                    songUris.Add(songUri);
                    _Log.DebugFormat("[{2}/{3}] Song added,\"{0} - {1}\"", song.Artist, song.Name, i + 1, songs.Count);
                }
            }
            dynamic postData = new System.Dynamic.ExpandoObject();
            postData.uris = songUris.ToArray();

            string header = JsonConvert.SerializeObject(postData);
            string url = string.Format("https://api.spotify.com/v1/playlists/{0}/tracks", playlist.id);
            string json = HttpHelper.Post(url, this.AuthenticationToken, header).Result;
            CheckForResponseErrors(json);
        }

        private async Task<string> GetSongUri(Song song)
        {
            string url = string.Format("https://api.spotify.com/v1/search?q={0}&type=track", song.Query);
            string json = await HttpHelper.Get(url, this.AuthenticationToken, true);
            CheckForResponseErrors(json);

            SpotifyTracks response = JsonConvert.DeserializeObject<SpotifyTracks>(json, new JsonSerializerSettings());
            Item[] songsFound = response?.tracks?.items;

            // Nothing at all found
            if (songsFound == null || songsFound.Length == 0 || songsFound[0].artists == null)
                return "";

            // Find best match
            foreach (var candidate in songsFound)
            {
                // Ignore "(with" and "[" in my songs
                if (candidate.name.ToLower() != song.JustName.ToLower())
                    continue;
                // Ignore "The " on either side
                if (candidate.artists[0].name.TrimThe().ToLower() == song.Artist.TrimThe().ToLower())
                    return candidate.uri;
            }

            return "";
        }

        public SpotifyPlaylist PlaylistExists(string name)
        {
            SpotifyPlaylist found = GetPlaylist(name);
            return found;
        }

        public SpotifyPlaylist GetPlaylist(string name)
        {
            List<SpotifyPlaylist> all = GetAllPlaylists().Result;

            foreach (var item in all)
            {
                _Log.Info(item.name);
            }

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

            SpotifyPlaylistCollection partial = JsonConvert.DeserializeObject<SpotifyPlaylistCollection>(json, new JsonSerializerSettings());
            List<SpotifyPlaylist> toReturn = new List<SpotifyPlaylist>(partial.items);

            while (!string.IsNullOrEmpty(partial.next))
            {
                json = await HttpHelper.Get(partial.next, this.AuthenticationToken, true);
                CheckForResponseErrors(json);
                partial = JsonConvert.DeserializeObject<SpotifyPlaylistCollection>(json, new JsonSerializerSettings());
                toReturn.AddRange(partial.items);

            }
            if (partial.total == toReturn.Count)
                _Log.InfoFormat("Playlists found,{0}", toReturn.Count);
            else
                _Log.InfoFormat("Retrieved only {1} playlists out of {1}", toReturn.Count, partial.total);

            return toReturn;
        }

        public async Task<SpotifyPlaylist> CreatePlaylist(string name, bool isPublic)
        {
            _Log.InfoFormat("Creating playlist,{0}", name);

            dynamic postData = new System.Dynamic.ExpandoObject();
            postData.name = name;
            postData.@public = isPublic;

            string header = JsonConvert.SerializeObject(postData);
            string json = await HttpHelper.Post("https://api.spotify.com/v1/me/playlists", this.AuthenticationToken, header);
            CheckForResponseErrors(json);

            SpotifyPlaylist toReturn = JsonConvert.DeserializeObject<SpotifyPlaylist>(json, new JsonSerializerSettings());
            _Log.InfoFormat("Playlists created,{0} [ID: {1}]", toReturn.name, toReturn.id);

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
