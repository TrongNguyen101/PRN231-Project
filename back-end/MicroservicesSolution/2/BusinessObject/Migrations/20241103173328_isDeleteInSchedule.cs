using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScheduleBusinessObject.Migrations
{
    /// <inheritdoc />
    public partial class isDeleteInSchedule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Isdelete",
                schema: "dbo",
                table: "Schedules",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Isdelete",
                schema: "dbo",
                table: "Schedules");
        }
    }
}
