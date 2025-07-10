using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class handleEmployeelogRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Logs_Employees_EmployeeId",
                schema: "Lookups",
                table: "Logs");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                schema: "Lookups",
                table: "Logs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Logs_Employees_EmployeeId",
                schema: "Lookups",
                table: "Logs",
                column: "EmployeeId",
                principalSchema: "HR",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Logs_Employees_EmployeeId",
                schema: "Lookups",
                table: "Logs");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                schema: "Lookups",
                table: "Logs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Logs_Employees_EmployeeId",
                schema: "Lookups",
                table: "Logs",
                column: "EmployeeId",
                principalSchema: "HR",
                principalTable: "Employees",
                principalColumn: "Id");
        }
    }
}
