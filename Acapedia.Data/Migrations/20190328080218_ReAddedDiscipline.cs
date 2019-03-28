using Microsoft.EntityFrameworkCore.Migrations;

namespace Acapedia.Data.Migrations
{
    public partial class ReAddedDiscipline : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Discipline",
                columns: table => new
                {
                    DisciplineId = table.Column<string>(nullable: false),
                    DisciplineName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discipline", x => x.DisciplineId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Discipline");
        }
    }
}
