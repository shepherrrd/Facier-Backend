using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AttendanceCapture.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SessionKey",
                table: "UserSessions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "TimeCreated", "TimeUpdated" },
                values: new object[] { "e7e71356-30d8-4435-871a-13adbfa03912", "AQAAAAIAAYagAAAAEAkDdj5VlRnLIbfLYrv4c1uAndciT5rDgUewzcLrDNkAEH4FzHZ1HxBwaDAjZDNAbg==", "80641C6694B59E4BB79FB32890D07A16", new DateTimeOffset(new DateTime(2024, 2, 29, 15, 55, 18, 870, DateTimeKind.Unspecified).AddTicks(4000), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 2, 29, 15, 55, 18, 870, DateTimeKind.Unspecified).AddTicks(4011), new TimeSpan(0, 0, 0, 0, 0)) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SessionKey",
                table: "UserSessions");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "TimeCreated", "TimeUpdated" },
                values: new object[] { "51cb8b44-98a0-4499-bffc-01db9460a31c", "AQAAAAIAAYagAAAAEMM9MO84rS6XeJb6brq281aMu7RAzIQA8EjZyjJ8/r6eTj2RuJRfccLfd4DUqzGauQ==", "78CA0BBAEBDA4F409E464238B473131B", new DateTimeOffset(new DateTime(2024, 2, 29, 15, 54, 16, 23, DateTimeKind.Unspecified).AddTicks(689), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 2, 29, 15, 54, 16, 23, DateTimeKind.Unspecified).AddTicks(695), new TimeSpan(0, 0, 0, 0, 0)) });
        }
    }
}
