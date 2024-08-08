namespace Spotify_Api.DB_Connection.Entitys
{
    public class AlbumEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<AlbumTitleEntity> AlbumTitel { get; set; } = new List<AlbumTitleEntity>();
    }
}
