using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BikeRental.Infrastructure.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Models",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    WheelSize = table.Column<double>(type: "float", nullable: false),
                    MaxWeight = table.Column<double>(type: "float", nullable: false),
                    Weight = table.Column<double>(type: "float", nullable: false),
                    Brakes = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    PricePerHour = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Models", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Renters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Renters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bicycles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SerialNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Color = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ModelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bicycles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bicycles_Models_ModelId",
                        column: x => x.ModelId,
                        principalTable: "Models",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Rentals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BicycleId = table.Column<int>(type: "int", nullable: false),
                    RenterId = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DurationHours = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rentals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rentals_Bicycles_BicycleId",
                        column: x => x.BicycleId,
                        principalTable: "Bicycles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rentals_Renters_RenterId",
                        column: x => x.RenterId,
                        principalTable: "Renters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Models",
                columns: new[] { "Id", "Brakes", "MaxWeight", "Name", "PricePerHour", "Type", "Weight", "WheelSize", "Year" },
                values: new object[,]
                {
                    { 1, 0, 120.0, "Stels Navigator", 150m, 0, 15.0, 26.0, 2022 },
                    { 2, 1, 100.0, "Cube Race", 250m, 3, 12.0, 28.0, 2021 },
                    { 3, 0, 110.0, "Merida Speed", 300m, 3, 13.0, 29.0, 2023 },
                    { 4, 1, 50.0, "Forward Junior", 100m, 4, 8.0, 20.0, 2020 },
                    { 5, 0, 130.0, "Trek FX", 200m, 0, 14.0, 27.5, 2022 },
                    { 6, 0, 120.0, "Giant Sport", 280m, 3, 14.0, 29.0, 2023 },
                    { 7, 0, 110.0, "Stark Cobra", 220m, 1, 13.0, 27.5, 2022 },
                    { 8, 1, 100.0, "Scott City", 180m, 0, 12.0, 28.0, 2023 },
                    { 9, 0, 115.0, "Author MTB", 260m, 1, 14.0, 29.0, 2024 },
                    { 10, 0, 100.0, "Orbea Aero", 320m, 3, 11.0, 28.0, 2023 }
                });

            migrationBuilder.InsertData(
                table: "Renters",
                columns: new[] { "Id", "FullName", "Phone" },
                values: new object[,]
                {
                    { 1, "John Smith", "+1-202-111-22-33" },
                    { 2, "Mary Johnson", "+1-202-222-33-44" },
                    { 3, "Robert Brown", "+1-202-333-44-55" },
                    { 4, "Jennifer Davis", "+1-202-444-55-66" },
                    { 5, "Michael Miller", "+1-202-555-66-77" },
                    { 6, "William Wilson", "+1-202-666-77-88" },
                    { 7, "Elizabeth Moore", "+1-202-777-88-99" },
                    { 8, "David Taylor", "+1-202-888-99-00" },
                    { 9, "Linda Anderson", "+1-202-999-00-11" },
                    { 10, "James Thomas", "+1-202-000-11-22" }
                });

            migrationBuilder.InsertData(
                table: "Bicycles",
                columns: new[] { "Id", "Color", "ModelId", "SerialNumber" },
                values: new object[,]
                {
                    { 1, "Red", 1, "SN1001" },
                    { 2, "Blue", 2, "SN1002" },
                    { 3, "Green", 3, "SN1003" },
                    { 4, "Yellow", 4, "SN1004" },
                    { 5, "Black", 5, "SN1005" },
                    { 6, "Silver", 6, "SN1006" },
                    { 7, "White", 7, "SN1007" },
                    { 8, "Gray", 8, "SN1008" },
                    { 9, "Orange", 9, "SN1009" },
                    { 10, "Blue", 10, "SN1010" },
                    { 11, "Purple", 1, "SN1011" },
                    { 12, "Brown", 3, "SN1012" }
                });

            migrationBuilder.InsertData(
                table: "Rentals",
                columns: new[] { "Id", "BicycleId", "DurationHours", "RenterId", "StartTime" },
                values: new object[,]
                {
                    { 1, 1, 3, 1, new DateTime(2025, 12, 14, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 2, 2, 5, 2, new DateTime(2025, 12, 14, 14, 0, 0, 0, DateTimeKind.Utc) },
                    { 3, 3, 2, 3, new DateTime(2025, 12, 14, 16, 0, 0, 0, DateTimeKind.Utc) },
                    { 4, 4, 6, 4, new DateTime(2025, 12, 14, 18, 0, 0, 0, DateTimeKind.Utc) },
                    { 5, 5, 4, 5, new DateTime(2025, 12, 14, 20, 0, 0, 0, DateTimeKind.Utc) },
                    { 6, 6, 7, 6, new DateTime(2025, 12, 14, 22, 0, 0, 0, DateTimeKind.Utc) },
                    { 7, 7, 5, 7, new DateTime(2025, 12, 14, 23, 0, 0, 0, DateTimeKind.Utc) },
                    { 8, 8, 3, 8, new DateTime(2025, 12, 14, 21, 0, 0, 0, DateTimeKind.Utc) },
                    { 9, 9, 4, 9, new DateTime(2025, 12, 14, 19, 0, 0, 0, DateTimeKind.Utc) },
                    { 10, 10, 6, 10, new DateTime(2025, 12, 14, 17, 0, 0, 0, DateTimeKind.Utc) },
                    { 11, 2, 3, 3, new DateTime(2025, 12, 14, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { 12, 3, 4, 5, new DateTime(2025, 12, 14, 4, 0, 0, 0, DateTimeKind.Utc) },
                    { 13, 6, 2, 1, new DateTime(2025, 12, 13, 18, 0, 0, 0, DateTimeKind.Utc) },
                    { 14, 8, 5, 4, new DateTime(2025, 12, 13, 23, 0, 0, 0, DateTimeKind.Utc) },
                    { 15, 10, 3, 7, new DateTime(2025, 12, 12, 22, 0, 0, 0, DateTimeKind.Utc) },
                    { 16, 1, 2, 2, new DateTime(2025, 12, 13, 13, 0, 0, 0, DateTimeKind.Utc) },
                    { 17, 5, 6, 10, new DateTime(2025, 12, 13, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { 18, 4, 7, 6, new DateTime(2025, 12, 14, 6, 0, 0, 0, DateTimeKind.Utc) },
                    { 19, 9, 5, 9, new DateTime(2025, 12, 13, 20, 0, 0, 0, DateTimeKind.Utc) },
                    { 20, 7, 3, 3, new DateTime(2025, 12, 14, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { 21, 6, 6, 1, new DateTime(2025, 12, 11, 20, 0, 0, 0, DateTimeKind.Utc) },
                    { 22, 3, 4, 5, new DateTime(2025, 12, 12, 22, 0, 0, 0, DateTimeKind.Utc) },
                    { 23, 8, 2, 8, new DateTime(2025, 12, 13, 23, 0, 0, 0, DateTimeKind.Utc) },
                    { 24, 2, 5, 4, new DateTime(2025, 12, 13, 19, 0, 0, 0, DateTimeKind.Utc) },
                    { 25, 9, 7, 9, new DateTime(2025, 12, 12, 21, 0, 0, 0, DateTimeKind.Utc) },
                    { 26, 1, 2, 3, new DateTime(2025, 12, 10, 18, 0, 0, 0, DateTimeKind.Utc) },
                    { 27, 6, 5, 7, new DateTime(2025, 12, 9, 21, 0, 0, 0, DateTimeKind.Utc) },
                    { 28, 10, 3, 5, new DateTime(2025, 12, 8, 22, 0, 0, 0, DateTimeKind.Utc) },
                    { 29, 3, 4, 10, new DateTime(2025, 12, 7, 23, 0, 0, 0, DateTimeKind.Utc) },
                    { 30, 7, 6, 6, new DateTime(2025, 12, 6, 22, 0, 0, 0, DateTimeKind.Utc) },
                    { 31, 6, 5, 4, new DateTime(2025, 12, 4, 19, 0, 0, 0, DateTimeKind.Utc) },
                    { 32, 10, 4, 1, new DateTime(2025, 12, 2, 21, 0, 0, 0, DateTimeKind.Utc) },
                    { 33, 5, 7, 9, new DateTime(2025, 11, 29, 18, 0, 0, 0, DateTimeKind.Utc) },
                    { 34, 7, 5, 8, new DateTime(2025, 11, 26, 20, 0, 0, 0, DateTimeKind.Utc) },
                    { 35, 3, 3, 2, new DateTime(2025, 11, 24, 21, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bicycles_ModelId",
                table: "Bicycles",
                column: "ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_BicycleId",
                table: "Rentals",
                column: "BicycleId");

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_RenterId",
                table: "Rentals",
                column: "RenterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rentals");

            migrationBuilder.DropTable(
                name: "Bicycles");

            migrationBuilder.DropTable(
                name: "Renters");

            migrationBuilder.DropTable(
                name: "Models");
        }
    }
}
