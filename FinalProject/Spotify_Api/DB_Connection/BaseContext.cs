using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Spotify_Api.DB_Connection.Config;
using Spotify_Api.DB_Connection.Entitys;
using Cassandra;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Spotify_Api.DB_Connection
{
    public class BaseContext : DbContext
    {
         
        public DbSet<AlbumEntity> Album { get; set; }
        public DbSet<ArtistEntity> Artist { get; set; }
        public DbSet<GenreEntity> Genres { get; set; }
        public DbSet<PlaylistEntity> Playlist { get; set; }
        public DbSet<PlaylistTitleEntity> Playlist_Title { get; set; }
        public DbSet<UserEntity> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {

            // Connect to Postgress DB 
            // Verbindungseinstellungen
            string host = "localhost";
            string port = "5432";
            string username = "user";
            string password = "password";
            string database = "mydb";

            // Verbindungszeichenfolge
            string connectionString = $"Host={host};Port={port};Username={username};Password={password};Database={database}";

        }

    }
}
