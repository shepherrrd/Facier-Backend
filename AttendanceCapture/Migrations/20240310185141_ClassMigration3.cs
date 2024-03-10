using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AttendanceCapture.Migrations
{
    /// <inheritdoc />
    public partial class ClassMigration3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "LecturerID",
                table: "Classes",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "TimeCreated", "TimeUpdated" },
                values: new object[] { "15add6ec-5c36-4f3a-96d0-465b7ef8bae2", "AQAAAAIAAYagAAAAEFeziu8/XrLAVS5eKe9QwGvcH9w1anmXqtyUEowm6D5nYhQZDFLPI6XNJbEwNbESSA==", "727FC69D51B26E4789AD4DD17C70F3A1", new DateTimeOffset(new DateTime(2024, 3, 10, 18, 51, 40, 942, DateTimeKind.Unspecified).AddTicks(3933), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 3, 10, 18, 51, 40, 942, DateTimeKind.Unspecified).AddTicks(3937), new TimeSpan(0, 0, 0, 0, 0)) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LecturerID",
                table: "Classes");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "TimeCreated", "TimeUpdated" },
                values: new object[] { "ca86932f-c4e6-4e85-b1e1-44dbeaffb802", "AQAAAAIAAYagAAAAED2VZjhoAMhwkMTcwg6VWTZrr0Ww+n4zGVyJSYFUHN2KIZUs0SQUvF97tjFynfBC+g==", "C2F479277AB70849909F856128B5C98A", new DateTimeOffset(new DateTime(2024, 3, 9, 11, 0, 17, 592, DateTimeKind.Unspecified).AddTicks(1914), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 3, 9, 11, 0, 17, 592, DateTimeKind.Unspecified).AddTicks(1918), new TimeSpan(0, 0, 0, 0, 0)) });
        }
    }
}
