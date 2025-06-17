using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace garbageDetetionApi.Migrations
{
    /// <inheritdoc />
    public partial class AddApiKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApiKeys",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiKeys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Garbages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DetectedObject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConfidenceScore = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CameraId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Longitude = table.Column<decimal>(type: "decimal(9,6)", nullable: false),
                    Latitude = table.Column<decimal>(type: "decimal(9,6)", nullable: false),
                    Weather = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Temp = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Humidity = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    WindSpeed = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Garbages", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Garbages",
                columns: new[] { "Id", "CameraId", "ConfidenceScore", "DetectedObject", "Humidity", "ImageName", "Latitude", "Longitude", "Temp", "TimeStamp", "Weather", "WindSpeed" },
                values: new object[,]
                {
                    { 1, new Guid("d3c1f8b2-4e5f-4a2b-9c3e-8f1b2c3d4e5f"), 0.95m, "Plastic Bottle", 45.0m, "plastic_bottle", 51.591415m, 4.778720m, 22.5m, new DateTime(2025, 5, 15, 10, 0, 0, 0, DateTimeKind.Unspecified), "Sunny", 5.2m },
                    { 2, new Guid("d3c1f8b2-4e5f-4a2b-9c3e-8f1b2c3d4e5f"), 0.89m, "Can", 55.0m, "can", 51.591415m, 4.778720m, 18.3m, new DateTime(2025, 5, 15, 11, 0, 0, 0, DateTimeKind.Unspecified), "Cloudy", 3.8m },
                    { 3, new Guid("d3c1f8b2-4e5f-4a2b-9c3e-8f1b2c3d4e5f"), 0.92m, "Paper", 80.0m, "paper", 51.591415m, 4.778720m, 16.0m, new DateTime(2025, 5, 15, 12, 0, 0, 0, DateTimeKind.Unspecified), "Rainy", 7.1m },
                    { 4, new Guid("d3c1f8b2-4e5f-4a2b-9c3e-8f1b2c3d4e5f"), 0.88m, "Glass", 50.0m, "glass", 51.591415m, 4.778720m, 20.0m, new DateTime(2025, 5, 15, 13, 0, 0, 0, DateTimeKind.Unspecified), "Windy", 12.0m },
                    { 5, new Guid("d3c1f8b2-4e5f-4a2b-9c3e-8f1b2c3d4e5f"), 0.90m, "Food Wrapper", 40.0m, "food_wrapper", 51.591415m, 4.778720m, 24.0m, new DateTime(2025, 5, 15, 14, 0, 0, 0, DateTimeKind.Unspecified), "Sunny", 4.5m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApiKeys");

            migrationBuilder.DropTable(
                name: "Garbages");
        }
    }
}
