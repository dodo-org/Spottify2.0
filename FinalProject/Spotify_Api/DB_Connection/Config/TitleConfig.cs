using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Spotify_Api.DB_Connection.Entitys;

namespace Spotify_Api.DB_Connection.Config
{
    public class TitleConfig : IEntityTypeConfiguration<TitleEntity>
    {
        void IEntityTypeConfiguration<TitleEntity>.Configure(EntityTypeBuilder<TitleEntity> builder)
        {
            builder.ToTable("Title");

            builder.HasKey(x => x.Id);

            builder.Property(x  => x.Name).IsRequired();
            builder.Property(x =>x.Description).IsRequired();

            // Fk`s
            builder.Property(x => x.ArtistID).IsRequired();
            builder
                .HasOne(x => x.Artist)
                .WithMany(x => x.Titles)
                .HasForeignKey(x => x.ArtistID)
                .IsRequired();

            builder.Property(x => x.GenreId).IsRequired();
            builder
                .HasOne(x => x.Genre)
                .WithMany(x => x.Titles)
                .HasForeignKey(x => x.GenreId)
                .IsRequired();

            builder.Property(x => x.AlbumId).IsRequired();
            builder
                .HasOne(x => x.Album)
                .WithMany(x => x.Titles)
                .HasForeignKey(x => x.AlbumId)
                .IsRequired();


        }
    }
}
