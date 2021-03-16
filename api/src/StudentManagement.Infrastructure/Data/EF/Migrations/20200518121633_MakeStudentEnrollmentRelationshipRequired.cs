using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentManagement.Infrastructure.Data.EF.Migrations
{
    public partial class MakeStudentEnrollmentRelationshipRequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollment_Students_StudentId",
                table: "Enrollment");

            migrationBuilder.AlterColumn<Guid>(
                name: "StudentId",
                table: "Enrollment",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollment_Students_StudentId",
                table: "Enrollment",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollment_Students_StudentId",
                table: "Enrollment");

            migrationBuilder.AlterColumn<Guid>(
                name: "StudentId",
                table: "Enrollment",
                type: "char(36)",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollment_Students_StudentId",
                table: "Enrollment",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
