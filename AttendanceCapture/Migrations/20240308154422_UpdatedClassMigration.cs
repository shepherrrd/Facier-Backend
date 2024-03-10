using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AttendanceCapture.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedClassMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClassNumber",
                table: "Classes",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "TimeCreated", "TimeUpdated" },
                values: new object[] { "c350f3a2-ba90-4ed1-9658-dfad1bdc2e60", "AQAAAAIAAYagAAAAEO2353PW4qYN8W2kyfHaT/xj8lSGcxG0p9Bok3pg5BhiTLEFXZL8bU1UdCDTyyUbDg==", "8A41A8980F864A439EAB8F546FF13480", new DateTimeOffset(new DateTime(2024, 3, 8, 15, 44, 21, 662, DateTimeKind.Unspecified).AddTicks(3539), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 3, 8, 15, 44, 21, 662, DateTimeKind.Unspecified).AddTicks(3544), new TimeSpan(0, 0, 0, 0, 0)) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClassNumber",
                table: "Classes");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "TimeCreated", "TimeUpdated" },
                values: new object[] { "7b484569-95d9-41b2-bac1-ec3299163d4d", "AQAAAAIAAYagAAAAEAZWv2pQ5rSrOJrk/GQS3fKW5+CSjH9dBJXZlVQBjhIojmR24tfub+ZVFvvEuYo/QA==", "AB8F7AAF96E37A4BA376D6270ED8D0E5", new DateTimeOffset(new DateTime(2024, 3, 6, 22, 39, 56, 746, DateTimeKind.Unspecified).AddTicks(2865), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 3, 6, 22, 39, 56, 746, DateTimeKind.Unspecified).AddTicks(2870), new TimeSpan(0, 0, 0, 0, 0)) });
        }
    }
}
