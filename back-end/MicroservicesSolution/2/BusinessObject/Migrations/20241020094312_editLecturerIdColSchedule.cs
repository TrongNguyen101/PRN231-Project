using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScheduleBusinessObject.Migrations
{
    /// <inheritdoc />
    public partial class editLecturerIdColSchedule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Class Id",
                schema: "dbo",
                table: "Schedules",
                newName: "LecturerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LecturerId",
                schema: "dbo",
                table: "Schedules",
                newName: "Class Id");
        }
    }
}
