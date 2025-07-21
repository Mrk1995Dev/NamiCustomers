using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NamiCustomers.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updatecolumnsn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleModels_Subscribers_SubscriberId",
                table: "VehicleModels");

 

            migrationBuilder.RenameColumn(
                name: "VehicleModelIdSevensoft",
                table: "VehicleModels",
                newName: "VehicleModelIdSevenSoft");

            migrationBuilder.AlterColumn<int>(
                name: "SubscriberId",
                table: "VehicleModels",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleModels_Subscribers_SubscriberId",
                table: "VehicleModels",
                column: "SubscriberId",
                principalTable: "Subscribers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleModels_Subscribers_SubscriberId",
                table: "VehicleModels");

            migrationBuilder.RenameColumn(
                name: "VehicleModelIdSevenSoft",
                table: "VehicleModels",
                newName: "VehicleModelIdSevensoft");

            migrationBuilder.AlterColumn<int>(
                name: "SubscriberId",
                table: "VehicleModels",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "VehicleModelId",
                table: "VehicleModels",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleModels_Subscribers_SubscriberId",
                table: "VehicleModels",
                column: "SubscriberId",
                principalTable: "Subscribers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
