using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TManagement.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Lookups",
                columns: new[] { "Id", "FatherLookupId", "Name", "Type" },
                values: new object[,]
                {
                    { new Guid("15ed9a59-3c4b-411b-9b0f-7fdd66d41c07"), null, "Tawjihi", 21 },
                    { new Guid("277321a7-2360-49e2-9cbe-525342a6d693"), null, "Elemantary", 21 },
                    { new Guid("3c6f7ef2-819e-4b02-b914-12745e111743"), null, "Master and above", 21 },
                    { new Guid("cfe43cb8-7b8d-4955-bca1-491971508a75"), null, "Palestine", 1 },
                    { new Guid("cfe43cb8-7b8d-4955-bca1-491971508a76"), null, "Jordan", 1 },
                    { new Guid("cfe43cb8-7b8d-4955-bca1-491971508a79"), null, "BA/BS", 21 },
                    { new Guid("116ae3a3-71b1-4d39-bc89-7618afa622bb"), new Guid("cfe43cb8-7b8d-4955-bca1-491971508a75"), "Ramallah", 5 },
                    { new Guid("cfe43cb8-7b8d-4955-bca1-491971508a77"), new Guid("cfe43cb8-7b8d-4955-bca1-491971508a75"), "Jerusalem", 5 },
                    { new Guid("d64b206b-f63b-4eed-a8af-4eaa1cf9d5a7"), new Guid("cfe43cb8-7b8d-4955-bca1-491971508a76"), "Amman", 5 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("116ae3a3-71b1-4d39-bc89-7618afa622bb"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("15ed9a59-3c4b-411b-9b0f-7fdd66d41c07"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("277321a7-2360-49e2-9cbe-525342a6d693"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("3c6f7ef2-819e-4b02-b914-12745e111743"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("cfe43cb8-7b8d-4955-bca1-491971508a77"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("cfe43cb8-7b8d-4955-bca1-491971508a79"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("d64b206b-f63b-4eed-a8af-4eaa1cf9d5a7"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("cfe43cb8-7b8d-4955-bca1-491971508a75"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("cfe43cb8-7b8d-4955-bca1-491971508a76"));
        }
    }
}
