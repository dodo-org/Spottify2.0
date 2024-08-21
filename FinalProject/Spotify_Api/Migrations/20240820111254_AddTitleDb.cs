using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Spotify_Api.Migrations
{
    /// <inheritdoc />
    public partial class AddTitleDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlaylistTitle_TitleEntity_TitleId",
                table: "PlaylistTitle");

            migrationBuilder.DropForeignKey(
                name: "FK_TitleEntity_Artist_ArtistID",
                table: "TitleEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TitleEntity",
                table: "TitleEntity");

            migrationBuilder.RenameTable(
                name: "TitleEntity",
                newName: "Title");

            migrationBuilder.RenameIndex(
                name: "IX_TitleEntity_ArtistID",
                table: "Title",
                newName: "IX_Title_ArtistID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Title",
                table: "Title",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlaylistTitle_Title_TitleId",
                table: "PlaylistTitle",
                column: "TitleId",
                principalTable: "Title",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Title_Artist_ArtistID",
                table: "Title",
                column: "ArtistID",
                principalTable: "Artist",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlaylistTitle_Title_TitleId",
                table: "PlaylistTitle");

            migrationBuilder.DropForeignKey(
                name: "FK_Title_Artist_ArtistID",
                table: "Title");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Title",
                table: "Title");

            migrationBuilder.RenameTable(
                name: "Title",
                newName: "TitleEntity");

            migrationBuilder.RenameIndex(
                name: "IX_Title_ArtistID",
                table: "TitleEntity",
                newName: "IX_TitleEntity_ArtistID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TitleEntity",
                table: "TitleEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlaylistTitle_TitleEntity_TitleId",
                table: "PlaylistTitle",
                column: "TitleId",
                principalTable: "TitleEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TitleEntity_Artist_ArtistID",
                table: "TitleEntity",
                column: "ArtistID",
                principalTable: "Artist",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
