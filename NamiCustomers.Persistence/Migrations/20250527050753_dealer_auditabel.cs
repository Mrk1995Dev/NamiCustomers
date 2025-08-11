using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NamiCustomers.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class dealer_auditabel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreateAt",
                table: "DealerSubscriber",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsRemoved",
                table: "DealerSubscriber",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedAt",
                table: "DealerSubscriber",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RemovedAt",
                table: "DealerSubscriber",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateAt",
                table: "Dealers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsRemoved",
                table: "Dealers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedAt",
                table: "Dealers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RemovedAt",
                table: "Dealers",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateAt",
                table: "DealerSubscriber");

            migrationBuilder.DropColumn(
                name: "IsRemoved",
                table: "DealerSubscriber");

            migrationBuilder.DropColumn(
                name: "LastModifiedAt",
                table: "DealerSubscriber");

            migrationBuilder.DropColumn(
                name: "RemovedAt",
                table: "DealerSubscriber");

            migrationBuilder.DropColumn(
                name: "CreateAt",
                table: "Dealers");

            migrationBuilder.DropColumn(
                name: "IsRemoved",
                table: "Dealers");

            migrationBuilder.DropColumn(
                name: "LastModifiedAt",
                table: "Dealers");

            migrationBuilder.DropColumn(
                name: "RemovedAt",
                table: "Dealers");
        }
    }
}
