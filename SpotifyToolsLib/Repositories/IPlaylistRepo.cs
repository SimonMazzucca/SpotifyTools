using SpotifyToolsLib.Spotify;

namespace SpotifyToolsLib.Repositories
{
    public interface IPlaylistRepo
    {
        Playlist GetSongList(string source);
    }
}