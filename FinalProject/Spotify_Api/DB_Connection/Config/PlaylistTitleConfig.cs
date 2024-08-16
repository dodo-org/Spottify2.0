using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Spotify_Api.DB_Connection.Entitys;

namespace Spotify_Api.DB_Connection.Config
{
    public class PlaylistTitleConfig : IEntityTypeConfiguration<PlaylistTitleEntity>
    {
        public void Configure(EntityTypeBuilder<PlaylistTitleEntity> builder)
        {
            builder.ToTable("PlaylistTitle");

            builder.HasKey(x => new { x.PlaylistId, x.TitleId });

            builder.HasOne(pt => pt.Playlist)
               .WithMany(p => p.TitlePlaylists)
               .HasForeignKey(pt => pt.PlaylistId);

            builder.HasOne(pt => pt.Title)
                   .WithMany(t => t.TitlePlaylists)
                   .HasForeignKey(pt => pt.TitleId);

        }
    }
}
