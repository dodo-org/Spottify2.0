using System.Text.Json.Serialization;

namespace Spotify_Api.DB_Connection.Entitys
{
    public class ArtistEntity
    {
        public int Id { get; set; }
        public string StageName { get; set; }


        // Fremde Daten
        [JsonIgnore]
        public ICollection<TitleEntity> Titles { get; set; }
    }
}
