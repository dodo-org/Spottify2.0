using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Spotify_Api.DB_Connection.Entitys;

namespace Spotify_Api.Models.Reply
{
    public class TitleSearchReply_Model 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ArtistID { get; set; }
        public string Artist { get; set; }

        public TitleSearchReply_Model(TitleEntity title)
        {
            Id = title.Id;
            Name = title.Name;
            Description = title.Description;
            ArtistID = title.ArtistID;
            Artist = title.Artist.StageName;
        }
    }
}
