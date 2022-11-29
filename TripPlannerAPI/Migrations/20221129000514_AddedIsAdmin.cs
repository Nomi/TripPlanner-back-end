using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TripPlannerAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddedIsAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "42a2d777-4b02-42ad-93c2-563df51592aa");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5f5e2aa0-3738-4e58-af7e-8b1918e440c0");

            migrationBuilder.AddColumn<bool>(
                name: "isAdmin",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "ce733666-789d-4adc-af2c-7db0815da70d", "12a297e1-5820-47d9-8fbd-b4a136976219", "User", "USER" },
                    { "d4fa7a58-e684-4b0a-8319-75a59322e1f6", "db98d5bb-2d20-4861-acfc-99b2bab04444", "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ce733666-789d-4adc-af2c-7db0815da70d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d4fa7a58-e684-4b0a-8319-75a59322e1f6");

            migrationBuilder.DropColumn(
                name: "isAdmin",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "42a2d777-4b02-42ad-93c2-563df51592aa", "552e98e7-1a78-429a-8821-49b031fd0f54", "User", "USER" },
                    { "5f5e2aa0-3738-4e58-af7e-8b1918e440c0", "03f5694f-1f45-4304-83fe-de15e1496156", "Admin", "ADMIN" }
                });
        }
    }
}
