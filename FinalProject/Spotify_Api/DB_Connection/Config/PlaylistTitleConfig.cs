using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Spotify_Api.DB_Connection.Entitys;

namespace Spotify_Api.DB_Connection.Config
{
    public class PlaylistTitleConfig : IEntityTypeConfiguration<PlaylistTitleEntity>
    {
        public void Configure(EntityTypeBuilder<PlaylistTitleEntity> builder)
        {
            throw new NotImplementedException();
        }
    }
}
