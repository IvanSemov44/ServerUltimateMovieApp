using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    public partial class connectionBetweenMovieUserAndMovie2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "08d0cba9-5576-4387-87a5-15e779b7e9da");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a85dcfab-8c10-4be1-b1ae-5b274f5a3ddd");

            migrationBuilder.AddColumn<string>(
                name: "MovieUserId",
                table: "Movies",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3ca51907-0d2c-41c9-92fc-b1b88de2f6a8", "a4ac353a-dc58-426e-9f8c-f0974b707a7c", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f30555b3-80f4-4f2b-9531-ed88dba28443", "27e33cac-6398-45da-9ded-444e30b31cf5", "User", "USER" });

            migrationBuilder.CreateIndex(
                name: "IX_Movies_MovieUserId",
                table: "Movies",
                column: "MovieUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_AspNetUsers_MovieUserId",
                table: "Movies",
                column: "MovieUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_AspNetUsers_MovieUserId",
                table: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_Movies_MovieUserId",
                table: "Movies");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3ca51907-0d2c-41c9-92fc-b1b88de2f6a8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f30555b3-80f4-4f2b-9531-ed88dba28443");

            migrationBuilder.DropColumn(
                name: "MovieUserId",
                table: "Movies");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "08d0cba9-5576-4387-87a5-15e779b7e9da", "dcc115b7-3c07-484b-a70e-93b77e8ef278", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a85dcfab-8c10-4be1-b1ae-5b274f5a3ddd", "06ef38a1-d3eb-437c-b5ff-b50e53bb3945", "Admin", "ADMIN" });
        }
    }
}
