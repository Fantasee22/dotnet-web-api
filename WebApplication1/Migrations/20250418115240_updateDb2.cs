using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    public partial class updateDb2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EMPLOYEE_UPLOAD_FILE_UPLOAD_FILE_ID",
                table: "EMPLOYEE");

            migrationBuilder.AlterColumn<long>(
                name: "UPLOAD_FILE_ID",
                table: "EMPLOYEE",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OFFICE_BRANCH_CODE",
                table: "OFFICE_BRANCH",
                column: "CODE",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EMPLOYEE_POSITION_CODE",
                table: "EMPLOYEE_POSITION",
                column: "CODE",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EMPLOYEE_CODE",
                table: "EMPLOYEE",
                column: "CODE",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EMPLOYEE_UPLOAD_FILE_UPLOAD_FILE_ID",
                table: "EMPLOYEE",
                column: "UPLOAD_FILE_ID",
                principalTable: "UPLOAD_FILE",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EMPLOYEE_UPLOAD_FILE_UPLOAD_FILE_ID",
                table: "EMPLOYEE");

            migrationBuilder.DropIndex(
                name: "IX_OFFICE_BRANCH_CODE",
                table: "OFFICE_BRANCH");

            migrationBuilder.DropIndex(
                name: "IX_EMPLOYEE_POSITION_CODE",
                table: "EMPLOYEE_POSITION");

            migrationBuilder.DropIndex(
                name: "IX_EMPLOYEE_CODE",
                table: "EMPLOYEE");

            migrationBuilder.AlterColumn<long>(
                name: "UPLOAD_FILE_ID",
                table: "EMPLOYEE",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_EMPLOYEE_UPLOAD_FILE_UPLOAD_FILE_ID",
                table: "EMPLOYEE",
                column: "UPLOAD_FILE_ID",
                principalTable: "UPLOAD_FILE",
                principalColumn: "ID");
        }
    }
}
