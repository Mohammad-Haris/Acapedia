using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Acapedia.Data.Migrations
{
    public partial class WebsiteLinks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contribution");

            migrationBuilder.DropTable(
                name: "UniversityDiscipline");

            migrationBuilder.DropTable(
                name: "Discipline");

            migrationBuilder.DropTable(
                name: "University");

            migrationBuilder.CreateTable(
                name: "WebsiteLink",
                columns: table => new
                {
                    LinkId = table.Column<string>(nullable: false),
                    LinkUrl = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    LinkCountryName = table.Column<string>(nullable: true),
                    LinkDisciplineId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebsiteLink", x => x.LinkId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WebsiteLink");

            migrationBuilder.CreateTable(
                name: "Contribution",
                columns: table => new
                {
                    ContributionId = table.Column<string>(nullable: false),
                    AuthorId = table.Column<string>(nullable: true),
                    ContributionContent = table.Column<string>(nullable: true),
                    ContributionSubject = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contribution", x => x.ContributionId);
                    table.ForeignKey(
                        name: "FK_Contribution_AspNetUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Discipline",
                columns: table => new
                {
                    DisciplineId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DisciplineName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discipline", x => x.DisciplineId);
                });

            migrationBuilder.CreateTable(
                name: "University",
                columns: table => new
                {
                    UniversityId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CountryName = table.Column<string>(nullable: true),
                    UniversityName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_University", x => x.UniversityId);
                    table.ForeignKey(
                        name: "FK_University_Country_CountryName",
                        column: x => x.CountryName,
                        principalTable: "Country",
                        principalColumn: "CountryName",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UniversityDiscipline",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DisciplineId = table.Column<int>(nullable: false),
                    UniversityId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UniversityDiscipline", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UniversityDiscipline_Discipline_DisciplineId",
                        column: x => x.DisciplineId,
                        principalTable: "Discipline",
                        principalColumn: "DisciplineId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UniversityDiscipline_University_UniversityId",
                        column: x => x.UniversityId,
                        principalTable: "University",
                        principalColumn: "UniversityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contribution_AuthorId",
                table: "Contribution",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_University_CountryName",
                table: "University",
                column: "CountryName");

            migrationBuilder.CreateIndex(
                name: "IX_UniversityDiscipline_DisciplineId",
                table: "UniversityDiscipline",
                column: "DisciplineId");

            migrationBuilder.CreateIndex(
                name: "IX_UniversityDiscipline_UniversityId",
                table: "UniversityDiscipline",
                column: "UniversityId");
        }
    }
}
