using Newtonsoft.Json;
using SpotifyToolsLib.Spotify.SpotifyModel;
using SpotifyToolsLib.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpotifyToolsLib.Spotify
{
    public class Authentication : BaseModel
    {
        public static string ClientId { get; set; }

        public static string ClientSecret { get; set; }

        public static string RedirectUri { get; set; }

        public async static Task<AuthenticationToken> GetAccessToken(string code)
        {
            Dictionary<string, string> postData = new Dictionary<string, string>();
            postData.Add("grant_type", "authorization_code");
            postData.Add("code", code);
            postData.Add("redirect_uri", RedirectUri);
            postData.Add("client_id", ClientId);
            postData.Add("client_secret", ClientSecret);

            var json = await HttpHelper.Post("https://accounts.spotify.com/api/token", postData);
            var obj = JsonConvert.DeserializeObject<accesstoken>(json, new JsonSerializerSettings());

            return obj.ToPOCO();
        }
    }
}
