using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NamiCustomers.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updatecolumns2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BrandIdSevenSoft",
                table: "VehicleModels");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "VehicleModels");

            migrationBuilder.DropColumn(
                name: "SaleBasketIdSevenSoft",
                table: "VehicleModels");

            migrationBuilder.DropColumn(
                name: "SalePlanIdSevenSoft",
                table: "VehicleModels");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BrandIdSevenSoft",
                table: "VehicleModels",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "VehicleModels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SaleBasketIdSevenSoft",
                table: "VehicleModels",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SalePlanIdSevenSoft",
                table: "VehicleModels",
                type: "uniqueidentifier",
                nullable: true);
        }
    }
}
