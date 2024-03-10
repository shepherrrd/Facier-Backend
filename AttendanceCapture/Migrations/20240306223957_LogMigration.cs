using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AttendanceCapture.Migrations
{
    /// <inheritdoc />
    public partial class LogMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LogActivities",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    TimeCreated = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    TimeUpdated = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogActivities", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "TimeCreated", "TimeUpdated" },
                values: new object[] { "7b484569-95d9-41b2-bac1-ec3299163d4d", "AQAAAAIAAYagAAAAEAZWv2pQ5rSrOJrk/GQS3fKW5+CSjH9dBJXZlVQBjhIojmR24tfub+ZVFvvEuYo/QA==", "AB8F7AAF96E37A4BA376D6270ED8D0E5", new DateTimeOffset(new DateTime(2024, 3, 6, 22, 39, 56, 746, DateTimeKind.Unspecified).AddTicks(2865), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 3, 6, 22, 39, 56, 746, DateTimeKind.Unspecified).AddTicks(2870), new TimeSpan(0, 0, 0, 0, 0)) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LogActivities");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "TimeCreated", "TimeUpdated" },
                values: new object[] { "c16a738d-46e4-446c-ada4-606d3bcc01c1", "AQAAAAIAAYagAAAAEPVzdI9Vv1D8vU3+OK4GSiaBaIVJj6+XNTqRwygoZVpu7CCZWMm9Gt/ZQfAMxPEnTg==", "F70E099EC7E2804CB08720A5F1B08568", new DateTimeOffset(new DateTime(2024, 3, 5, 11, 5, 54, 753, DateTimeKind.Unspecified).AddTicks(4261), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 3, 5, 11, 5, 54, 753, DateTimeKind.Unspecified).AddTicks(4267), new TimeSpan(0, 0, 0, 0, 0)) });
        }
    }
}
