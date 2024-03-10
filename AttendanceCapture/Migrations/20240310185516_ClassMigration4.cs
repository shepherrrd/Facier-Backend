using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AttendanceCapture.Migrations
{
    /// <inheritdoc />
    public partial class ClassMigration4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "LecturerID",
                table: "Attendances",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "TimeCreated", "TimeUpdated" },
                values: new object[] { "6eaebebe-4432-4615-93af-3d2c67958f8e", "AQAAAAIAAYagAAAAEB+h1loB2rnS5q6htME46g/yBEced4NFe1ILh0q17CqT8gug4sZuD9xsQnLT1KOoNg==", "E5612E4D361F914EABB4056F90624177", new DateTimeOffset(new DateTime(2024, 3, 10, 18, 55, 16, 332, DateTimeKind.Unspecified).AddTicks(9161), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 3, 10, 18, 55, 16, 332, DateTimeKind.Unspecified).AddTicks(9167), new TimeSpan(0, 0, 0, 0, 0)) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LecturerID",
                table: "Attendances");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "TimeCreated", "TimeUpdated" },
                values: new object[] { "15add6ec-5c36-4f3a-96d0-465b7ef8bae2", "AQAAAAIAAYagAAAAEFeziu8/XrLAVS5eKe9QwGvcH9w1anmXqtyUEowm6D5nYhQZDFLPI6XNJbEwNbESSA==", "727FC69D51B26E4789AD4DD17C70F3A1", new DateTimeOffset(new DateTime(2024, 3, 10, 18, 51, 40, 942, DateTimeKind.Unspecified).AddTicks(3933), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 3, 10, 18, 51, 40, 942, DateTimeKind.Unspecified).AddTicks(3937), new TimeSpan(0, 0, 0, 0, 0)) });
        }
    }
}
