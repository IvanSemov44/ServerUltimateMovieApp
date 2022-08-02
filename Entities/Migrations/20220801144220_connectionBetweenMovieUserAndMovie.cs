using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    public partial class connectionBetweenMovieUserAndMovie : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d3f7fa51-8720-4caf-a1d6-5f0109ec8161");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f510293b-5909-4e6b-89b3-27b79cd76016");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "08d0cba9-5576-4387-87a5-15e779b7e9da", "dcc115b7-3c07-484b-a70e-93b77e8ef278", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a85dcfab-8c10-4be1-b1ae-5b274f5a3ddd", "06ef38a1-d3eb-437c-b5ff-b50e53bb3945", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "08d0cba9-5576-4387-87a5-15e779b7e9da");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a85dcfab-8c10-4be1-b1ae-5b274f5a3ddd");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d3f7fa51-8720-4caf-a1d6-5f0109ec8161", "4b635d62-e9b9-4691-9ec1-fba4d5383aff", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f510293b-5909-4e6b-89b3-27b79cd76016", "6ec048c2-eb44-43d9-a0c2-0b16cb11eb24", "User", "USER" });
        }
    }
}
