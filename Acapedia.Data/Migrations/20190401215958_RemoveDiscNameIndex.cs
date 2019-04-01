using Microsoft.EntityFrameworkCore.Migrations;

namespace Acapedia.Data.Migrations
{
    public partial class RemoveDiscNameIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Discipline_DisciplineName",
                table: "Discipline");

            migrationBuilder.AlterColumn<string>(
                name: "DisciplineName",
                table: "Discipline",
                nullable: false,
                oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DisciplineName",
                table: "Discipline",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_Discipline_DisciplineName",
                table: "Discipline",
                column: "DisciplineName");
        }
    }
}
