using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class AddedWatchlistTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Favourites_AspNetUsers_UserId",
                table: "Favourites");

            migrationBuilder.DropForeignKey(
                name: "FK_Favourites_AspNetUsers_UserId1",
                table: "Favourites");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Favourites",
                table: "Favourites");

            migrationBuilder.RenameTable(
                name: "Favourites",
                newName: "Media");

            migrationBuilder.RenameIndex(
                name: "IX_Favourites_UserId1",
                table: "Media",
                newName: "IX_Media_UserId1");

            migrationBuilder.RenameIndex(
                name: "IX_Favourites_UserId",
                table: "Media",
                newName: "IX_Media_UserId");

            migrationBuilder.AddColumn<string>(
                name: "UserId2",
                table: "Media",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Media",
                table: "Media",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Media_UserId2",
                table: "Media",
                column: "UserId2");

            migrationBuilder.AddForeignKey(
                name: "FK_Media_AspNetUsers_UserId",
                table: "Media",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Media_AspNetUsers_UserId1",
                table: "Media",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Media_AspNetUsers_UserId2",
                table: "Media",
                column: "UserId2",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Media_AspNetUsers_UserId",
                table: "Media");

            migrationBuilder.DropForeignKey(
                name: "FK_Media_AspNetUsers_UserId1",
                table: "Media");

            migrationBuilder.DropForeignKey(
                name: "FK_Media_AspNetUsers_UserId2",
                table: "Media");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Media",
                table: "Media");

            migrationBuilder.DropIndex(
                name: "IX_Media_UserId2",
                table: "Media");

            migrationBuilder.DropColumn(
                name: "UserId2",
                table: "Media");

            migrationBuilder.RenameTable(
                name: "Media",
                newName: "Favourites");

            migrationBuilder.RenameIndex(
                name: "IX_Media_UserId1",
                table: "Favourites",
                newName: "IX_Favourites_UserId1");

            migrationBuilder.RenameIndex(
                name: "IX_Media_UserId",
                table: "Favourites",
                newName: "IX_Favourites_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Favourites",
                table: "Favourites",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Favourites_AspNetUsers_UserId",
                table: "Favourites",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Favourites_AspNetUsers_UserId1",
                table: "Favourites",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
