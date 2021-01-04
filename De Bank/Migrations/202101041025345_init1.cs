namespace De_Bank.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccountHolders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AccountHolderName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AccountNumber = c.String(),
                        AccountBalance = c.Double(nullable: false),
                        AccountHolder_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AccountHolders", t => t.AccountHolder_Id)
                .Index(t => t.AccountHolder_Id);
            
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TransactionAmount = c.Double(nullable: false),
                        TransactionType = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TransactionAccounts",
                c => new
                    {
                        Transaction_Id = c.Int(nullable: false),
                        Account_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Transaction_Id, t.Account_Id })
                .ForeignKey("dbo.Transactions", t => t.Transaction_Id, cascadeDelete: true)
                .ForeignKey("dbo.Accounts", t => t.Account_Id, cascadeDelete: true)
                .Index(t => t.Transaction_Id)
                .Index(t => t.Account_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TransactionAccounts", "Account_Id", "dbo.Accounts");
            DropForeignKey("dbo.TransactionAccounts", "Transaction_Id", "dbo.Transactions");
            DropForeignKey("dbo.Accounts", "AccountHolder_Id", "dbo.AccountHolders");
            DropIndex("dbo.TransactionAccounts", new[] { "Account_Id" });
            DropIndex("dbo.TransactionAccounts", new[] { "Transaction_Id" });
            DropIndex("dbo.Accounts", new[] { "AccountHolder_Id" });
            DropTable("dbo.TransactionAccounts");
            DropTable("dbo.Transactions");
            DropTable("dbo.Accounts");
            DropTable("dbo.AccountHolders");
        }
    }
}
