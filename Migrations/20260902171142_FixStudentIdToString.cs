using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentCourseRegistrationSystem.Migrations
{
    /// <inheritdoc />
    public partial class FixStudentIdToString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseRequests_Students_StudentId",
                table: "CourseRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentCourses_Students_StudentId",
                table: "StudentCourses");

            migrationBuilder.DropIndex(
                name: "IX_StudentCourses_StudentId",
                table: "StudentCourses");

            migrationBuilder.DropIndex(
                name: "IX_CourseRequests_StudentId",
                table: "CourseRequests");

            migrationBuilder.AlterColumn<string>(
                name: "StudentId",
                table: "StudentCourses",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "StudentId1",
                table: "StudentCourses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "StudentId",
                table: "CourseRequests",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "StudentId1",
                table: "CourseRequests",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentCourses_StudentId1",
                table: "StudentCourses",
                column: "StudentId1");

            migrationBuilder.CreateIndex(
                name: "IX_CourseRequests_StudentId1",
                table: "CourseRequests",
                column: "StudentId1");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseRequests_Students_StudentId1",
                table: "CourseRequests",
                column: "StudentId1",
                principalTable: "Students",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentCourses_Students_StudentId1",
                table: "StudentCourses",
                column: "StudentId1",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseRequests_Students_StudentId1",
                table: "CourseRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentCourses_Students_StudentId1",
                table: "StudentCourses");

            migrationBuilder.DropIndex(
                name: "IX_StudentCourses_StudentId1",
                table: "StudentCourses");

            migrationBuilder.DropIndex(
                name: "IX_CourseRequests_StudentId1",
                table: "CourseRequests");

            migrationBuilder.DropColumn(
                name: "StudentId1",
                table: "StudentCourses");

            migrationBuilder.DropColumn(
                name: "StudentId1",
                table: "CourseRequests");

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "StudentCourses",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "CourseRequests",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_StudentCourses_StudentId",
                table: "StudentCourses",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseRequests_StudentId",
                table: "CourseRequests",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseRequests_Students_StudentId",
                table: "CourseRequests",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentCourses_Students_StudentId",
                table: "StudentCourses",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
