using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScheduleBusinessObject.Migrations
{
    /// <inheritdoc />
    public partial class initialdata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Classes",
                schema: "dbo",
                columns: table => new
                {
                    ClassId = table.Column<int>(name: "Class Id", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassName = table.Column<string>(name: "Class Name", type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.ClassId);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                schema: "dbo",
                columns: table => new
                {
                    RoomID = table.Column<int>(name: "Room ID", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomName = table.Column<string>(name: "Room Name", type: "nvarchar(max)", nullable: false),
                    RoomStatus = table.Column<bool>(name: "Room Status", type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.RoomID);
                });

            migrationBuilder.CreateTable(
                name: "subjects",
                schema: "dbo",
                columns: table => new
                {
                    SubjectID = table.Column<int>(name: "Subject ID", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubjectName = table.Column<string>(name: "Subject Name", type: "nvarchar(max)", nullable: false),
                    NumberOfSlot = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subjects", x => x.SubjectID);
                });

            migrationBuilder.CreateTable(
                name: "TimeSlots",
                schema: "dbo",
                columns: table => new
                {
                    timeSlotID = table.Column<int>(name: "timeSlot ID", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeStart = table.Column<TimeOnly>(name: "Time Start", type: "time", nullable: false),
                    TimeEnd = table.Column<TimeOnly>(name: "Time End", type: "time", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeSlots", x => x.timeSlotID);
                });

            migrationBuilder.CreateTable(
                name: "StudentInClass",
                columns: table => new
                {
                    StudentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClassId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentInClass", x => new { x.StudentId, x.ClassId });
                    table.ForeignKey(
                        name: "FK_StudentInClass_Classes_ClassId",
                        column: x => x.ClassId,
                        principalSchema: "dbo",
                        principalTable: "Classes",
                        principalColumn: "Class Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
                schema: "dbo",
                columns: table => new
                {
                    ScheduleId = table.Column<int>(name: "Schedule Id", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassId = table.Column<int>(type: "int", nullable: false),
                    ClassId0 = table.Column<string>(name: "Class Id", type: "nvarchar(max)", nullable: false),
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    TimeSlotId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.ScheduleId);
                    table.ForeignKey(
                        name: "FK_Schedules_Classes_ClassId",
                        column: x => x.ClassId,
                        principalSchema: "dbo",
                        principalTable: "Classes",
                        principalColumn: "Class Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Schedules_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalSchema: "dbo",
                        principalTable: "Rooms",
                        principalColumn: "Room ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Schedules_TimeSlots_TimeSlotId",
                        column: x => x.TimeSlotId,
                        principalSchema: "dbo",
                        principalTable: "TimeSlots",
                        principalColumn: "timeSlot ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Schedules_subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalSchema: "dbo",
                        principalTable: "subjects",
                        principalColumn: "Subject ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_ClassId",
                schema: "dbo",
                table: "Schedules",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_RoomId",
                schema: "dbo",
                table: "Schedules",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_SubjectId",
                schema: "dbo",
                table: "Schedules",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_TimeSlotId",
                schema: "dbo",
                table: "Schedules",
                column: "TimeSlotId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentInClass_ClassId",
                table: "StudentInClass",
                column: "ClassId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Schedules",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "StudentInClass");

            migrationBuilder.DropTable(
                name: "Rooms",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "TimeSlots",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "subjects",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Classes",
                schema: "dbo");
        }
    }
}
