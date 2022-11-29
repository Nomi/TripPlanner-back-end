using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TripPlannerAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddedRemIsAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "14544e1a-93f8-4a4e-9d15-cf5828e68b47");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b66c1402-3fb8-4b16-bede-27f3c7927cfb");

            migrationBuilder.DropColumn(
                name: "isAdmin",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "14238eb6-c809-43a0-83e6-097831a13692", "6fd8a16f-2a46-4af7-b24b-5b73852f70a9", "User", "USER" },
                    { "1d2ef803-ef44-452e-a16f-5bf53ab4f735", "33fa6fd0-85ad-4e03-8fea-41803bac3272", "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "14238eb6-c809-43a0-83e6-097831a13692");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1d2ef803-ef44-452e-a16f-5bf53ab4f735");

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
                    { "14544e1a-93f8-4a4e-9d15-cf5828e68b47", "12f1462e-21fd-4c39-b706-b22fd29956a4", "User", "USER" },
                    { "b66c1402-3fb8-4b16-bede-27f3c7927cfb", "22587476-b225-44c2-b904-4ab54450596d", "Admin", "ADMIN" }
                });
        }
    }
}
