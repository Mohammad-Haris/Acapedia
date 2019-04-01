using Microsoft.EntityFrameworkCore.Migrations;

namespace Acapedia.Data.Migrations
{
    public partial class AddIndexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LinkDisciplineId",
                table: "WebsiteLink",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LinkCountryName",
                table: "WebsiteLink",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "DisciplineName",
                table: "Discipline",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_WebsiteLink_LinkCountryName_LinkDisciplineId",
                table: "WebsiteLink",
                columns: new[] { "LinkCountryName", "LinkDisciplineId" });

            migrationBuilder.CreateIndex(
                name: "IX_Discipline_DisciplineName",
                table: "Discipline",
                column: "DisciplineName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WebsiteLink_LinkCountryName_LinkDisciplineId",
                table: "WebsiteLink");

            migrationBuilder.DropIndex(
                name: "IX_Discipline_DisciplineName",
                table: "Discipline");

            migrationBuilder.AlterColumn<string>(
                name: "LinkDisciplineId",
                table: "WebsiteLink",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LinkCountryName",
                table: "WebsiteLink",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "DisciplineName",
                table: "Discipline",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
