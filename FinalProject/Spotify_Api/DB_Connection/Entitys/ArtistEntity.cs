namespace Spotify_Api.DB_Connection.Entitys
{
    public class ArtistEntity
    {
        public int Id { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string StageName { get; set; }


        // Fremde Daten

        public ICollection<TitleEntity> Titles { get; set; }
    }
}
