using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class UnmapGenreIDs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Media",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<bool>(
                name: "Adult",
                table: "Media",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Language",
                table: "Media",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MediaType",
                table: "Media",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Overview",
                table: "Media",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Media",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Adult",
                table: "Media");

            migrationBuilder.DropColumn(
                name: "Language",
                table: "Media");

            migrationBuilder.DropColumn(
                name: "MediaType",
                table: "Media");

            migrationBuilder.DropColumn(
                name: "Overview",
                table: "Media");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Media");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Media",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);
        }
    }
}
