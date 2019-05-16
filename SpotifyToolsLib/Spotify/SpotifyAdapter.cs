using SpotifyToolsLib.Utilities;
using System.Threading.Tasks;

namespace SpotifyToolsLib.Spotify
{
    public class SpotifyAdapter
    {
        ISettingsFacade _SettingsFacade;
        Settings _Settings;

        public AuthenticationToken AuthenticationToken
        {
            get
            {
                return new AuthenticationToken()
                {
                    AccessToken = _Settings.Spotify.AuthToken.AccessToken,
                    ExpiresOn = _Settings.Spotify.AuthToken.ExpiresOn,
                    RefreshToken = _Settings.Spotify.AuthToken.RefreshToken,
                    TokenType = _Settings.Spotify.AuthToken.TokenType,
                };
            }
            set
            {
                _Settings.Spotify.AuthToken.AccessToken = value.AccessToken;
                _Settings.Spotify.AuthToken.ExpiresOn = value.ExpiresOn;
                _Settings.Spotify.AuthToken.RefreshToken = value.RefreshToken;
                _Settings.Spotify.AuthToken.TokenType = value.TokenType;
            }
        }

        public SpotifyAdapter(ISettingsFacade settingsFacade)
        {
            _SettingsFacade = settingsFacade;
        }

        public SpotifyAdapter()
        {
            _SettingsFacade = new SettingsFacade();
        }

        public async Task<bool> CreatePlaylistAsync(string name)
        {
            User user = await User.GetCurrentUserProfile(this.AuthenticationToken);

            return true;
        }

    }
}
