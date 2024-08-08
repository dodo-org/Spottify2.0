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

            builder.Property(x  => x.Title).IsRequired();
            builder.Property(x =>x.Description).IsRequired();
            builder.Property(x => x.ArtistID).IsRequired();
            builder.Property(x => x.GenreId).IsRequired();

            
        }
    }
}
