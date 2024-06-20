using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS.api.Migrations
{
    /// <inheritdoc />
    public partial class makecourseoptionalforuser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Course_CourseID",
                table: "User");

            migrationBuilder.AlterColumn<int>(
                name: "CourseID",
                table: "User",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Course_CourseID",
                table: "User",
                column: "CourseID",
                principalTable: "Course",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Course_CourseID",
                table: "User");

            migrationBuilder.AlterColumn<int>(
                name: "CourseID",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Course_CourseID",
                table: "User",
                column: "CourseID",
                principalTable: "Course",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
