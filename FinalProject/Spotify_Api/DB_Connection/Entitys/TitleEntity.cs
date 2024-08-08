namespace Spotify_Api.DB_Connection.Entitys
{
    public class TitleEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int ArtistID { get; set; }
        public ArtistEntity Artist { get; set; }

        public int GenreId {  get; set; }
        public GenreEntity Genre { get; set; }

        // Others 

        public ICollection<PlaylistTitleEntity> Playlists { get; set; } = new List<PlaylistTitleEntity>();

    }
}
