using Microsoft.EntityFrameworkCore.Migrations;

namespace Bank.FrontEnd.Migrations
{
    public partial class rommeldebommel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_User_IdentityUserId",
                schema: "Identity",
                table: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_IdentityUserId",
                schema: "Identity",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "IdentityUserId",
                schema: "Identity",
                table: "Accounts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdentityUserId",
                schema: "Identity",
                table: "Accounts",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_IdentityUserId",
                schema: "Identity",
                table: "Accounts",
                column: "IdentityUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_User_IdentityUserId",
                schema: "Identity",
                table: "Accounts",
                column: "IdentityUserId",
                principalSchema: "Identity",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
