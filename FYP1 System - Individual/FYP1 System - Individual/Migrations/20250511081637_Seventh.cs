using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FYP1_System___Individual.Migrations
{
    /// <inheritdoc />
    public partial class Seventh : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Proposals_Lecturers_EvaluatorId",
                table: "Proposals");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_AcademicPrograms_AcademicProgramId",
                table: "Student");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Student",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "DocumentPath",
                table: "Proposals");

            migrationBuilder.RenameTable(
                name: "Student",
                newName: "Students");

            migrationBuilder.RenameColumn(
                name: "EvaluatorId",
                table: "Proposals",
                newName: "LecturerId");

            migrationBuilder.RenameIndex(
                name: "IX_Proposals_EvaluatorId",
                table: "Proposals",
                newName: "IX_Proposals_LecturerId");

            migrationBuilder.RenameColumn(
                name: "AcademicProgramId",
                table: "Students",
                newName: "ProgramId");

            migrationBuilder.RenameIndex(
                name: "IX_Student_AcademicProgramId",
                table: "Students",
                newName: "IX_Students_ProgramId");

            migrationBuilder.AddColumn<int>(
                name: "Evaluator1Id",
                table: "Proposals",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Evaluator2Id",
                table: "Proposals",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "Proposals",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "Proposals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SupervisorId",
                table: "Proposals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Students",
                table: "Students",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Proposals_Evaluator1Id",
                table: "Proposals",
                column: "Evaluator1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Proposals_Evaluator2Id",
                table: "Proposals",
                column: "Evaluator2Id");

            migrationBuilder.CreateIndex(
                name: "IX_Proposals_StudentId",
                table: "Proposals",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Proposals_SupervisorId",
                table: "Proposals",
                column: "SupervisorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Proposals_Lecturers_Evaluator1Id",
                table: "Proposals",
                column: "Evaluator1Id",
                principalTable: "Lecturers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Proposals_Lecturers_Evaluator2Id",
                table: "Proposals",
                column: "Evaluator2Id",
                principalTable: "Lecturers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Proposals_Lecturers_LecturerId",
                table: "Proposals",
                column: "LecturerId",
                principalTable: "Lecturers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Proposals_Lecturers_SupervisorId",
                table: "Proposals",
                column: "SupervisorId",
                principalTable: "Lecturers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Proposals_Students_StudentId",
                table: "Proposals",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_AcademicPrograms_ProgramId",
                table: "Students",
                column: "ProgramId",
                principalTable: "AcademicPrograms",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Proposals_Lecturers_Evaluator1Id",
                table: "Proposals");

            migrationBuilder.DropForeignKey(
                name: "FK_Proposals_Lecturers_Evaluator2Id",
                table: "Proposals");

            migrationBuilder.DropForeignKey(
                name: "FK_Proposals_Lecturers_LecturerId",
                table: "Proposals");

            migrationBuilder.DropForeignKey(
                name: "FK_Proposals_Lecturers_SupervisorId",
                table: "Proposals");

            migrationBuilder.DropForeignKey(
                name: "FK_Proposals_Students_StudentId",
                table: "Proposals");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_AcademicPrograms_ProgramId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Proposals_Evaluator1Id",
                table: "Proposals");

            migrationBuilder.DropIndex(
                name: "IX_Proposals_Evaluator2Id",
                table: "Proposals");

            migrationBuilder.DropIndex(
                name: "IX_Proposals_StudentId",
                table: "Proposals");

            migrationBuilder.DropIndex(
                name: "IX_Proposals_SupervisorId",
                table: "Proposals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Students",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Evaluator1Id",
                table: "Proposals");

            migrationBuilder.DropColumn(
                name: "Evaluator2Id",
                table: "Proposals");

            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "Proposals");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Proposals");

            migrationBuilder.DropColumn(
                name: "SupervisorId",
                table: "Proposals");

            migrationBuilder.RenameTable(
                name: "Students",
                newName: "Student");

            migrationBuilder.RenameColumn(
                name: "LecturerId",
                table: "Proposals",
                newName: "EvaluatorId");

            migrationBuilder.RenameIndex(
                name: "IX_Proposals_LecturerId",
                table: "Proposals",
                newName: "IX_Proposals_EvaluatorId");

            migrationBuilder.RenameColumn(
                name: "ProgramId",
                table: "Student",
                newName: "AcademicProgramId");

            migrationBuilder.RenameIndex(
                name: "IX_Students_ProgramId",
                table: "Student",
                newName: "IX_Student_AcademicProgramId");

            migrationBuilder.AddColumn<string>(
                name: "DocumentPath",
                table: "Proposals",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Student",
                table: "Student",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Proposals_Lecturers_EvaluatorId",
                table: "Proposals",
                column: "EvaluatorId",
                principalTable: "Lecturers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Student_AcademicPrograms_AcademicProgramId",
                table: "Student",
                column: "AcademicProgramId",
                principalTable: "AcademicPrograms",
                principalColumn: "Id");
        }
    }
}
