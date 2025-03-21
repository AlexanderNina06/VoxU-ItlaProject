using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VoxU_Backend.Core.Persistence.Migrations
{
    public partial class BookLib : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LinkPdf",
                table: "Library",
                newName: "UserId");

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "Library",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Library",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UploadDate",
                table: "Library",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "Library");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Library");

            migrationBuilder.DropColumn(
                name: "UploadDate",
                table: "Library");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Library",
                newName: "LinkPdf");
        }
    }
}
