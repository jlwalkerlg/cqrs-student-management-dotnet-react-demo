using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentManagement.Infrastructure.Data.EF.Migrations
{
    public partial class RelateEnrollmentToCourseByKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Enrollment_CourseId",
                table: "Enrollment",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollment_Courses_CourseId",
                table: "Enrollment",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollment_Courses_CourseId",
                table: "Enrollment");

            migrationBuilder.DropIndex(
                name: "IX_Enrollment_CourseId",
                table: "Enrollment");
        }
    }
}
