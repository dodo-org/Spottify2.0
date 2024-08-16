using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Spotify_Api.DB_Connection.Entitys;

namespace Spotify_Api.DB_Connection.Config
{
    public class AlbumConfig : IEntityTypeConfiguration<AlbumEntity>
    {
        void IEntityTypeConfiguration<AlbumEntity>.Configure(EntityTypeBuilder<AlbumEntity> builder)
        {
            builder.ToTable("Album");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired();

            builder.HasMany(a => a.Titles)
              .WithOne(t => t.Album)
              .HasForeignKey(t => t.AlbumId);
        }
    }
}
