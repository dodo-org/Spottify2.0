namespace Spotify_Api.DB_Connection.Entitys
{
    public class GenreEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Fremddaten

        public ICollection<TitleEntity> Titles { get; set; } = new List<TitleEntity>();
    }
}
