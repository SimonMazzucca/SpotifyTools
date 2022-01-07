using log4net;
using Newtonsoft.Json;
using SpotifyToolsLib.Spotify.SpotifyModel;
using SpotifyToolsLib.Utilities;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace SpotifyToolsLib.Spotify
{
    public class Authentication : BaseModel
    {
        private static readonly ILog _Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        
        public static string ClientId { get; set; }

        public static string ClientSecret { get; set; }

        public static string RedirectUri { get; set; }

        public async static Task<AuthenticationToken> GetAccessToken(string code)
        {
            string json = "";

            try
            {
                Dictionary<string, string> postData = new Dictionary<string, string>();
                postData.Add("grant_type", "authorization_code");
                postData.Add("code", code);
                postData.Add("redirect_uri", RedirectUri);
                postData.Add("client_id", ClientId);
                postData.Add("client_secret", ClientSecret);

                json = await HttpHelper.Post("https://accounts.spotify.com/api/token", postData);
                var obj = JsonConvert.DeserializeObject<AcessToken>(json, new JsonSerializerSettings());

                return obj.ToPOCO();
            }
            catch (Exception ex)
            {
                _Log.ErrorFormat("GetAccessToken JSON,{0}", json);
                _Log.ErrorInnerException("Exception occurred,{0}", ex);
                throw;
            }
        }
    }
}
