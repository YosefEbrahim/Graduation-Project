using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Models.Migrations
{
    public partial class addsubjectanddepartment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DepartmentId",
                table: "Exams",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SubjectId",
                table: "Exams",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Exams_DepartmentId",
                table: "Exams",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Exams_SubjectId",
                table: "Exams",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_Departments_DepartmentId",
                table: "Exams",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_Subjects_SubjectId",
                table: "Exams",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exams_Departments_DepartmentId",
                table: "Exams");

            migrationBuilder.DropForeignKey(
                name: "FK_Exams_Subjects_SubjectId",
                table: "Exams");

            migrationBuilder.DropIndex(
                name: "IX_Exams_DepartmentId",
                table: "Exams");

            migrationBuilder.DropIndex(
                name: "IX_Exams_SubjectId",
                table: "Exams");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Exams");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "Exams");
        }
    }
}
