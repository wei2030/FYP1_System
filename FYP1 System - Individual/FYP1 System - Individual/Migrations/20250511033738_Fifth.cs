using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FYP1_System___Individual.Migrations
{
    /// <inheritdoc />
    public partial class Fifth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProgramId",
                table: "Lecturers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Lecturers_ProgramId",
                table: "Lecturers",
                column: "ProgramId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lecturers_AcademicPrograms_ProgramId",
                table: "Lecturers",
                column: "ProgramId",
                principalTable: "AcademicPrograms",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lecturers_AcademicPrograms_ProgramId",
                table: "Lecturers");

            migrationBuilder.DropIndex(
                name: "IX_Lecturers_ProgramId",
                table: "Lecturers");

            migrationBuilder.DropColumn(
                name: "ProgramId",
                table: "Lecturers");
        }
    }
}
