using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bank.FrontEnd.Migrations
{
    public partial class _10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Transactions",
                columns: new[] { "Id", "AccountFrom", "AccountId", "AccountTo", "CreationDate", "Frequenty", "IdentityHolderId", "IsPeriodic", "NextPayment", "PeriodicTransactionFrequentyDays", "Status", "TransactionAmount", "TransactionDate" },
                values: new object[] { 1, "Jaap", null, "Piet", new DateTime(2021, 1, 21, 13, 52, 34, 154, DateTimeKind.Local).AddTicks(4411), 0, null, false, new DateTime(2021, 1, 21, 13, 52, 34, 154, DateTimeKind.Local).AddTicks(6070), 0, 0, 50f, new DateTime(2021, 1, 21, 13, 52, 34, 151, DateTimeKind.Local).AddTicks(1841) });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Transactions",
                columns: new[] { "Id", "AccountFrom", "AccountId", "AccountTo", "CreationDate", "Frequenty", "IdentityHolderId", "IsPeriodic", "NextPayment", "PeriodicTransactionFrequentyDays", "Status", "TransactionAmount", "TransactionDate" },
                values: new object[] { 2, "Klaas", null, "Henk", new DateTime(2021, 1, 21, 14, 2, 34, 154, DateTimeKind.Local).AddTicks(6695), 0, null, false, new DateTime(2021, 1, 21, 14, 2, 34, 154, DateTimeKind.Local).AddTicks(6723), 0, 0, 75f, new DateTime(2021, 1, 21, 14, 2, 34, 154, DateTimeKind.Local).AddTicks(6652) });

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Transactions",
                columns: new[] { "Id", "AccountFrom", "AccountId", "AccountTo", "CreationDate", "Frequenty", "IdentityHolderId", "IsPeriodic", "NextPayment", "PeriodicTransactionFrequentyDays", "Status", "TransactionAmount", "TransactionDate" },
                values: new object[] { 3, "Joop", null, "Henny", new DateTime(2021, 1, 21, 14, 12, 34, 154, DateTimeKind.Local).AddTicks(6739), 0, null, false, new DateTime(2021, 1, 21, 14, 12, 34, 154, DateTimeKind.Local).AddTicks(6743), 0, 0, 100f, new DateTime(2021, 1, 21, 14, 12, 34, 154, DateTimeKind.Local).AddTicks(6735) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
