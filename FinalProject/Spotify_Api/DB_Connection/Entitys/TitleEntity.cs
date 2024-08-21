using System.Text.Json.Serialization;

namespace Spotify_Api.DB_Connection.Entitys
{
    public class TitleEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ArtistID { get; set; }
        public ArtistEntity Artist { get; set; }

        //public int GenreId {  get; set; }
        //public GenreEntity Genre { get; set; }

        //public int AlbumId { get; set; }
        //public AlbumEntity Album { get; set; }

        // Others 
        [JsonIgnore]
        public ICollection<PlaylistTitleEntity> TitlePlaylists { get; set; } 

    }
}
