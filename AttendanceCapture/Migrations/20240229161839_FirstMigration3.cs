using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AttendanceCapture.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_Students_StudentId",
                table: "Attendances");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Lecturers_LectureriD",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Faculties_FacultyID",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_Lecturers_Departments_DepartmentId1",
                table: "Lecturers");

            migrationBuilder.DropIndex(
                name: "IX_Lecturers_DepartmentId1",
                table: "Lecturers");

            migrationBuilder.DropIndex(
                name: "IX_Departments_FacultyID",
                table: "Departments");

            migrationBuilder.DropIndex(
                name: "IX_Courses_LectureriD",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Attendances_StudentId",
                table: "Attendances");

            migrationBuilder.DropColumn(
                name: "DepartmentId1",
                table: "Lecturers");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Attendances");

            migrationBuilder.AddColumn<string>(
                name: "Attendances",
                table: "Students",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MatricNumber",
                table: "Students",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Courses",
                table: "Lecturers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Departments",
                table: "Faculties",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "TimeCreated", "TimeUpdated" },
                values: new object[] { "ed41fd91-7ab9-464b-8b87-9abef60350ae", "AQAAAAIAAYagAAAAEKcoACnB/F+kJwoUvepqqGIxbbCmQRQ1+K77apwm88bVd0o2M4vudya+GCAM8/fLbA==", "6F178362F6DB894398062596D39FA660", new DateTimeOffset(new DateTime(2024, 2, 29, 16, 18, 39, 55, DateTimeKind.Unspecified).AddTicks(9707), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 2, 29, 16, 18, 39, 55, DateTimeKind.Unspecified).AddTicks(9713), new TimeSpan(0, 0, 0, 0, 0)) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Attendances",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "MatricNumber",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Courses",
                table: "Lecturers");

            migrationBuilder.DropColumn(
                name: "Departments",
                table: "Faculties");

            migrationBuilder.AddColumn<long>(
                name: "DepartmentId1",
                table: "Lecturers",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "StudentId",
                table: "Attendances",
                type: "bigint",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "TimeCreated", "TimeUpdated" },
                values: new object[] { "e7e71356-30d8-4435-871a-13adbfa03912", "AQAAAAIAAYagAAAAEAkDdj5VlRnLIbfLYrv4c1uAndciT5rDgUewzcLrDNkAEH4FzHZ1HxBwaDAjZDNAbg==", "80641C6694B59E4BB79FB32890D07A16", new DateTimeOffset(new DateTime(2024, 2, 29, 15, 55, 18, 870, DateTimeKind.Unspecified).AddTicks(4000), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 2, 29, 15, 55, 18, 870, DateTimeKind.Unspecified).AddTicks(4011), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.CreateIndex(
                name: "IX_Lecturers_DepartmentId1",
                table: "Lecturers",
                column: "DepartmentId1");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_FacultyID",
                table: "Departments",
                column: "FacultyID");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_LectureriD",
                table: "Courses",
                column: "LectureriD");

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_StudentId",
                table: "Attendances",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_Students_StudentId",
                table: "Attendances",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Lecturers_LectureriD",
                table: "Courses",
                column: "LectureriD",
                principalTable: "Lecturers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Faculties_FacultyID",
                table: "Departments",
                column: "FacultyID",
                principalTable: "Faculties",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Lecturers_Departments_DepartmentId1",
                table: "Lecturers",
                column: "DepartmentId1",
                principalTable: "Departments",
                principalColumn: "Id");
        }
    }
}
