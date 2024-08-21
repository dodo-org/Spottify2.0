using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Spotify_Api.DB_Connection.Entitys;

namespace Spotify_Api.DB_Connection.Config
{
    //public class GenreConfig : IEntityTypeConfiguration<GenreEntity>
    //{
    //    public void Configure(EntityTypeBuilder<GenreEntity> builder)
    //    {
    //        builder.ToTable("Genre");

    //        builder.HasKey(x => x.Id);

    //        builder.Property(x => x.Name).IsRequired();

    //        builder.HasMany(g => g.Titles)
    //           .WithOne(t => t.Genre)
    //           .HasForeignKey(t => t.GenreId);
    //    }
    //}
}
