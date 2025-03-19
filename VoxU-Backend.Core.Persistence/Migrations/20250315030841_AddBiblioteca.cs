using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VoxU_Backend.Core.Persistence.Migrations
{
    public partial class AddBiblioteca : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isActive",
                table: "Publications");

            migrationBuilder.CreateTable(
                name: "Biblioteca",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LinkPdf = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Biblioteca", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Biblioteca");

            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "Publications",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
