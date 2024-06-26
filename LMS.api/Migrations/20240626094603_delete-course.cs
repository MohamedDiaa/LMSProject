using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS.api.Migrations
{
    /// <inheritdoc />
    public partial class deletecourse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Course_CourseID",
                table: "User");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Course_CourseID",
                table: "User",
                column: "CourseID",
                principalTable: "Course",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Course_CourseID",
                table: "User");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Course_CourseID",
                table: "User",
                column: "CourseID",
                principalTable: "Course",
                principalColumn: "Id");
        }
    }
}
