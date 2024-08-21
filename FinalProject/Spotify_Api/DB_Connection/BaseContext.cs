using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Spotify_Api.DB_Connection.Config;
using Spotify_Api.DB_Connection.Entitys;
using Cassandra;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;

namespace Spotify_Api.DB_Connection
{
    public class BaseContext : DbContext
    {

        //public DbSet<AlbumEntity> Album { get; set; }
        public DbSet<ArtistEntity> Artist { get; set; }
        //public DbSet<GenreEntity> Genres { get; set; }
        public DbSet<PlaylistEntity> Playlist { get; set; }
        public DbSet<PlaylistTitleEntity> Playlist_Title { get; set; }
        public DbSet<UserEntity> User { get; set; }
        public DbSet<TitleEntity> Title { get; set; }




        public BaseContext() : base()
        {
        }

        public BaseContext(DbContextOptions<BaseContext> options)
        : base(options)
        {
        }


        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {

            
            if (!builder.IsConfigured)
            {
                
                //var connectionString = "Host=postgres;Database=mydb;Username=user;Password=password;Port=5432";
                var connectionString = "Host=localhost;Database=mydb;Username=user;Password=password;Port=5432";
                builder.UseNpgsql(connectionString);


            }
        }

        public void EnsureCreated()
        {
            if (Database.GetPendingMigrations().Any())
            {
                
                Database.Migrate();
            }
            else if (Database.HasPendingModelChanges())
            {
                throw new NotImplementedException("Erstellen sie eine Migration");
            }
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Hier kannst du zusätzliche Konfigurationen für deine Modelle hinzufügen.
            //_ = modelBuilder.ApplyConfiguration(new AlbumConfig());
            _ = modelBuilder.ApplyConfiguration(new ArtistConfig());
            //_ = modelBuilder.ApplyConfiguration(new GenreConfig());
            _ = modelBuilder.ApplyConfiguration(new PlaylistConfig());
            _ = modelBuilder.ApplyConfiguration(new PlaylistTitleConfig());
            _ = modelBuilder.ApplyConfiguration(new UserConfig());
            _ = modelBuilder.ApplyConfiguration(new TitleConfig());

            //base.OnModelCreating(modelBuilder);
        }

        // DbSets für deine Modelle


        // befehl: Add-Migration <name>
    }
}
