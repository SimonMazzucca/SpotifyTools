namespace SpotifyToolsLib.Repositories
{
    public interface IPlaylistRepo
    {
        SongList GetSongList(string source);
    }
}