using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NamiCustomers.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChageDtoVehicleModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BodyColor",
                table: "VehicleModels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ChassisUsageTypeName",
                table: "VehicleModels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FullSystem",
                table: "VehicleModels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MotorNumber",
                table: "VehicleModels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductYear",
                table: "VehicleModels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SelectedVehicleCommonName",
                table: "VehicleModels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SelectedVehicleDescription",
                table: "VehicleModels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "VehicleModelId",
                table: "VehicleModels",
                type: "uniqueidentifier",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BodyColor",
                table: "VehicleModels");

            migrationBuilder.DropColumn(
                name: "ChassisUsageTypeName",
                table: "VehicleModels");

            migrationBuilder.DropColumn(
                name: "FullSystem",
                table: "VehicleModels");

            migrationBuilder.DropColumn(
                name: "MotorNumber",
                table: "VehicleModels");

            migrationBuilder.DropColumn(
                name: "ProductYear",
                table: "VehicleModels");

            migrationBuilder.DropColumn(
                name: "SelectedVehicleCommonName",
                table: "VehicleModels");

            migrationBuilder.DropColumn(
                name: "SelectedVehicleDescription",
                table: "VehicleModels");

            migrationBuilder.DropColumn(
                name: "VehicleModelId",
                table: "VehicleModels");
        }
    }
}
