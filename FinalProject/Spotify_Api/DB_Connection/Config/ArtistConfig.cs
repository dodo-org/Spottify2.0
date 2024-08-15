using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Spotify_Api.DB_Connection.Entitys;

namespace Spotify_Api.DB_Connection.Config
{
    public class ArtistConfig : IEntityTypeConfiguration<ArtistEntity>
    {
        public void Configure(EntityTypeBuilder<ArtistEntity> builder)
        {
            builder.ToTable("Title");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.FName).IsRequired();
            builder.Property(x => x.LName).IsRequired(); 
            builder.Property(x => x.StageName).IsRequired();
        }
    }
}
