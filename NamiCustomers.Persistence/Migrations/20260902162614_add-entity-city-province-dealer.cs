using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NamiCustomers.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addentitycityprovincedealer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "VehicleModels");

            //migrationBuilder.AddColumn<string>(
            //    name: "HashPassword",
            //    table: "Subscribers",
            //    type: "nvarchar(max)",
            //    nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Cities",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "ProvinceId",
                table: "Cities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Dealers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DealerNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    DealerName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ManagerName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DealerAddress = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DealerPrePhone = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    DealerPhone = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    DealerMobile = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Fax = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    DealerType = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    NationalId = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    EconomicCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Latitude = table.Column<double>(type: "float", nullable: true),
                    Longitude = table.Column<double>(type: "float", nullable: true),
                    Sort = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRemoved = table.Column<bool>(type: "bit", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RemovedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dealers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dealers_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Provinces",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRemoved = table.Column<bool>(type: "bit", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RemovedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provinces", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Provinces",
                columns: new[] { "Id", "Code", "CreateAt", "IsRemoved", "LastModifiedAt", "RemovedAt", "Title" },
                values: new object[,]
                {
                    { 1, "01", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, null, "آذربایجان شرقی" },
                    { 2, "02", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, null, "آذربایجان غربی" },
                    { 3, "03", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, null, "اردبیل" },
                    { 4, "04", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, null, "اصفهان" },
                    { 5, "05", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, null, "البرز" },
                    { 6, "06", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, null, "ایلام" },
                    { 7, "07", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, null, "بوشهر" },
                    { 8, "08", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, null, "تهران" },
                    { 9, "09", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, null, "چهارمحال و بختیاری" },
                    { 10, "10", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, null, "خراسان جنوبی" },
                    { 11, "11", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, null, "خراسان رضوی" },
                    { 12, "12", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, null, "خراسان شمالی" },
                    { 13, "13", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, null, "خوزستان" },
                    { 14, "14", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, null, "زنجان" },
                    { 15, "15", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, null, "سمنان" },
                    { 16, "16", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, null, "سیستان و بلوچستان" },
                    { 17, "17", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, null, "فارس" },
                    { 18, "18", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, null, "قزوین" },
                    { 19, "19", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, null, "قم" },
                    { 20, "20", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, null, "کردستان" },
                    { 21, "21", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, null, "کرمان" },
                    { 22, "22", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, null, "کرمانشاه" },
                    { 23, "23", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, null, "کهگیلویه و بویراحمد" },
                    { 24, "24", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, null, "گلستان" },
                    { 25, "25", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, null, "گیلان" },
                    { 26, "26", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, null, "لرستان" },
                    { 27, "27", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, null, "مازندران" },
                    { 28, "28", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, null, "مرکزی" },
                    { 29, "29", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, null, "هرمزگان" },
                    { 30, "30", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, null, "همدان" },
                    { 31, "31", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, null, "یزد" }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CreateAt", "IsRemoved", "LastModifiedAt", "ProvinceId", "RemovedAt", "Title" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 1, null, "تبریز" },
                    { 2, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 2, null, "ارومیه" },
                    { 3, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 3, null, "اردبیل" },
                    { 4, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 4, null, "اصفهان" },
                    { 5, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 5, null, "کرج" },
                    { 6, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 6, null, "ایلام" },
                    { 7, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 7, null, "بوشهر" },
                    { 8, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 8, null, "تهران" },
                    { 9, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 9, null, "شهرکرد" },
                    { 10, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 10, null, "بیرجند" },
                    { 11, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 11, null, "مشهد" },
                    { 12, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 12, null, "بجنورد" },
                    { 13, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 13, null, "اهواز" },
                    { 14, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 14, null, "زنجان" },
                    { 15, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 15, null, "سمنان" },
                    { 16, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 16, null, "زاهدان" },
                    { 17, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 17, null, "شیراز" },
                    { 18, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 18, null, "قزوین" },
                    { 19, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 19, null, "قم" },
                    { 20, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 20, null, "سنندج" },
                    { 21, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 21, null, "کرمان" },
                    { 22, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 22, null, "کرمانشاه" },
                    { 23, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 23, null, "یاسوج" },
                    { 24, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 24, null, "گرگان" },
                    { 25, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 25, null, "رشت" },
                    { 26, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 26, null, "خرم‌آباد" },
                    { 27, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 27, null, "ساری" },
                    { 28, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 28, null, "اراک" },
                    { 29, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 29, null, "بندرعباس" },
                    { 30, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 30, null, "همدان" },
                    { 31, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 31, null, "یزد" },
                    { 32, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 1, null, "مراغه" },
                    { 33, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 1, null, "مرند" },
                    { 34, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 1, null, "میانه" },
                    { 35, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 1, null, "اهر" },
                    { 36, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 1, null, "بناب" },
                    { 37, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 1, null, "شبستر" },
                    { 38, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 1, null, "سراب" },
                    { 39, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 1, null, "جلفا" },
                    { 40, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 1, null, "آذرشهر" },
                    { 41, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 1, null, "اسکو" },
                    { 42, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 1, null, "هشترود" },
                    { 43, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 1, null, "هریس" },
                    { 44, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 1, null, "ملکان" },
                    { 45, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 1, null, "عجب‌شیر" },
                    { 46, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 2, null, "خوی" },
                    { 47, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 2, null, "مهاباد" },
                    { 48, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 2, null, "میاندوآب" },
                    { 49, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 2, null, "بوکان" },
                    { 50, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 2, null, "سلماس" },
                    { 51, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 2, null, "پیرانشهر" },
                    { 52, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 2, null, "نقده" },
                    { 53, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 2, null, "ماکو" },
                    { 54, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 2, null, "سردشت" },
                    { 55, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 2, null, "تکاب" },
                    { 56, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 2, null, "شاهین‌دژ" },
                    { 57, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 2, null, "اشنویه" },
                    { 58, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 3, null, "پارس‌آباد" },
                    { 59, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 3, null, "مشگین‌شهر" },
                    { 60, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 3, null, "خلخال" },
                    { 61, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 3, null, "گرمی" },
                    { 62, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 3, null, "نمین" },
                    { 63, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 3, null, "سرعین" },
                    { 64, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 3, null, "بیله‌سوار" },
                    { 65, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 4, null, "کاشان" },
                    { 66, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 4, null, "نجف‌آباد" },
                    { 67, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 4, null, "خمینی‌شهر" },
                    { 68, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 4, null, "شاهین‌شهر" },
                    { 69, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 4, null, "فلاورجان" },
                    { 70, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 4, null, "شهرضا" },
                    { 71, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 4, null, "مبارکه" },
                    { 72, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 4, null, "گلپایگان" },
                    { 73, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 4, null, "نائین" },
                    { 74, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 4, null, "آران و بیدگل" },
                    { 75, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 4, null, "خوانسار" },
                    { 76, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 4, null, "نطنز" },
                    { 77, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 4, null, "اردستان" },
                    { 78, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 4, null, "سمیرم" },
                    { 79, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 5, null, "فردیس" },
                    { 80, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 5, null, "نظرآباد" },
                    { 81, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 5, null, "هشتگرد" },
                    { 82, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 5, null, "طالقان" },
                    { 83, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 5, null, "اشتهارد" },
                    { 84, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 5, null, "گرمدره" },
                    { 85, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 5, null, "ماهدشت" },
                    { 86, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 6, null, "دهلران" },
                    { 87, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 6, null, "آبدانان" },
                    { 88, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 6, null, "ایوان" },
                    { 89, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 6, null, "مهران" },
                    { 90, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 6, null, "دره‌شهر" },
                    { 91, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 6, null, "چرداول" },
                    { 92, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 7, null, "برازجان" },
                    { 93, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 7, null, "گناوه" },
                    { 94, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 7, null, "کنگان" },
                    { 95, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 7, null, "عسلویه" },
                    { 96, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 7, null, "دیر" },
                    { 97, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 7, null, "دیلم" },
                    { 98, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 7, null, "جم" },
                    { 99, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 8, null, "اسلامشهر" },
                    { 100, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 8, null, "شهریار" },
                    { 101, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 8, null, "ری" },
                    { 102, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 8, null, "قدس" },
                    { 103, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 8, null, "ملارد" },
                    { 104, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 8, null, "پاکدشت" },
                    { 105, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 8, null, "ورامین" },
                    { 106, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 8, null, "قرچک" },
                    { 107, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 8, null, "پردیس" },
                    { 108, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 8, null, "دماوند" },
                    { 109, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 8, null, "فیروزکوه" },
                    { 110, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 8, null, "رباط‌کریم" },
                    { 111, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 8, null, "پرند" },
                    { 112, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 8, null, "اندیشه" },
                    { 113, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 8, null, "بومهن" },
                    { 114, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 8, null, "لواسان" },
                    { 115, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 9, null, "بروجن" },
                    { 116, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 9, null, "فارسان" },
                    { 117, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 9, null, "لردگان" },
                    { 118, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 9, null, "کوهرنگ" },
                    { 119, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 9, null, "اردل" },
                    { 120, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 9, null, "سامان" },
                    { 121, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 10, null, "قائن" },
                    { 122, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 10, null, "فردوس" },
                    { 123, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 10, null, "طبس" },
                    { 124, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 10, null, "نهبندان" },
                    { 125, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 10, null, "سرایان" },
                    { 126, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 10, null, "بشرویه" },
                    { 127, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 11, null, "نیشابور" },
                    { 128, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 11, null, "سبزوار" },
                    { 129, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 11, null, "تربت حیدریه" },
                    { 130, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 11, null, "کاشمر" },
                    { 131, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 11, null, "قوچان" },
                    { 132, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 11, null, "تربت جام" },
                    { 133, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 11, null, "چناران" },
                    { 134, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 11, null, "گناباد" },
                    { 135, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 11, null, "تایباد" },
                    { 136, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 11, null, "خواف" },
                    { 137, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 11, null, "درگز" },
                    { 138, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 11, null, "فریمان" },
                    { 139, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 11, null, "سرخس" },
                    { 140, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 11, null, "بردسکن" },
                    { 141, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 12, null, "شیروان" },
                    { 142, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 12, null, "اسفراین" },
                    { 143, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 12, null, "آشخانه" },
                    { 144, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 12, null, "فاروج" },
                    { 145, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 12, null, "جاجرم" },
                    { 146, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 13, null, "آبادان" },
                    { 147, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 13, null, "خرمشهر" },
                    { 148, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 13, null, "دزفول" },
                    { 149, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 13, null, "اندیمشک" },
                    { 150, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 13, null, "شوشتر" },
                    { 151, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 13, null, "مسجدسلیمان" },
                    { 152, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 13, null, "ایذه" },
                    { 153, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 13, null, "بهبهان" },
                    { 154, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 13, null, "ماهشهر" },
                    { 155, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 13, null, "شادگان" },
                    { 156, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 13, null, "شوش" },
                    { 157, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 13, null, "رامهرمز" },
                    { 158, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 13, null, "امیدیه" },
                    { 159, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 14, null, "ابهر" },
                    { 160, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 14, null, "خرمدره" },
                    { 161, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 14, null, "قیدار" },
                    { 162, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 14, null, "ماه‌نشان" },
                    { 163, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 14, null, "سلطانیه" },
                    { 164, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 15, null, "شاهرود" },
                    { 165, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 15, null, "دامغان" },
                    { 166, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 15, null, "گرمسار" },
                    { 167, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 15, null, "مهدی‌شهر" },
                    { 168, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 15, null, "ایوانکی" },
                    { 169, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 16, null, "چابهار" },
                    { 170, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 16, null, "ایرانشهر" },
                    { 171, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 16, null, "زابل" },
                    { 172, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 16, null, "خاش" },
                    { 173, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 16, null, "سراوان" },
                    { 174, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 16, null, "نیک‌شهر" },
                    { 175, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 16, null, "کنارک" },
                    { 176, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 16, null, "میرجاوه" },
                    { 177, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 17, null, "مرودشت" },
                    { 178, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 17, null, "جهرم" },
                    { 179, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 17, null, "کازرون" },
                    { 180, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 17, null, "فسا" },
                    { 181, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 17, null, "داراب" },
                    { 182, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 17, null, "لار" },
                    { 183, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 17, null, "فیروزآباد" },
                    { 184, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 17, null, "آباده" },
                    { 185, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 17, null, "نی‌ریز" },
                    { 186, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 17, null, "لامرد" },
                    { 187, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 17, null, "استهبان" },
                    { 188, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 17, null, "اقلید" },
                    { 189, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 18, null, "تاکستان" },
                    { 190, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 18, null, "آبیک" },
                    { 191, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 18, null, "بوئین‌زهرا" },
                    { 192, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 18, null, "الوند" },
                    { 193, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 18, null, "محمدیه" },
                    { 194, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 18, null, "آوج" },
                    { 195, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 19, null, "جعفریه" },
                    { 196, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 19, null, "کهک" },
                    { 197, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 19, null, "سلفچگان" },
                    { 198, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 20, null, "سقز" },
                    { 199, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 20, null, "مریوان" },
                    { 200, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 20, null, "بانه" },
                    { 201, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 20, null, "قروه" },
                    { 202, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 20, null, "بیجار" },
                    { 203, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 20, null, "کامیاران" },
                    { 204, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 20, null, "دیواندره" },
                    { 205, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 21, null, "سیرجان" },
                    { 206, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 21, null, "رفسنجان" },
                    { 207, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 21, null, "جیرفت" },
                    { 208, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 21, null, "بم" },
                    { 209, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 21, null, "زرند" },
                    { 210, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 21, null, "کهنوج" },
                    { 211, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 21, null, "شهربابک" },
                    { 212, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 21, null, "بردسیر" },
                    { 213, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 21, null, "بافت" },
                    { 214, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 22, null, "اسلام‌آباد غرب" },
                    { 215, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 22, null, "سنقر" },
                    { 216, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 22, null, "کنگاور" },
                    { 217, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 22, null, "جوانرود" },
                    { 218, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 22, null, "سرپل ذهاب" },
                    { 219, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 22, null, "پاوه" },
                    { 220, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 22, null, "هرسین" },
                    { 221, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 22, null, "گیلانغرب" },
                    { 222, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 23, null, "دوگنبدان" },
                    { 223, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 23, null, "دهدشت" },
                    { 224, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 23, null, "سی‌سخت" },
                    { 225, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 23, null, "لیکک" },
                    { 226, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 23, null, "باشت" },
                    { 227, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 24, null, "گنبد کاووس" },
                    { 228, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 24, null, "علی‌آباد کتول" },
                    { 229, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 24, null, "بندرترکمن" },
                    { 230, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 24, null, "کردکوی" },
                    { 231, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 24, null, "آق‌قلا" },
                    { 232, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 24, null, "آزادشهر" },
                    { 233, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 24, null, "کلاله" },
                    { 234, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 24, null, "مینودشت" },
                    { 235, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 25, null, "بندر انزلی" },
                    { 236, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 25, null, "لاهیجان" },
                    { 237, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 25, null, "لنگرود" },
                    { 238, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 25, null, "تالش" },
                    { 239, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 25, null, "آستارا" },
                    { 240, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 25, null, "رودسر" },
                    { 241, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 25, null, "صومعه‌سرا" },
                    { 242, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 25, null, "فومن" },
                    { 243, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 25, null, "آستانه اشرفیه" },
                    { 244, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 25, null, "رودبار" },
                    { 245, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 26, null, "بروجرد" },
                    { 246, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 26, null, "دورود" },
                    { 247, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 26, null, "الیگودرز" },
                    { 248, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 26, null, "کوهدشت" },
                    { 249, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 26, null, "ازنا" },
                    { 250, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 26, null, "نورآباد" },
                    { 251, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 26, null, "الشتر" },
                    { 252, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 26, null, "پلدختر" },
                    { 253, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 27, null, "بابل" },
                    { 254, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 27, null, "آمل" },
                    { 255, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 27, null, "قائمشهر" },
                    { 256, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 27, null, "تنکابن" },
                    { 257, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 27, null, "بهشهر" },
                    { 258, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 27, null, "نوشهر" },
                    { 259, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 27, null, "چالوس" },
                    { 260, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 27, null, "بابلسر" },
                    { 261, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 27, null, "رامسر" },
                    { 262, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 27, null, "نکا" },
                    { 263, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 27, null, "محمودآباد" },
                    { 264, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 27, null, "نور" },
                    { 265, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 28, null, "ساوه" },
                    { 266, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 28, null, "خمین" },
                    { 267, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 28, null, "محلات" },
                    { 268, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 28, null, "دلیجان" },
                    { 269, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 28, null, "تفرش" },
                    { 270, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 28, null, "شازند" },
                    { 271, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 28, null, "آشتیان" },
                    { 272, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 29, null, "میناب" },
                    { 273, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 29, null, "بندر لنگه" },
                    { 274, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 29, null, "قشم" },
                    { 275, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 29, null, "کیش" },
                    { 276, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 29, null, "حاجی‌آباد" },
                    { 277, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 29, null, "رودان" },
                    { 278, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 29, null, "بستک" },
                    { 279, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 29, null, "جاسک" },
                    { 280, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 30, null, "ملایر" },
                    { 281, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 30, null, "نهاوند" },
                    { 282, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 30, null, "تویسرکان" },
                    { 283, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 30, null, "کبودرآهنگ" },
                    { 284, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 30, null, "رزن" },
                    { 285, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 30, null, "بهار" },
                    { 286, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 30, null, "اسدآباد" },
                    { 287, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 31, null, "میبد" },
                    { 288, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 31, null, "اردکان" },
                    { 289, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 31, null, "بافق" },
                    { 290, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 31, null, "مهریز" },
                    { 291, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 31, null, "تفت" },
                    { 292, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 31, null, "ابرکوه" },
                    { 293, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), false, null, 31, null, "اشکذر" }
                });

            migrationBuilder.InsertData(
                table: "Dealers",
                columns: new[] { "Id", "CityId", "CreateAt", "DealerAddress", "DealerMobile", "DealerName", "DealerNo", "DealerPhone", "DealerPrePhone", "DealerType", "Description", "EconomicCode", "Email", "Fax", "IsActive", "IsRemoved", "LastModifiedAt", "Latitude", "Longitude", "ManagerName", "NationalId", "PostalCode", "RemovedAt", "Sort" },
                values: new object[,]
                {
                    { 1, 8, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "تهران، خیابان شهید مطهری، خیابان فجر، ساختمان نامی", null, "شعبه مرکزی", null, "41421", "021", 1, null, null, "info@namikhodro.com", null, true, false, null, null, null, null, null, null, null, 1 },
                    { 2, 8, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "تهران، ۴۵ متری رسالت، بعد از ۱۶ متری دوم مجیدیه، نرسیده به خیابان کرمان، پلاک ۹۳۰", null, "ظفرقندی (عاملیت مرکزی)", "701", "22300973-5", "021", 3, null, null, null, null, true, false, null, null, null, null, null, null, null, 2 },
                    { 3, 8, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "تهران، خیابان شریعتی، بالاتر از پل سیدخندان، خیابان خواجه عبدالله انصاری", null, "بخشی", "729", "22841616", "021", 3, null, null, null, null, true, false, null, null, null, null, null, null, null, 3 },
                    { 4, 17, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "فروش: شیراز، بلوار امیرکبیر، نبش والفجر. خدمات پس از فروش: شیراز، بلوار سلمان فارسی، جنب پمپ بنزین، کوچه ۱", null, "نیکوان", "702", "90000745، 38333331، 38333332", "071", 3, null, null, null, null, true, false, null, null, null, null, null, null, null, 4 },
                    { 5, 253, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "بابل، کیلومتر ۳ امیرکلا به بابلسر", null, "جمالی", "703", "44413201-3", "011", 3, null, null, null, null, true, false, null, null, null, null, null, null, null, 5 },
                    { 6, 24, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "گرگان، کیلومتر ۱ جاده گنبد", null, "بازرگانی خودرو ماندگار گلستان", "704", "32179000", "017", 3, null, null, null, null, true, false, null, null, null, null, null, null, null, 6 },
                    { 7, 13, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "اهواز، ابتدای اتوبان آیت‌الله بهبهانی، ۲۰۰ متر بعد از میدان جمهوری، پلاک ۹۵۷", null, "کلهر", "705", "35545817", "061", 3, null, null, null, null, true, false, null, null, null, null, null, null, null, 7 },
                    { 8, 254, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "آمل، میدان هزار سنگر، کیلومتر ۶ جاده جدید بابل-دابودشت", null, "گیلانی", "706", "4124", "011", 3, null, null, null, null, true, false, null, null, null, null, null, null, null, 8 },
                    { 9, 7, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "بوشهر، بلوار شهید قرنی، قبل از میدان امام علی، مجموعه قنبرپور", null, "قنبرپور", "707", "33451163-33451422", "077", 3, null, null, null, null, true, false, null, null, null, null, null, null, null, 9 },
                    { 10, 18, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "قزوین، بلوار شهید بهشتی، بعد از بیمارستان قدس، جنب هلال احمر", null, "همتی", "708", "فروش: 33344881 | خدمات: 33347040", "028", 3, null, null, null, null, true, false, null, null, null, null, null, null, null, 10 },
                    { 11, 21, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "کرمان، بلوار شهید صدوقی، بین بلوار هزار و یک شب جنوبی و بلوار فارابی", null, "گسترش ایده‌های تجاری گات", "709", "فروش: 32466617 | خدمات: 62466617", "034", 3, null, null, null, null, true, false, null, null, null, null, null, null, null, 11 },
                    { 12, 19, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "قم، خیابان امام خمینی، پلاک ۲۷۵", null, "پورات", "711", "فروش: 36622247 | خدمات: 36603900 / 36604238", "025", 3, null, null, null, null, true, false, null, null, null, null, null, null, null, 12 },
                    { 13, 205, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "فروش: سیرجان، خیابان مقداد، نبش خیابان رجائی. خدمات پس از فروش: سیرجان، کیلومتر ۳ جاده تهران، روبروی منطقه ویژه اقتصادی", null, "اسفندیارپور", "712", "42261197", "034", 3, null, null, null, null, true, false, null, null, null, null, null, null, null, 13 },
                    { 14, 5, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "کرج، پل فردیس، ابتدای جاده ملارد، بعد از پل سرحدآباد", null, "چهره", "713", "36615027", "026", 3, null, null, null, null, true, false, null, null, null, null, null, null, null, 14 },
                    { 15, 14, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "زنجان، خیابان خیام غربی، روبروی میراث فرهنگی", null, "ثابت قدم", "714", "33331116-33366000", "024", 3, null, null, null, null, true, false, null, null, null, null, null, null, null, 15 },
                    { 16, 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "فروش: تبریز، ولیعصر، نرسیده به فلکه معلم، روبروی ناحیه پستی، پلاک ۲۰. خدمات پس از فروش: تبریز، بالاتر از میدان بسیج، جنب کارخانه آناتا", null, "تلاش خودرو ایرانیان", "715", "82868686", "041", 3, null, null, null, null, true, false, null, null, null, null, null, null, null, 16 },
                    { 17, 30, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "همدان، میدان هگمتانه، بلوار بم", null, "شیری-حنیفی", "716", "34243470", "081", 3, null, null, null, null, true, false, null, null, null, null, null, null, null, 17 },
                    { 18, 22, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "فروش: کرمانشاه، گلریزان، پایین‌تر از چهارراه خرم، پلاک ۸۱۲. خدمات پس از فروش: کرمانشاه، اربابی، خیابان حکیم نظامی", null, "نعمتی", "717", "فروش: 38438346 | خدمات: 38249218", "083", 3, null, null, null, null, true, false, null, null, null, null, null, null, null, 18 },
                    { 19, 164, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "شاهرود، میدان هفت تیر، جاده کارخانه قند، ابتدای جاده مغان، مجتمع خودرویی رضائی", null, "رضائی", "718", "31020", "023", 3, null, null, null, null, true, false, null, null, null, null, null, null, null, 19 },
                    { 20, 25, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "رشت، کیلومتر ۳ جاده رشت به فومن، آتشگاه", null, "فن‌آوران صنعت خودرو", "719", "33594501-4", "013", 3, null, null, null, null, true, false, null, null, null, null, null, null, null, 20 },
                    { 21, 31, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "یزد، بلوار مدرس، میدان نماز، ابتدای خیابان ولیعصر، خیابان سعادت", null, "توانگر", "720", "36241300 / 36241400 / 36241500", "035", 3, null, null, null, null, true, false, null, null, null, null, null, null, null, 21 },
                    { 22, 272, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "میناب، بلوار سردار سلیمانی، بعد از پمپ بنزین", null, "زارعی", "721", "42281400-2", "076", 3, null, null, null, null, true, false, null, null, null, null, null, null, null, 22 },
                    { 23, 11, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "فروش: مشهد، خیابان ملک‌الشعرا بهار، بین ملک‌الشعرا بهار ۴۶ و ۴۸، پلاک ۱۵۱. خدمات پس از فروش: مشهد، خیابان ملک‌الشعرا بهار، خیابان ملک‌الشعرا بهار ۴۸ (سپه ۲)، پلاک ۳", null, "صالحی", "725", "38553353", "051", 3, null, null, null, null, true, false, null, null, null, null, null, null, null, 23 },
                    { 24, 16, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "زاهدان، میدان پانزده خرداد", null, "غازی‌زاده", "726", "33230415", "054", 3, null, null, null, null, true, false, null, null, null, null, null, null, null, 24 },
                    { 25, 128, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "سبزوار، حد فاصل چهارراه کوشک و میدان مادر", null, "افشاری‌کیا", "727", "فروش: 44248080 | خدمات: 44248181", "051", 3, null, null, null, null, true, false, null, null, null, null, null, null, null, 25 },
                    { 26, 4, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "اصفهان، خیابان امام خمینی، نبش کوچه مینو", null, "بازرگانی قناد محور موتور", "728", "37111", "031", 3, null, null, null, null, true, false, null, null, null, null, null, null, null, 26 }
                });

            migrationBuilder.InsertData(
                table: "Dealers",
                columns: new[] { "Id", "CityId", "CreateAt", "DealerAddress", "DealerMobile", "DealerName", "DealerNo", "DealerPhone", "DealerPrePhone", "DealerType", "Description", "EconomicCode", "Email", "Fax", "IsRemoved", "LastModifiedAt", "Latitude", "Longitude", "ManagerName", "NationalId", "PostalCode", "RemovedAt", "Sort" },
                values: new object[] { 27, 4, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "اصفهان، خیابان امام خمینی، خیابان مشیرالدوله شرقی، خیابان مهارت", null, "حموله", "710", "33853035", "031", 3, null, null, null, null, false, null, null, null, null, null, null, null, 27 });

            migrationBuilder.CreateIndex(
                name: "IX_Cities_ProvinceId_Title",
                table: "Cities",
                columns: new[] { "ProvinceId", "Title" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Dealers_CityId",
                table: "Dealers",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Dealers_DealerNo",
                table: "Dealers",
                column: "DealerNo",
                unique: true,
                filter: "[DealerNo] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Provinces_Title",
                table: "Provinces",
                column: "Title",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Provinces_ProvinceId",
                table: "Cities",
                column: "ProvinceId",
                principalTable: "Provinces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Provinces_ProvinceId",
                table: "Cities");

            migrationBuilder.DropTable(
                name: "Dealers");

            migrationBuilder.DropTable(
                name: "Provinces");

            migrationBuilder.DropIndex(
                name: "IX_Cities_ProvinceId_Title",
                table: "Cities");

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 75);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 76);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 77);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 78);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 79);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 80);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 81);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 82);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 83);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 84);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 85);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 86);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 87);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 88);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 89);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 90);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 91);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 92);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 93);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 94);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 95);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 96);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 97);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 98);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 99);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 100);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 104);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 105);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 106);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 107);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 108);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 109);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 110);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 111);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 112);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 113);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 114);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 115);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 116);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 117);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 118);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 119);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 120);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 121);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 122);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 123);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 124);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 125);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 126);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 127);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 128);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 129);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 130);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 131);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 132);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 133);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 134);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 135);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 136);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 137);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 138);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 139);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 140);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 141);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 142);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 143);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 144);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 145);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 146);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 147);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 148);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 149);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 150);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 151);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 152);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 153);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 154);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 155);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 156);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 157);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 158);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 159);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 160);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 161);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 162);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 163);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 164);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 165);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 166);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 167);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 168);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 169);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 170);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 171);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 172);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 173);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 174);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 175);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 176);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 177);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 178);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 179);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 180);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 181);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 182);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 183);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 184);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 185);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 186);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 187);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 188);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 189);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 190);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 191);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 192);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 193);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 194);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 195);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 196);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 197);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 198);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 199);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 200);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 201);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 202);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 203);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 204);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 205);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 206);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 207);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 208);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 209);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 210);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 211);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 212);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 213);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 214);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 215);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 216);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 217);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 218);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 219);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 220);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 221);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 222);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 223);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 224);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 225);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 226);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 227);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 228);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 229);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 230);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 231);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 232);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 233);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 234);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 235);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 236);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 237);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 238);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 239);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 240);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 241);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 242);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 243);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 244);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 245);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 246);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 247);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 248);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 249);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 250);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 251);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 252);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 253);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 254);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 255);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 256);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 257);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 258);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 259);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 260);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 261);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 262);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 263);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 264);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 265);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 266);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 267);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 268);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 269);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 270);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 271);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 272);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 273);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 274);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 275);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 276);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 277);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 278);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 279);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 280);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 281);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 282);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 283);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 284);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 285);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 286);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 287);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 288);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 289);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 290);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 291);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 292);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 293);

            migrationBuilder.DropColumn(
                name: "HashPassword",
                table: "Subscribers");

            migrationBuilder.DropColumn(
                name: "ProvinceId",
                table: "Cities");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "VehicleModels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Cities",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);
        }
    }
}
