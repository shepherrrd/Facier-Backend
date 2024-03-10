using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AttendanceCapture.Migrations
{
    /// <inheritdoc />
    public partial class updatedAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "Email", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp", "TimeCreated", "TimeUpdated", "UserName" },
                values: new object[] { "9c05be85-309b-4af5-8974-9e84d58285b9", "technology@facifier.com", "TECHNOLOGY@FACIFIER.COM", "TECHNOLOGY@FACIFIER.COM", "AQAAAAIAAYagAAAAEGcAvs8zmhLnuA0Rq+fxagNegQNIJRgsCI6PGqplgRrSHYTEPxr9K63H9eM7HynS0g==", "CD478A88CFB7A249937B6A8AAB1CA03A", new DateTimeOffset(new DateTime(2024, 3, 2, 10, 46, 42, 42, DateTimeKind.Unspecified).AddTicks(5529), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 3, 2, 10, 46, 42, 42, DateTimeKind.Unspecified).AddTicks(5535), new TimeSpan(0, 0, 0, 0, 0)), "FACIFIER" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "Email", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp", "TimeCreated", "TimeUpdated", "UserName" },
                values: new object[] { "ed41fd91-7ab9-464b-8b87-9abef60350ae", "technology@dataaggregator.com", "TECHNOLOGY@DATAAGGREGATOR.COM", "TECHNOLOGY@DATAAGGREGATOR.COM", "AQAAAAIAAYagAAAAEKcoACnB/F+kJwoUvepqqGIxbbCmQRQ1+K77apwm88bVd0o2M4vudya+GCAM8/fLbA==", "6F178362F6DB894398062596D39FA660", new DateTimeOffset(new DateTime(2024, 2, 29, 16, 18, 39, 55, DateTimeKind.Unspecified).AddTicks(9707), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 2, 29, 16, 18, 39, 55, DateTimeKind.Unspecified).AddTicks(9713), new TimeSpan(0, 0, 0, 0, 0)), "TECHNOLOGY@DATAAGGREGATOR.COM" });
        }
    }
}
