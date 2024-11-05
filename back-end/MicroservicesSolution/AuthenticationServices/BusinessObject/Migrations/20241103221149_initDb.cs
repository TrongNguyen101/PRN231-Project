using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObject.Migrations
{
    /// <inheritdoc />
    public partial class initDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Major",
                columns: table => new
                {
                    MajorID = table.Column<int>(name: "Major ID", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MajorName = table.Column<string>(name: "Major Name", type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Major", x => x.MajorID);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    RoleID = table.Column<int>(name: "Role ID", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(name: "Role Name", type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    AccountID = table.Column<int>(name: "Account ID", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleID = table.Column<int>(name: "Role ID", type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.AccountID);
                    table.ForeignKey(
                        name: "FK_Account_Role_Role ID",
                        column: x => x.RoleID,
                        principalTable: "Role",
                        principalColumn: "Role ID");
                });

            migrationBuilder.CreateTable(
                name: "Profile",
                columns: table => new
                {
                    ProfileID = table.Column<int>(name: "Profile ID", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountID = table.Column<int>(name: "Account ID", type: "int", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(name: "First Name", type: "nvarchar(max)", nullable: true),
                    MiddelName = table.Column<string>(name: "Middel Name", type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(name: "Last Name", type: "nvarchar(max)", nullable: true),
                    GenderID = table.Column<int>(name: "Gender ID", type: "int", nullable: false),
                    Birthday = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MajorID = table.Column<int>(name: "Major ID", type: "int", nullable: false),
                    CreatedAt = table.Column<string>(name: "Created At", type: "nvarchar(max)", nullable: false),
                    LastModifiedAt = table.Column<string>(name: "Last Modified At", type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profile", x => x.ProfileID);
                    table.ForeignKey(
                        name: "FK_Profile_Account_Account ID",
                        column: x => x.AccountID,
                        principalTable: "Account",
                        principalColumn: "Account ID");
                    table.ForeignKey(
                        name: "FK_Profile_Major_Major ID",
                        column: x => x.MajorID,
                        principalTable: "Major",
                        principalColumn: "Major ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Account_Role ID",
                table: "Account",
                column: "Role ID");

            migrationBuilder.CreateIndex(
                name: "IX_Profile_Account ID",
                table: "Profile",
                column: "Account ID",
                unique: true,
                filter: "[Account ID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Profile_Major ID",
                table: "Profile",
                column: "Major ID",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Profile");

            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "Major");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
