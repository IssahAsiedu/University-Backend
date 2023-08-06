using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversityRestApi.Migrations
{
    /// <inheritdoc />
    public partial class AddCourseNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CourseNumber",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourseNumber",
                table: "Courses");
        }
    }
}
