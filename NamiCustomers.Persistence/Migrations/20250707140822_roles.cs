using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NamiCustomers.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class roles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "AspNetRoles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "110alim-5841-4d44-b807-679d272e7110", null, "ادمین", "Admin", "ADMIN" },
                    { "283875b0-1760-4e08-ba59-e532dc873bb7", null, "اپراتور", "Operator", "OPERATOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NationalCode", "NormalizedEmail", "NormalizedUserName", "PassWord", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "1109abb4-7619-4567-9a1b-8dcf5e4b73aa", 0, "7a0803d9-dac0-446f-b145-a48b202dbf52", "a.moardi@namikhodro.com", true, "علی", "مرادی", false, null, null, "A.MORADI@NAMIKHODRO.COM", "A.MORADI@NAMIKHODRO.COM", "Aa12334566*", "AQAAAAIAAYagAAAAEDWXR4EMPJhFFXowJTQ51DhTJ8/Trup0It8Ws2LzXKTf1sIhEMuKY3UFbYG/7uoq2A==", "09191646456", true, "VDH6RYMZDZ2U5JB5VYQRK47G6LZRQJ6O", false, "a.moradi@namikhodro.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "110alim-5841-4d44-b807-679d272e7110", "1109abb4-7619-4567-9a1b-8dcf5e4b73aa" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "283875b0-1760-4e08-ba59-e532dc873bb7");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "110alim-5841-4d44-b807-679d272e7110", "1109abb4-7619-4567-9a1b-8dcf5e4b73aa" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "110alim-5841-4d44-b807-679d272e7110");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1109abb4-7619-4567-9a1b-8dcf5e4b73aa");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "AspNetRoles");
        }
    }
}
