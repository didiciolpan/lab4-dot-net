using Microsoft.EntityFrameworkCore.Migrations;

namespace lab1.Migrations
{
    public partial class AddAssignedTasks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AssignedTasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignedTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssignedTasks_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AssignedTasksTasks",
                columns: table => new
                {
                    AssignedTasksId = table.Column<int>(type: "int", nullable: false),
                    TasksId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignedTasksTasks", x => new { x.AssignedTasksId, x.TasksId });
                    table.ForeignKey(
                        name: "FK_AssignedTasksTasks_AssignedTasks_AssignedTasksId",
                        column: x => x.AssignedTasksId,
                        principalTable: "AssignedTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssignedTasksTasks_Tasks_TasksId",
                        column: x => x.TasksId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssignedTasks_ApplicationUserId",
                table: "AssignedTasks",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignedTasksTasks_TasksId",
                table: "AssignedTasksTasks",
                column: "TasksId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssignedTasksTasks");

            migrationBuilder.DropTable(
                name: "AssignedTasks");
        }
    }
}
