using Microsoft.EntityFrameworkCore.Migrations;

namespace Bank.FrontEnd.Migrations
{
    public partial class Holder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                schema: "Identity",
                table: "User");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                schema: "Identity",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
