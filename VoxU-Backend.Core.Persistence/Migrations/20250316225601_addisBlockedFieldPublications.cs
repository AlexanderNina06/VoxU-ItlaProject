using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VoxU_Backend.Core.Persistence.Migrations
{
    public partial class addisBlockedFieldPublications : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isActive",
                table: "Publications");

            migrationBuilder.AddColumn<bool>(
                name: "isBlocked",
                table: "Publications",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isBlocked",
                table: "Publications");

            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "Publications",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
