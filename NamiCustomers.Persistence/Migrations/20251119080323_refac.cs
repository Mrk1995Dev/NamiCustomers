using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NamiCustomers.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class refac : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "110alim-5841-4d44-b807-679d272e7110",
                column: "ConcurrencyStamp",
                value: "fbbd208c-d84c-4ca9-9e51-cd5270945bef");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "283875b0-1760-4e08-ba59-e532dc873bb7",
                column: "ConcurrencyStamp",
                value: "7965cad9-1ac4-4c95-9ec1-c0ad1203f5e0");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserTokens_ApplicationUserId",
                table: "AspNetUserTokens",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_ApplicationUserId",
                table: "AspNetUserTokens",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_ApplicationUserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserTokens_ApplicationUserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "AspNetUserTokens");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "110alim-5841-4d44-b807-679d272e7110",
                column: "ConcurrencyStamp",
                value: "c4000268-6668-465f-8c60-21db2913b4ea");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "283875b0-1760-4e08-ba59-e532dc873bb7",
                column: "ConcurrencyStamp",
                value: "3abe6670-5c59-44e7-ad77-9ca4dcdb65fd");
        }
    }
}
