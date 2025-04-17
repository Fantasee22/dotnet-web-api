using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    public partial class initDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EMPLOYEE_POSITION",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAME = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    CODE = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EMPLOYEE_POSITION", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "OFFICE_BRANCH",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAME = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    CODE = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ADDRESS = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OFFICE_BRANCH", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "EMPLOYEE",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAME = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    CODE = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IS_CONTRACT = table.Column<bool>(type: "bit", nullable: false),
                    START_DT = table.Column<DateTime>(type: "datetime2", nullable: true),
                    END_DT = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ADDRESS = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    PHONE = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EMPLOYEE_POSITION_ID = table.Column<long>(type: "bigint", nullable: false),
                    OFFICE_BRANCH_ID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EMPLOYEE", x => x.ID);
                    table.ForeignKey(
                        name: "FK_EMPLOYEE_EMPLOYEE_POSITION_EMPLOYEE_POSITION_ID",
                        column: x => x.EMPLOYEE_POSITION_ID,
                        principalTable: "EMPLOYEE_POSITION",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EMPLOYEE_OFFICE_BRANCH_OFFICE_BRANCH_ID",
                        column: x => x.OFFICE_BRANCH_ID,
                        principalTable: "OFFICE_BRANCH",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EMPLOYEE_EMPLOYEE_POSITION_ID",
                table: "EMPLOYEE",
                column: "EMPLOYEE_POSITION_ID");

            migrationBuilder.CreateIndex(
                name: "IX_EMPLOYEE_OFFICE_BRANCH_ID",
                table: "EMPLOYEE",
                column: "OFFICE_BRANCH_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EMPLOYEE");

            migrationBuilder.DropTable(
                name: "EMPLOYEE_POSITION");

            migrationBuilder.DropTable(
                name: "OFFICE_BRANCH");
        }
    }
}
