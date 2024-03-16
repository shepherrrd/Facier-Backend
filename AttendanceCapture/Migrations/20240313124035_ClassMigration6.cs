using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AttendanceCapture.Migrations
{
    /// <inheritdoc />
    public partial class ClassMigration6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Faculties");

            migrationBuilder.DropColumn(
                name: "FacultyID",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "FacultyId",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "LecturerID",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Classes");

            migrationBuilder.RenameColumn(
                name: "LectureriD",
                table: "Courses",
                newName: "LecturerID");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Courses",
                newName: "Title");

            migrationBuilder.AlterColumn<long>(
                name: "ClassId",
                table: "Students",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ClassId",
                table: "Courses",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CourseCode",
                table: "Courses",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Level",
                table: "Courses",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DepartmentID",
                table: "Classes",
                type: "bigint",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "TimeCreated", "TimeUpdated" },
                values: new object[] { "58d24727-4983-4dc0-9f0d-951b7e053bdc", "AQAAAAIAAYagAAAAEH6+GuMwEYy3+M5Yqpd2hFilD0Mnv6FTZqs1a7BruoJtI0A5tJbDEGG7qCg/RhuhTg==", "DB60B5104AB0984192285030F9D71484", new DateTimeOffset(new DateTime(2024, 3, 13, 12, 40, 34, 466, DateTimeKind.Unspecified).AddTicks(6555), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 3, 13, 12, 40, 34, 466, DateTimeKind.Unspecified).AddTicks(6561), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.InsertData(
                table: "Classes",
                columns: new[] { "Id", "ClassNumber", "DepartmentID", "StudentIds", "TimeCreated", "TimeUpdated" },
                values: new object[] { 3L, "SE400", 1L, null, new DateTimeOffset(new DateTime(2024, 3, 13, 12, 40, 34, 466, DateTimeKind.Unspecified).AddTicks(7182), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 3, 13, 12, 40, 34, 466, DateTimeKind.Unspecified).AddTicks(7182), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "Lecturers", "Name", "TimeCreated", "TimeUpdated" },
                values: new object[,]
                {
                    { 1L, ";1;", "Software Engineering", new DateTimeOffset(new DateTime(2024, 3, 13, 12, 40, 34, 466, DateTimeKind.Unspecified).AddTicks(7081), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 3, 13, 12, 40, 34, 466, DateTimeKind.Unspecified).AddTicks(7082), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 2L, ";1;", "Computer Science", new DateTimeOffset(new DateTime(2024, 3, 13, 12, 40, 34, 466, DateTimeKind.Unspecified).AddTicks(7113), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 3, 13, 12, 40, 34, 466, DateTimeKind.Unspecified).AddTicks(7114), new TimeSpan(0, 0, 0, 0, 0)) },
                    { 3L, ";1;", "Computer Technology", new DateTimeOffset(new DateTime(2024, 3, 13, 12, 40, 34, 466, DateTimeKind.Unspecified).AddTicks(7140), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 3, 13, 12, 40, 34, 466, DateTimeKind.Unspecified).AddTicks(7141), new TimeSpan(0, 0, 0, 0, 0)) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DropColumn(
                name: "ClassId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "CourseCode",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "DepartmentID",
                table: "Classes");

            migrationBuilder.RenameColumn(
                name: "LecturerID",
                table: "Courses",
                newName: "LectureriD");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Courses",
                newName: "Name");

            migrationBuilder.AlterColumn<int>(
                name: "ClassId",
                table: "Students",
                type: "integer",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "FacultyID",
                table: "Departments",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FacultyId",
                table: "Departments",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LecturerID",
                table: "Classes",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Classes",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Faculties",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Departments = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    TimeCreated = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    TimeUpdated = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faculties", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "TimeCreated", "TimeUpdated" },
                values: new object[] { "6eaebebe-4432-4615-93af-3d2c67958f8e", "AQAAAAIAAYagAAAAEB+h1loB2rnS5q6htME46g/yBEced4NFe1ILh0q17CqT8gug4sZuD9xsQnLT1KOoNg==", "E5612E4D361F914EABB4056F90624177", new DateTimeOffset(new DateTime(2024, 3, 10, 18, 55, 16, 332, DateTimeKind.Unspecified).AddTicks(9161), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 3, 10, 18, 55, 16, 332, DateTimeKind.Unspecified).AddTicks(9167), new TimeSpan(0, 0, 0, 0, 0)) });
        }
    }
}
