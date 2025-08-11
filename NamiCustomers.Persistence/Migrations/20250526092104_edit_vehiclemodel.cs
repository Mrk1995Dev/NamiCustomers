using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NamiCustomers.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class edit_vehiclemodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FullDesc",
                table: "VehicleModels",
                newName: "VinNumber");

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

            migrationBuilder.AddColumn<Guid>(
                name: "VehicleModelIdSevensoft",
                table: "VehicleModels",
                type: "uniqueidentifier",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "VehicleModelIdSevensoft",
                table: "VehicleModels");

            migrationBuilder.RenameColumn(
                name: "VinNumber",
                table: "VehicleModels",
                newName: "FullDesc");
        }
    }
}
