using Newtonsoft.Json;
using SpotifyToolsLib.Spotify.SpotifyModel;
using SpotifyToolsLib.Utilities;
using System;
using System.Threading.Tasks;

namespace SpotifyToolsLib.Spotify
{
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


        //TODO: remove all TypeNameAssemblyFormat
        public async Task<playlist> CreatePlaylist(string name, bool isPublic)
        {

            dynamic postData = new System.Dynamic.ExpandoObject();
            postData.name = name;
            postData.@public = isPublic;

            string jsonInput = JsonConvert.SerializeObject(postData);
            string json = await HttpHelper.Post("https://api.spotify.com/v1/me/playlists", this.AuthenticationToken, jsonInput);
            CheckForExpiredToken(json);

            playlist toReturn = JsonConvert.DeserializeObject<playlist>(json, new JsonSerializerSettings());

            return toReturn;
        }

        //TODO: refactor 
        private static void CheckForExpiredToken(string json)
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
