namespace Spotify_Api.DB_Connection.Entitys
{
    public class AlbumTitleEntity
    {
        public int TitleID { get; set; }
        public TitleEntity Title { get; set; }
        public int AlbumID { get; set; }
        public AlbumEntity Album { get; set; }
    }
}
