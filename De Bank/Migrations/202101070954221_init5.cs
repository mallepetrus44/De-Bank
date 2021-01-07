namespace De_Bank.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init5 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TransactionAccounts", "Transaction_Id", "dbo.Transactions");
            DropForeignKey("dbo.TransactionAccounts", "Account_Id", "dbo.Accounts");
            DropForeignKey("dbo.AccountHolders", "Bank_Id", "dbo.Banks");
            DropForeignKey("dbo.Accounts", "Bank_Id", "dbo.Banks");
            DropForeignKey("dbo.Transactions", "Bank_Id", "dbo.Banks");
            DropIndex("dbo.AccountHolders", new[] { "Bank_Id" });
            DropIndex("dbo.Accounts", new[] { "Bank_Id" });
            DropIndex("dbo.Transactions", new[] { "Bank_Id" });
            DropIndex("dbo.TransactionAccounts", new[] { "Transaction_Id" });
            DropIndex("dbo.TransactionAccounts", new[] { "Account_Id" });
            AddColumn("dbo.Accounts", "AccountMinimum", c => c.Double(nullable: false));
            AddColumn("dbo.Transactions", "AmountAccount1Before", c => c.Double(nullable: false));
            AddColumn("dbo.Transactions", "AmountAccount1After", c => c.Double(nullable: false));
            AddColumn("dbo.Transactions", "AmountAccount2Before", c => c.Double(nullable: false));
            AddColumn("dbo.Transactions", "AmountAccount2After", c => c.Double(nullable: false));
            AddColumn("dbo.Transactions", "TransactionId", c => c.Int(nullable: false));
            AddColumn("dbo.Transactions", "AutoTransaction", c => c.Boolean(nullable: false));
            AddColumn("dbo.Transactions", "AutoTransactionFrequentyDays", c => c.Int(nullable: false));
            AddColumn("dbo.Transactions", "Account1_Id", c => c.Int());
            AddColumn("dbo.Transactions", "Account2_Id", c => c.Int());
            AddColumn("dbo.Transactions", "Account_Id", c => c.Int());
            AlterColumn("dbo.Transactions", "TransactionDate", c => c.DateTime(nullable: false));
            CreateIndex("dbo.Transactions", "Account1_Id");
            CreateIndex("dbo.Transactions", "Account2_Id");
            CreateIndex("dbo.Transactions", "Account_Id");
            AddForeignKey("dbo.Transactions", "Account1_Id", "dbo.Accounts", "Id");
            AddForeignKey("dbo.Transactions", "Account2_Id", "dbo.Accounts", "Id");
            AddForeignKey("dbo.Transactions", "Account_Id", "dbo.Accounts", "Id");
            DropColumn("dbo.AccountHolders", "Bank_Id");
            DropColumn("dbo.Accounts", "Bank_Id");
            DropColumn("dbo.Transactions", "Bank_Id");
            DropTable("dbo.Banks");
            DropTable("dbo.TransactionAccounts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TransactionAccounts",
                c => new
                    {
                        Transaction_Id = c.Int(nullable: false),
                        Account_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Transaction_Id, t.Account_Id });
            
            CreateTable(
                "dbo.Banks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Transactions", "Bank_Id", c => c.Int());
            AddColumn("dbo.Accounts", "Bank_Id", c => c.Int());
            AddColumn("dbo.AccountHolders", "Bank_Id", c => c.Int());
            DropForeignKey("dbo.Transactions", "Account_Id", "dbo.Accounts");
            DropForeignKey("dbo.Transactions", "Account2_Id", "dbo.Accounts");
            DropForeignKey("dbo.Transactions", "Account1_Id", "dbo.Accounts");
            DropIndex("dbo.Transactions", new[] { "Account_Id" });
            DropIndex("dbo.Transactions", new[] { "Account2_Id" });
            DropIndex("dbo.Transactions", new[] { "Account1_Id" });
            AlterColumn("dbo.Transactions", "TransactionDate", c => c.String());
            DropColumn("dbo.Transactions", "Account_Id");
            DropColumn("dbo.Transactions", "Account2_Id");
            DropColumn("dbo.Transactions", "Account1_Id");
            DropColumn("dbo.Transactions", "AutoTransactionFrequentyDays");
            DropColumn("dbo.Transactions", "AutoTransaction");
            DropColumn("dbo.Transactions", "TransactionId");
            DropColumn("dbo.Transactions", "AmountAccount2After");
            DropColumn("dbo.Transactions", "AmountAccount2Before");
            DropColumn("dbo.Transactions", "AmountAccount1After");
            DropColumn("dbo.Transactions", "AmountAccount1Before");
            DropColumn("dbo.Accounts", "AccountMinimum");
            CreateIndex("dbo.TransactionAccounts", "Account_Id");
            CreateIndex("dbo.TransactionAccounts", "Transaction_Id");
            CreateIndex("dbo.Transactions", "Bank_Id");
            CreateIndex("dbo.Accounts", "Bank_Id");
            CreateIndex("dbo.AccountHolders", "Bank_Id");
            AddForeignKey("dbo.Transactions", "Bank_Id", "dbo.Banks", "Id");
            AddForeignKey("dbo.Accounts", "Bank_Id", "dbo.Banks", "Id");
            AddForeignKey("dbo.AccountHolders", "Bank_Id", "dbo.Banks", "Id");
            AddForeignKey("dbo.TransactionAccounts", "Account_Id", "dbo.Accounts", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TransactionAccounts", "Transaction_Id", "dbo.Transactions", "Id", cascadeDelete: true);
        }
    }
}
