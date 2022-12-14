using Microsoft.EntityFrameworkCore.Migrations;

namespace LMS_WEB_API_CF_12Dec.Migrations
{
    public partial class EmployeeId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leaves_Employees_EmployeeId",
                table: "Leaves");

            migrationBuilder.DropIndex(
                name: "IX_Leaves_EmployeeId",
                table: "Leaves");

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeId",
                table: "Leaves",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId1",
                table: "Leaves",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Leaves_EmployeeId1",
                table: "Leaves",
                column: "EmployeeId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Leaves_Employees_EmployeeId1",
                table: "Leaves",
                column: "EmployeeId1",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leaves_Employees_EmployeeId1",
                table: "Leaves");

            migrationBuilder.DropIndex(
                name: "IX_Leaves_EmployeeId1",
                table: "Leaves");

            migrationBuilder.DropColumn(
                name: "EmployeeId1",
                table: "Leaves");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "Leaves",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Leaves_EmployeeId",
                table: "Leaves",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Leaves_Employees_EmployeeId",
                table: "Leaves",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
