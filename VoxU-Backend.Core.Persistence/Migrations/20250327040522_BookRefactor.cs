using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VoxU_Backend.Core.Persistence.Migrations
{
    public partial class BookRefactor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "Library");

            migrationBuilder.DropColumn(
                name: "UploadDate",
                table: "Library");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Library",
                newName: "Url");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Library",
                newName: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Url",
                table: "Library",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Library",
                newName: "Title");

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "Library",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UploadDate",
                table: "Library",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
