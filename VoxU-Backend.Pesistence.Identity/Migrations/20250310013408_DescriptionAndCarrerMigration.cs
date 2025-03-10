using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VoxU_Backend.Pesistence.Identity.Migrations
{
    public partial class DescriptionAndCarrerMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Career",
                schema: "Identity",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "Identity",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Career",
                schema: "Identity",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "Identity",
                table: "Users");
        }
    }
}
