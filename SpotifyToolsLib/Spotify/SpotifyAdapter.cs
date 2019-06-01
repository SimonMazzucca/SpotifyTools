using SpotifyToolsLib.Utilities;
using System.Threading.Tasks;

namespace SpotifyToolsLib.Spotify
{
    public class SpotifyAdapter
    {
        private readonly ISettingsFacade _SettingsFacade;
        public Settings Settings { get; }

        public SpotifyAdapter(ISettingsFacade settingsFacade)
        {
            _SettingsFacade = settingsFacade;
            Settings = _SettingsFacade.GetSettings();
        }

        public SpotifyAdapter() : this(new SettingsFacade())
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

        public async Task<bool> CreatePlaylistAsync(string name)
        {
            User user = await User.GetCurrentUserProfile(this.AuthenticationToken);

            return true;
        }

        public async Task<User> GetCurrentUser()
        {
            User user = await User.GetCurrentUserProfile(this.AuthenticationToken);

            return user;
        }

    }
}
