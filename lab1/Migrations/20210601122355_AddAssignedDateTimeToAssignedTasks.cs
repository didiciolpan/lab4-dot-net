using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace lab1.Migrations
{
    public partial class AddAssignedDateTimeToAssignedTasks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AssignedDateTime",
                table: "AssignedTasks",
                type: "datetime2",
                nullable: false,
                defaultValue: DateTime.UtcNow);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssignedDateTime",
                table: "AssignedTasks");
        }
    }
}
