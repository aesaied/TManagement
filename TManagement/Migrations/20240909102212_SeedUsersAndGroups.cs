using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TManagement.Migrations
{
    /// <inheritdoc />
    public partial class SeedUsersAndGroups : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                keyValue: new Guid("d64b206b-f63b-4eed-a8af-4eaa1cf9d5a7"));

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "ADMINS" },
                    { 2, "Secretary" },
                    { 3, "Reports" }
                });

            migrationBuilder.InsertData(
                table: "Lookups",
                columns: new[] { "Id", "FatherLookupId", "Name", "Type" },
                values: new object[,]
                {
                    { new Guid("29be72b1-3a33-4188-9fc7-0579adac9ffa"), null, "Tawjihi", 21 },
                    { new Guid("46baabeb-030b-49f1-b6e5-cd02e313956e"), new Guid("cfe43cb8-7b8d-4955-bca1-491971508a76"), "Amman", 5 },
                    { new Guid("8bf52a09-678c-470e-a8e9-ad95fe2c1d7f"), new Guid("cfe43cb8-7b8d-4955-bca1-491971508a75"), "Ramallah", 5 },
                    { new Guid("e17c10fd-e4c0-47cc-a3bc-c5ef3e229b4a"), null, "Elemantary", 21 },
                    { new Guid("f3e1ba09-f161-4f62-824a-8db683276850"), null, "Master and above", 21 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CityId", "CurrentStatus", "EducationLevelId", "Email", "FullName", "PasswordHash", "PasswordSalt" },
                values: new object[] { 1, new Guid("cfe43cb8-7b8d-4955-bca1-491971508a77"), 1, new Guid("cfe43cb8-7b8d-4955-bca1-491971508a79"), "Atallah.esaied@gmail.com", "System admin", "8F042D4A735B2AD0EB0474F2253F14D47CF81C37964BC64495EAFD83D4ECC8EB2F0643F987CA8CA1B527F130588DDE42A09B6DC02FCA1F764C299FFBF06B47F4", "AC3058FE0771233877CD439096950A7077EB79DCB32010B615AB0BD5EFF4CED02F50908EF7B48EE1B1412955AB1E95C29C2A57666D40F301690F725666DEF9EE" });

            migrationBuilder.InsertData(
                table: "UserGroups",
                columns: new[] { "Id", "GroupId", "UserId" },
                values: new object[] { new Guid("a7352666-5593-4cff-9443-6db0a1b9dcbc"), 1, 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("29be72b1-3a33-4188-9fc7-0579adac9ffa"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("46baabeb-030b-49f1-b6e5-cd02e313956e"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("8bf52a09-678c-470e-a8e9-ad95fe2c1d7f"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("e17c10fd-e4c0-47cc-a3bc-c5ef3e229b4a"));

            migrationBuilder.DeleteData(
                table: "Lookups",
                keyColumn: "Id",
                keyValue: new Guid("f3e1ba09-f161-4f62-824a-8db683276850"));

            migrationBuilder.DeleteData(
                table: "UserGroups",
                keyColumn: "Id",
                keyValue: new Guid("a7352666-5593-4cff-9443-6db0a1b9dcbc"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.InsertData(
                table: "Lookups",
                columns: new[] { "Id", "FatherLookupId", "Name", "Type" },
                values: new object[,]
                {
                    { new Guid("116ae3a3-71b1-4d39-bc89-7618afa622bb"), new Guid("cfe43cb8-7b8d-4955-bca1-491971508a75"), "Ramallah", 5 },
                    { new Guid("15ed9a59-3c4b-411b-9b0f-7fdd66d41c07"), null, "Tawjihi", 21 },
                    { new Guid("277321a7-2360-49e2-9cbe-525342a6d693"), null, "Elemantary", 21 },
                    { new Guid("3c6f7ef2-819e-4b02-b914-12745e111743"), null, "Master and above", 21 },
                    { new Guid("d64b206b-f63b-4eed-a8af-4eaa1cf9d5a7"), new Guid("cfe43cb8-7b8d-4955-bca1-491971508a76"), "Amman", 5 }
                });
        }
    }
}
