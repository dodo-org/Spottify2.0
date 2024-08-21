﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Spotify_Api.DB_Connection;

#nullable disable

namespace Spotify_Api.Migrations
{
    [DbContext(typeof(BaseContext))]
    [Migration("20240820110103_removeArtistAndGenere")]
    partial class removeArtistAndGenere
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Spotify_Api.DB_Connection.Entitys.ArtistEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("StageName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Artist", (string)null);
                });

            modelBuilder.Entity("Spotify_Api.DB_Connection.Entitys.PlaylistEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UserID")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserID");

                    b.ToTable("Playlist", (string)null);
                });

            modelBuilder.Entity("Spotify_Api.DB_Connection.Entitys.PlaylistTitleEntity", b =>
                {
                    b.Property<int>("PlaylistId")
                        .HasColumnType("integer");

                    b.Property<int>("TitleId")
                        .HasColumnType("integer");

                    b.HasKey("PlaylistId", "TitleId");

                    b.HasIndex("TitleId");

                    b.ToTable("PlaylistTitle", (string)null);
                });

            modelBuilder.Entity("Spotify_Api.DB_Connection.Entitys.TitleEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ArtistID")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ArtistID");

                    b.ToTable("TitleEntity");
                });

            modelBuilder.Entity("Spotify_Api.DB_Connection.Entitys.UserEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Token")
                        .HasColumnType("text");

                    b.Property<DateTime?>("TokenCreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("Spotify_Api.DB_Connection.Entitys.PlaylistEntity", b =>
                {
                    b.HasOne("Spotify_Api.DB_Connection.Entitys.UserEntity", "User")
                        .WithMany("Playlists")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Spotify_Api.DB_Connection.Entitys.PlaylistTitleEntity", b =>
                {
                    b.HasOne("Spotify_Api.DB_Connection.Entitys.PlaylistEntity", "Playlist")
                        .WithMany("TitlePlaylists")
                        .HasForeignKey("PlaylistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Spotify_Api.DB_Connection.Entitys.TitleEntity", "Title")
                        .WithMany("TitlePlaylists")
                        .HasForeignKey("TitleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Playlist");

                    b.Navigation("Title");
                });

            modelBuilder.Entity("Spotify_Api.DB_Connection.Entitys.TitleEntity", b =>
                {
                    b.HasOne("Spotify_Api.DB_Connection.Entitys.ArtistEntity", "Artist")
                        .WithMany("Titles")
                        .HasForeignKey("ArtistID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Artist");
                });

            modelBuilder.Entity("Spotify_Api.DB_Connection.Entitys.ArtistEntity", b =>
                {
                    b.Navigation("Titles");
                });

            modelBuilder.Entity("Spotify_Api.DB_Connection.Entitys.PlaylistEntity", b =>
                {
                    b.Navigation("TitlePlaylists");
                });

            modelBuilder.Entity("Spotify_Api.DB_Connection.Entitys.TitleEntity", b =>
                {
                    b.Navigation("TitlePlaylists");
                });

            modelBuilder.Entity("Spotify_Api.DB_Connection.Entitys.UserEntity", b =>
                {
                    b.Navigation("Playlists");
                });
#pragma warning restore 612, 618
        }
    }
}
