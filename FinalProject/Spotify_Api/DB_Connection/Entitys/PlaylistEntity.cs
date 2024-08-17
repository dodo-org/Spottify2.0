namespace Spotify_Api.DB_Connection.Entitys
{
    public class PlaylistEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserID { get; set; }
        public UserEntity User { get; set; }

        // Fremddaten 

        public ICollection<PlaylistTitleEntity> TitlePlaylists { get; set; }
    }
}
