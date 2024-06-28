using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS.api.Migrations
{
    /// <inheritdoc />
    public partial class init4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Starts",
                table: "Module",
                newName: "Start");

            migrationBuilder.RenameColumn(
                name: "Ends",
                table: "Module",
                newName: "End");

            migrationBuilder.RenameColumn(
                name: "Starts",
                table: "Course",
                newName: "Start");

            migrationBuilder.RenameColumn(
                name: "Ends",
                table: "Course",
                newName: "End");

            migrationBuilder.RenameColumn(
                name: "Starts",
                table: "Activity",
                newName: "Start");

            migrationBuilder.RenameColumn(
                name: "Ends",
                table: "Activity",
                newName: "End");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Start",
                table: "Module",
                newName: "Starts");

            migrationBuilder.RenameColumn(
                name: "End",
                table: "Module",
                newName: "Ends");

            migrationBuilder.RenameColumn(
                name: "Start",
                table: "Course",
                newName: "Starts");

            migrationBuilder.RenameColumn(
                name: "End",
                table: "Course",
                newName: "Ends");

            migrationBuilder.RenameColumn(
                name: "Start",
                table: "Activity",
                newName: "Starts");

            migrationBuilder.RenameColumn(
                name: "End",
                table: "Activity",
                newName: "Ends");
        }
    }
}
