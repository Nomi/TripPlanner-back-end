using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TripPlannerAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddedIsAdmin2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ce733666-789d-4adc-af2c-7db0815da70d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d4fa7a58-e684-4b0a-8319-75a59322e1f6");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "14544e1a-93f8-4a4e-9d15-cf5828e68b47", "12f1462e-21fd-4c39-b706-b22fd29956a4", "User", "USER" },
                    { "b66c1402-3fb8-4b16-bede-27f3c7927cfb", "22587476-b225-44c2-b904-4ab54450596d", "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "14544e1a-93f8-4a4e-9d15-cf5828e68b47");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b66c1402-3fb8-4b16-bede-27f3c7927cfb");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "ce733666-789d-4adc-af2c-7db0815da70d", "12a297e1-5820-47d9-8fbd-b4a136976219", "User", "USER" },
                    { "d4fa7a58-e684-4b0a-8319-75a59322e1f6", "db98d5bb-2d20-4861-acfc-99b2bab04444", "Admin", "ADMIN" }
                });
        }
    }
}
