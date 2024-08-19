namespace Spotify_Api.DB_Connection.Entitys
{
    public class PlaylistTitleEntity
    {
        public int PlaylistId { get; set; }
        public PlaylistEntity Playlist { get; set; }
        public int TitleId { get; set; }
        public TitleEntity Title { get; set; }
    }
}
