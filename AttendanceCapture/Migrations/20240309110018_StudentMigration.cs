using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AttendanceCapture.Migrations
{
    /// <inheritdoc />
    public partial class StudentMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "Students",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "TimeCreated", "TimeUpdated" },
                values: new object[] { "ca86932f-c4e6-4e85-b1e1-44dbeaffb802", "AQAAAAIAAYagAAAAED2VZjhoAMhwkMTcwg6VWTZrr0Ww+n4zGVyJSYFUHN2KIZUs0SQUvF97tjFynfBC+g==", "C2F479277AB70849909F856128B5C98A", new DateTimeOffset(new DateTime(2024, 3, 9, 11, 0, 17, 592, DateTimeKind.Unspecified).AddTicks(1914), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 3, 9, 11, 0, 17, 592, DateTimeKind.Unspecified).AddTicks(1918), new TimeSpan(0, 0, 0, 0, 0)) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photo",
                table: "Students");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "TimeCreated", "TimeUpdated" },
                values: new object[] { "c350f3a2-ba90-4ed1-9658-dfad1bdc2e60", "AQAAAAIAAYagAAAAEO2353PW4qYN8W2kyfHaT/xj8lSGcxG0p9Bok3pg5BhiTLEFXZL8bU1UdCDTyyUbDg==", "8A41A8980F864A439EAB8F546FF13480", new DateTimeOffset(new DateTime(2024, 3, 8, 15, 44, 21, 662, DateTimeKind.Unspecified).AddTicks(3539), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 3, 8, 15, 44, 21, 662, DateTimeKind.Unspecified).AddTicks(3544), new TimeSpan(0, 0, 0, 0, 0)) });
        }
    }
}
