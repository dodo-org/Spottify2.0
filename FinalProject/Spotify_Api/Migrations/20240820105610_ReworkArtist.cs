using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Spotify_Api.Migrations
{
    /// <inheritdoc />
    public partial class ReworkArtist : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FName",
                table: "Artist");

            migrationBuilder.DropColumn(
                name: "LName",
                table: "Artist");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FName",
                table: "Artist",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LName",
                table: "Artist",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
