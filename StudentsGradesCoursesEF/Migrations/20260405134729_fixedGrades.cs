using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentsGradesCoursesEF.Migrations
{
    /// <inheritdoc />
    public partial class fixedGrades : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Course_Students_StudentId",
                table: "Course");

            migrationBuilder.DropTable(
                name: "CourseGrade");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Course",
                table: "Course");

            migrationBuilder.RenameTable(
                name: "Course",
                newName: "Courses");

            migrationBuilder.RenameIndex(
                name: "IX_Course_StudentId",
                table: "Courses",
                newName: "IX_Courses_StudentId");

            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "Grades",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Courses",
                table: "Courses",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_CourseId",
                table: "Grades",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Students_StudentId",
                table: "Courses",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Courses_CourseId",
                table: "Grades",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Students_StudentId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Courses_CourseId",
                table: "Grades");

            migrationBuilder.DropIndex(
                name: "IX_Grades_CourseId",
                table: "Grades");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Courses",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "Grades");

            migrationBuilder.RenameTable(
                name: "Courses",
                newName: "Course");

            migrationBuilder.RenameIndex(
                name: "IX_Courses_StudentId",
                table: "Course",
                newName: "IX_Course_StudentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Course",
                table: "Course",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "CourseGrade",
                columns: table => new
                {
                    CoursesId = table.Column<int>(type: "int", nullable: false),
                    GradesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseGrade", x => new { x.CoursesId, x.GradesId });
                    table.ForeignKey(
                        name: "FK_CourseGrade_Course_CoursesId",
                        column: x => x.CoursesId,
                        principalTable: "Course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseGrade_Grades_GradesId",
                        column: x => x.GradesId,
                        principalTable: "Grades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseGrade_GradesId",
                table: "CourseGrade",
                column: "GradesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Course_Students_StudentId",
                table: "Course",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");
        }
    }
}
