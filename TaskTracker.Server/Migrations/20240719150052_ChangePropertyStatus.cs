using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskTracker.Server.Migrations
{
    /// <inheritdoc />
    public partial class ChangePropertyStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "ToDoItems",
                newName: "State");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "State",
                table: "ToDoItems",
                newName: "Status");
        }
    }
}
