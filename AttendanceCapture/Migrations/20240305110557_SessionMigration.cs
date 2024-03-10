using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AttendanceCapture.Migrations
{
    /// <inheritdoc />
    public partial class SessionMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ExpiresAt",
                table: "UserSessions",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "Lecturers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "TimeCreated", "TimeUpdated" },
                values: new object[] { "c16a738d-46e4-446c-ada4-606d3bcc01c1", "AQAAAAIAAYagAAAAEPVzdI9Vv1D8vU3+OK4GSiaBaIVJj6+XNTqRwygoZVpu7CCZWMm9Gt/ZQfAMxPEnTg==", "F70E099EC7E2804CB08720A5F1B08568", new DateTimeOffset(new DateTime(2024, 3, 5, 11, 5, 54, 753, DateTimeKind.Unspecified).AddTicks(4261), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 3, 5, 11, 5, 54, 753, DateTimeKind.Unspecified).AddTicks(4267), new TimeSpan(0, 0, 0, 0, 0)) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpiresAt",
                table: "UserSessions");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Lecturers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "TimeCreated", "TimeUpdated" },
                values: new object[] { "9c05be85-309b-4af5-8974-9e84d58285b9", "AQAAAAIAAYagAAAAEGcAvs8zmhLnuA0Rq+fxagNegQNIJRgsCI6PGqplgRrSHYTEPxr9K63H9eM7HynS0g==", "CD478A88CFB7A249937B6A8AAB1CA03A", new DateTimeOffset(new DateTime(2024, 3, 2, 10, 46, 42, 42, DateTimeKind.Unspecified).AddTicks(5529), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 3, 2, 10, 46, 42, 42, DateTimeKind.Unspecified).AddTicks(5535), new TimeSpan(0, 0, 0, 0, 0)) });
        }
    }
}
