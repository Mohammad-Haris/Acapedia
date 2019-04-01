using Microsoft.EntityFrameworkCore.Migrations;

namespace Acapedia.Data.Migrations
{
    public partial class AddDisciplineNameIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LinkUrl",
                table: "WebsiteLink",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LinkDisciplineId",
                table: "WebsiteLink",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LinkCountryName",
                table: "WebsiteLink",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DisciplineName",
                table: "Discipline",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Discipline_DisciplineName",
                table: "Discipline",
                column: "DisciplineName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Discipline_DisciplineName",
                table: "Discipline");

            migrationBuilder.AlterColumn<string>(
                name: "LinkUrl",
                table: "WebsiteLink",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LinkDisciplineId",
                table: "WebsiteLink",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LinkCountryName",
                table: "WebsiteLink",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "DisciplineName",
                table: "Discipline",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
