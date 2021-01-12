using Microsoft.EntityFrameworkCore.Migrations;

namespace Bank.FrontEnd.Data.Migrations
{
    public partial class holdertoIdentityholder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_AspNetUsers_HolderId",
                schema: "Identity",
                table: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_HolderId",
                schema: "Identity",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "HolderId",
                schema: "Identity",
                table: "Accounts");

            migrationBuilder.AddColumn<string>(
                name: "IdentityHolderId",
                schema: "Identity",
                table: "Accounts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_IdentityHolderId",
                schema: "Identity",
                table: "Accounts",
                column: "IdentityHolderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_AspNetUsers_IdentityHolderId",
                schema: "Identity",
                table: "Accounts",
                column: "IdentityHolderId",
                principalSchema: "Identity",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_AspNetUsers_IdentityHolderId",
                schema: "Identity",
                table: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_IdentityHolderId",
                schema: "Identity",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "IdentityHolderId",
                schema: "Identity",
                table: "Accounts");

            migrationBuilder.AddColumn<string>(
                name: "HolderId",
                schema: "Identity",
                table: "Accounts",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_HolderId",
                schema: "Identity",
                table: "Accounts",
                column: "HolderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_AspNetUsers_HolderId",
                schema: "Identity",
                table: "Accounts",
                column: "HolderId",
                principalSchema: "Identity",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
