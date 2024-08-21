﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Spotify_Api.DB_Connection.Entitys;

namespace Spotify_Api.DB_Connection.Config
{
    public class ArtistConfig : IEntityTypeConfiguration<ArtistEntity>
    {
        public void Configure(EntityTypeBuilder<ArtistEntity> builder)
        {
            builder.ToTable("Artist");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.StageName).IsRequired();

            builder.HasMany(a => a.Titles)
                           .WithOne(t => t.Artist)
                           .HasForeignKey(t => t.ArtistID);
        }
    }
}
