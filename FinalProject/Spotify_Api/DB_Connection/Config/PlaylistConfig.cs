using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Spotify_Api.DB_Connection.Entitys;

namespace Spotify_Api.DB_Connection.Config
{
    public class PlaylistConfig : IEntityTypeConfiguration<PlaylistEntity>
    {
        public void Configure(EntityTypeBuilder<PlaylistEntity> builder)
        {
            builder.ToTable("Title");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired();
            
            // Fk`s
            builder.Property(x => x.UserID).IsRequired();
            builder
                .HasOne(x => x.User)
                .WithMany(x => x.Playlists)
                .HasForeignKey(x => x.UserID)
                .IsRequired();
        }
    }
}
