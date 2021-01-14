using Microsoft.EntityFrameworkCore.Migrations;

namespace Bank.FrontEnd.Migrations
{
    public partial class dus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullName",
                schema: "Identity",
                table: "User");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullName",
                schema: "Identity",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
