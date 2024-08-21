using Spotify_Api.DB_Connection.Entitys;

namespace Spotify_Api.DB_Connection.TestData
{
    public class CreateAndAplyTestData
    {
        public List<UserEntity> Users = new List<UserEntity>();
        public List<ArtistEntity> Artists = new List<ArtistEntity>();
        //public List<AlbumEntity> Albums = new List<AlbumEntity>();
        //public List<GenreEntity> Genres = new List<GenreEntity>();
        public List<TitleEntity> Titles = new List<TitleEntity>();

        public CreateAndAplyTestData()
        {
            // Add User
            Users.Add(new UserEntity()
            {
                Id = 1,
                UserName = "Dominic",
                Email = "Dominic@gmail.com",
                Password = "1234",
            });
            Users.Add(new UserEntity()
            {
                Id = 2,
                UserName = "Friedemann",
                Email = "Friedemann2@gmail.com",
                Password = "1234",
            });
            Users.Add(new UserEntity()
            {
                Id = 3,
                UserName = "Dennis",
                Email = "Dennis@gmail.com",
                Password = "1234",
            });


            // Add Artist

            Artists.Add(new ArtistEntity()
            {
                Id = 1,
                StageName = "Queen"
            });

            Artists.Add(new ArtistEntity
            {
                Id = 2,
                StageName = "Eminem"
            });

            Artists.Add(new ArtistEntity
            {
                Id = 3,
                StageName = "Gary Jules"
            });

            Artists.Add(new ArtistEntity
            {
                Id = 4,
                StageName = "Toto"
            });

            Artists.Add(new ArtistEntity
            {
                Id = 5,
                StageName = "A-Ha"
            });

            Artists.Add(new ArtistEntity
            {
                Id = 6,
                StageName = "ABBA"
            });

            Artists.Add(new ArtistEntity
            {
                Id = 7,
                StageName = "E.A.V."
            });

            Artists.Add(new ArtistEntity
            {
                Id = 8,
                StageName = "Peter Schilling"
            });

            Artists.Add(new ArtistEntity
            {
                Id = 9,
                StageName = "Lady Gaga"
            });


            // Add titles

            Titles.Add(new TitleEntity()
            {
                Id = 1,
                Name = "Mad World",
                ArtistID = 3,
            });

            Titles.Add(new TitleEntity()
            {
                Id = 2,
                Name = "We Will Rock You",
                ArtistID = 1,
            });

            Titles.Add(new TitleEntity()
            {
                Id = 3,
                Name = "Africa",
                ArtistID = 4,
            });

            Titles.Add(new TitleEntity()
            {
                Id = 4,
                Name = "Bad Romance",
                ArtistID = 9,
            });

            Titles.Add(new TitleEntity()
            {
                Id = 5,
                Name = "Take On Me",
                ArtistID = 5,
            });

            Titles.Add(new TitleEntity()
            {
                Id = 6,
                Name = "Super Trooper",
                ArtistID = 6,
            });

            Titles.Add(new TitleEntity()
            {
                Id = 7,
                Name = "Just Loose it",
                ArtistID = 2,
            });

            Titles.Add(new TitleEntity()
            {
                Id = 8,
                Name = "Ba-Ba Banküberfall",
                ArtistID = 7,
            });

            Titles.Add(new TitleEntity()
            {
                Id = 9,
                Name = "Major Tom",
                ArtistID = 8,
            });

            Titles.Add(new TitleEntity()
            {
                Id = 10,
                Name = "Bohemian Rhapsody",
                ArtistID = 1,
            });
        }


        public void ApplyTestData()
        {
            using ( var context = new BaseContext())
            {
                // User
                var allUser = context.User.ToList();
                context.User.RemoveRange(allUser);
                context.SaveChanges();

                context.User.AddRange(Users);
                context.SaveChanges();

                // Artists
                var allArtist = context.Artist.ToList();
                context.Artist.RemoveRange(allArtist);
                context.SaveChanges();

                context.Artist.AddRange(Artists);
                context.SaveChanges();

                // Title
                var allTitle = context.Title.ToList();
                context.Title.RemoveRange(allTitle);
                context.SaveChanges();

                context.Title.AddRange(Titles);
                context.SaveChanges();
            }
        }
    }
}
