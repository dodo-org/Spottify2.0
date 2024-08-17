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
            builder.HasOne(t => t.Artist)
               .WithMany(a => a.Titles)
               .HasForeignKey(t => t.ArtistID);

            builder.HasOne(t => t.Genre)
                   .WithMany(g => g.Titles)
                   .HasForeignKey(t => t.GenreId);

            builder.HasOne(t => t.Album)
                   .WithMany(a => a.Titles)
                   .HasForeignKey(t => t.AlbumId);


        }
    }
}
