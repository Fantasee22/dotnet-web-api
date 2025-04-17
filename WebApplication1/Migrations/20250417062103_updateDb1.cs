using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    public partial class updateDb1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "UPLOAD_FILE_ID",
                table: "EMPLOYEE",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UPLOAD_FILE",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAME = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    STATUS = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BASE64_DATA = table.Column<string>(type: "nvarchar(3000)", maxLength: 3000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UPLOAD_FILE", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EMPLOYEE_UPLOAD_FILE_ID",
                table: "EMPLOYEE",
                column: "UPLOAD_FILE_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_EMPLOYEE_UPLOAD_FILE_UPLOAD_FILE_ID",
                table: "EMPLOYEE",
                column: "UPLOAD_FILE_ID",
                principalTable: "UPLOAD_FILE",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EMPLOYEE_UPLOAD_FILE_UPLOAD_FILE_ID",
                table: "EMPLOYEE");

            migrationBuilder.DropTable(
                name: "UPLOAD_FILE");

            migrationBuilder.DropIndex(
                name: "IX_EMPLOYEE_UPLOAD_FILE_ID",
                table: "EMPLOYEE");

            migrationBuilder.DropColumn(
                name: "UPLOAD_FILE_ID",
                table: "EMPLOYEE");
        }
    }
}
