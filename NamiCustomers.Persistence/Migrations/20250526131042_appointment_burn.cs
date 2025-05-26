using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NamiCustomers.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class appointment_burn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dealers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DealerNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DealerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DealerAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DealerPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DealerprePhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DealerType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CityId = table.Column<int>(type: "int", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DealerMobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sort = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dealers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dealers_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubscriberId = table.Column<int>(type: "int", nullable: true),
                    ReservedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DealerId = table.Column<int>(type: "int", nullable: false),
                    ReservedNumber = table.Column<int>(type: "int", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRemoved = table.Column<bool>(type: "bit", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RemovedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointments_Dealers_DealerId",
                        column: x => x.DealerId,
                        principalTable: "Dealers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appointments_Subscribers_SubscriberId",
                        column: x => x.SubscriberId,
                        principalTable: "Subscribers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DealerSubscriber",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DealerId = table.Column<int>(type: "int", nullable: true),
                    SubscriberId = table.Column<int>(type: "int", nullable: true),
                    Code = table.Column<int>(type: "int", nullable: false),
                    IsAccept = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DealerSubscriber", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DealerSubscriber_Dealers_DealerId",
                        column: x => x.DealerId,
                        principalTable: "Dealers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DealerSubscriber_Subscribers_SubscriberId",
                        column: x => x.SubscriberId,
                        principalTable: "Subscribers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_DealerId",
                table: "Appointments",
                column: "DealerId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_SubscriberId",
                table: "Appointments",
                column: "SubscriberId");

            migrationBuilder.CreateIndex(
                name: "IX_Dealers_CityId",
                table: "Dealers",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_DealerSubscriber_DealerId",
                table: "DealerSubscriber",
                column: "DealerId");

            migrationBuilder.CreateIndex(
                name: "IX_DealerSubscriber_SubscriberId",
                table: "DealerSubscriber",
                column: "SubscriberId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "DealerSubscriber");

            migrationBuilder.DropTable(
                name: "Dealers");
        }
    }
}
