using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Spotify_Api.Migrations
{
    /// <inheritdoc />
    public partial class removeArtistAndGenere : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TitleEntity_Album_AlbumId",
                table: "TitleEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_TitleEntity_Genre_GenreId",
                table: "TitleEntity");

            migrationBuilder.DropTable(
                name: "Album");

            migrationBuilder.DropTable(
                name: "Genre");

            migrationBuilder.DropIndex(
                name: "IX_TitleEntity_AlbumId",
                table: "TitleEntity");

            migrationBuilder.DropIndex(
                name: "IX_TitleEntity_GenreId",
                table: "TitleEntity");

            migrationBuilder.DropColumn(
                name: "AlbumId",
                table: "TitleEntity");

            migrationBuilder.DropColumn(
                name: "GenreId",
                table: "TitleEntity");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AlbumId",
                table: "TitleEntity",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GenreId",
                table: "TitleEntity",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Album",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Album", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genre",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genre", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TitleEntity_AlbumId",
                table: "TitleEntity",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_TitleEntity_GenreId",
                table: "TitleEntity",
                column: "GenreId");

            migrationBuilder.AddForeignKey(
                name: "FK_TitleEntity_Album_AlbumId",
                table: "TitleEntity",
                column: "AlbumId",
                principalTable: "Album",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TitleEntity_Genre_GenreId",
                table: "TitleEntity",
                column: "GenreId",
                principalTable: "Genre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
