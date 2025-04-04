using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VoxU_Backend.Core.Persistence.Migrations
{
    public partial class bookUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BookCover",
                table: "Library",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookCover",
                table: "Library");
        }
    }
}
