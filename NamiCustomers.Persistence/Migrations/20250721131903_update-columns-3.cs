using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NamiCustomers.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updatecolumns3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleAttachments_VehicleModels_VehicleModelId",
                table: "VehicleAttachments");

            migrationBuilder.DropIndex(
                name: "IX_VehicleAttachments_VehicleModelId",
                table: "VehicleAttachments");

            migrationBuilder.DropColumn(
                name: "VehicleModelId",
                table: "VehicleAttachments");

            migrationBuilder.AddColumn<int>(
                name: "VehicleAttachmentId",
                table: "VehicleModels",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "VehicleModelIdSevenSoft",
                table: "VehicleAttachments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VehicleModels_VehicleAttachmentId",
                table: "VehicleModels",
                column: "VehicleAttachmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleModels_VehicleAttachments_VehicleAttachmentId",
                table: "VehicleModels",
                column: "VehicleAttachmentId",
                principalTable: "VehicleAttachments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleModels_VehicleAttachments_VehicleAttachmentId",
                table: "VehicleModels");

            migrationBuilder.DropIndex(
                name: "IX_VehicleModels_VehicleAttachmentId",
                table: "VehicleModels");

            migrationBuilder.DropColumn(
                name: "VehicleAttachmentId",
                table: "VehicleModels");

            migrationBuilder.DropColumn(
                name: "VehicleModelIdSevenSoft",
                table: "VehicleAttachments");

            migrationBuilder.AddColumn<int>(
                name: "VehicleModelId",
                table: "VehicleAttachments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_VehicleAttachments_VehicleModelId",
                table: "VehicleAttachments",
                column: "VehicleModelId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleAttachments_VehicleModels_VehicleModelId",
                table: "VehicleAttachments",
                column: "VehicleModelId",
                principalTable: "VehicleModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
