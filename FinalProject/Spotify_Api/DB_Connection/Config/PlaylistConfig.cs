using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Spotify_Api.DB_Connection.Entitys;

namespace Spotify_Api.DB_Connection.Config
{
    public class PlaylistConfig : IEntityTypeConfiguration<PlaylistEntity>
    {
        public void Configure(EntityTypeBuilder<PlaylistEntity> builder)
        {
            builder.ToTable("Playlist");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired();

            // Fk`s
            builder.HasOne(p => p.User)
               .WithMany(u => u.Playlists)
               .HasForeignKey(p => p.UserID);


        }
    }
}
