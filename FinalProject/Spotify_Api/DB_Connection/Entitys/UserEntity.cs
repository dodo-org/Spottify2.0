namespace Spotify_Api.DB_Connection.Entitys
{
    public class UserEntity
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public DateTime TokenCreatedAt { get; set; }

        // Fremddaten

        public ICollection<PlaylistEntity> Playlists { get; set; }
    }
}
