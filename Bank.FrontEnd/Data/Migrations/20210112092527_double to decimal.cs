using Microsoft.EntityFrameworkCore.Migrations;

namespace Bank.FrontEnd.Data.Migrations
{
    public partial class doubletodecimal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "TransactionAmount",
                schema: "Identity",
                table: "Transactions",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<decimal>(
                name: "AccountMinimum",
                schema: "Identity",
                table: "Accounts",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<decimal>(
                name: "AccountBalance",
                schema: "Identity",
                table: "Accounts",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "TransactionAmount",
                schema: "Identity",
                table: "Transactions",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<double>(
                name: "AccountMinimum",
                schema: "Identity",
                table: "Accounts",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<double>(
                name: "AccountBalance",
                schema: "Identity",
                table: "Accounts",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal));
        }
    }
}
