using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TManagement.Migrations
{
    /// <inheritdoc />
    public partial class AddTaskUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrentStatus",
                table: "Tasks",
                type: "int",
                nullable: false,
                defaultValueSql: "0");

            migrationBuilder.CreateTable(
                name: "ETaskUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaskId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    AssignDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ETaskUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ETaskUsers_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ETaskUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

           
            migrationBuilder.CreateIndex(
                name: "IX_ETaskUsers_TaskId",
                table: "ETaskUsers",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_ETaskUsers_UserId",
                table: "ETaskUsers",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ETaskUsers");

            migrationBuilder.DropColumn(
                name: "CurrentStatus",
                table: "Tasks");

         
        }
    }
}
