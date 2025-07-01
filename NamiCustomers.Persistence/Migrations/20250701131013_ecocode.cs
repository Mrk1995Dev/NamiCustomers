using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NamiCustomers.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ecocode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T1_SAMPLEs",
                schema: "PROJECTNAME");

            migrationBuilder.DropIndex(
                name: "IX_Subscribers_NatinalCode",
                table: "Subscribers");

            migrationBuilder.DropColumn(
                name: "NatinalCode",
                table: "Subscribers");

            migrationBuilder.RenameColumn(
                name: "PostalCode",
                table: "Subscribers",
                newName: "EconomicCode");

            migrationBuilder.AlterColumn<string>(
                name: "NationalCode",
                table: "Subscribers",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subscribers_NationalCode",
                table: "Subscribers",
                column: "NationalCode",
                unique: true,
                filter: "[NationalCode] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Subscribers_NationalCode",
                table: "Subscribers");

            migrationBuilder.EnsureSchema(
                name: "PROJECTNAME");

            migrationBuilder.RenameColumn(
                name: "EconomicCode",
                table: "Subscribers",
                newName: "PostalCode");

            migrationBuilder.AlterColumn<string>(
                name: "NationalCode",
                table: "Subscribers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NatinalCode",
                table: "Subscribers",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "T1_SAMPLEs",
                schema: "PROJECTNAME",
                columns: table => new
                {
                    T1_SAMPLEs_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    T1_SAMPLEs_Field = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsRemoved = table.Column<bool>(type: "bit", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RemovedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T1_SAMPLEs", x => x.T1_SAMPLEs_Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Subscribers_NatinalCode",
                table: "Subscribers",
                column: "NatinalCode",
                unique: true,
                filter: "[NatinalCode] IS NOT NULL");
        }
    }
}
