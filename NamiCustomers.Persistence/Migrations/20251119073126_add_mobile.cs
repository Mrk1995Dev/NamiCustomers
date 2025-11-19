using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NamiCustomers.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class add_mobile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "AspNetUserTokens",
                type: "datetime2",
                nullable: true,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUserTokens",
                type: "nvarchar(34)",
                maxLength: 34,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "AspNetUserTokens",
                type: "bit",
                nullable: true,
                defaultValue: true);

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "AspNetUserTokens",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExp",
                table: "AspNetUserTokens",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TokenType",
                table: "AspNetUserTokens",
                type: "int",
                nullable: true,
                defaultValue: 1);

            migrationBuilder.AddColumn<string>(
                name: "Mobile",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

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

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1109abb4-7619-4567-9a1b-8dcf5e4b73aa",
                column: "Mobile",
                value: "09191646456");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "AspNetUserTokens");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUserTokens");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "AspNetUserTokens");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "AspNetUserTokens");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExp",
                table: "AspNetUserTokens");

            migrationBuilder.DropColumn(
                name: "TokenType",
                table: "AspNetUserTokens");

            migrationBuilder.DropColumn(
                name: "Mobile",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "110alim-5841-4d44-b807-679d272e7110",
                column: "ConcurrencyStamp",
                value: null);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "283875b0-1760-4e08-ba59-e532dc873bb7",
                column: "ConcurrencyStamp",
                value: null);
        }
    }
}
