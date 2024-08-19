using Spotify_Api.DB_Connection.Entitys;

namespace Spotify_Api.DB_Connection.TestData
{
    public class CreateAndAplyTestData
    {
        public List<UserEntity> Users = new List<UserEntity>();
        public List<ArtistEntity> Artists = new List<ArtistEntity>();
        public List<AlbumEntity> Albums = new List<AlbumEntity>();
        public List<GenreEntity> Genres = new List<GenreEntity>();
        public List<TitleEntity> Titles = new List<TitleEntity>();

        CreateAndAplyTestData()
        {
            Users.Add(new UserEntity()
            {
                Id = 1,
                UserName = "User1",
                Email = "User1@gmail.com",
                Password = "User1",
            });
            Users.Add(new UserEntity()
            {
                Id = 2,
                UserName = "User2",
                Email = "User2@gmail.com",
                Password = "User2",
            });
            Users.Add(new UserEntity()
            {
                Id = 3,
                UserName = "User3",
                Email = "User3@gmail.com",
                Password = "User3",
            });

            Artists.Add(new ArtistEntity()
            {
                Id = 1,
                FName = "Farrokh",
                LName = "Bulsara",
                StageName = "Freddy Mercury"
            });

            Artists.Add(new ArtistEntity
            {
                Id = 2,
                FName = "Marshall",
                LName = "Mathers",
                StageName = "Eminem"
            })

        }


        public void ApplyTestData()
        {
            
        }
    }
}
