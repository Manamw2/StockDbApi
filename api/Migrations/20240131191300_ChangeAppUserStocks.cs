using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class ChangeAppUserStocks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a1b676cc-1038-4a7a-a41e-973372e76238");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d5731a1d-535b-4291-919c-6a1c0ab7fbd2");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6031432a-4758-47dc-b001-087fe90d10c3", null, "admin", "ADMIN" },
                    { "766b8cfa-c8c1-4580-a10f-ff6440397bcb", null, "user", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6031432a-4758-47dc-b001-087fe90d10c3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "766b8cfa-c8c1-4580-a10f-ff6440397bcb");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "a1b676cc-1038-4a7a-a41e-973372e76238", null, "user", "USER" },
                    { "d5731a1d-535b-4291-919c-6a1c0ab7fbd2", null, "admin", "ADMIN" }
                });
        }
    }
}
