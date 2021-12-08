using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class AddWatchlist : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Media",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Media_UserId1",
                table: "Media",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Media_AspNetUsers_UserId1",
                table: "Media",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Media_AspNetUsers_UserId1",
                table: "Media");

            migrationBuilder.DropIndex(
                name: "IX_Media_UserId1",
                table: "Media");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Media");
        }
    }
}
