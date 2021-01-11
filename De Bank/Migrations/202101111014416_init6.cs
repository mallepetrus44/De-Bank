namespace De_Bank.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init6 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Transactions", name: "Account1_Id", newName: "AccountFrom_Id");
            RenameColumn(table: "dbo.Transactions", name: "Account2_Id", newName: "AccountTo_Id");
            RenameIndex(table: "dbo.Transactions", name: "IX_Account1_Id", newName: "IX_AccountFrom_Id");
            RenameIndex(table: "dbo.Transactions", name: "IX_Account2_Id", newName: "IX_AccountTo_Id");
            AddColumn("dbo.Accounts", "AccountLock", c => c.Boolean(nullable: false));
            AddColumn("dbo.Transactions", "PeriodicPayment", c => c.Boolean(nullable: false));
            DropColumn("dbo.Transactions", "AmountAccount1Before");
            DropColumn("dbo.Transactions", "AmountAccount1After");
            DropColumn("dbo.Transactions", "AmountAccount2Before");
            DropColumn("dbo.Transactions", "AmountAccount2After");
            DropColumn("dbo.Transactions", "TransactionId");
            DropColumn("dbo.Transactions", "AutoTransaction");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Transactions", "AutoTransaction", c => c.Boolean(nullable: false));
            AddColumn("dbo.Transactions", "TransactionId", c => c.Int(nullable: false));
            AddColumn("dbo.Transactions", "AmountAccount2After", c => c.Double(nullable: false));
            AddColumn("dbo.Transactions", "AmountAccount2Before", c => c.Double(nullable: false));
            AddColumn("dbo.Transactions", "AmountAccount1After", c => c.Double(nullable: false));
            AddColumn("dbo.Transactions", "AmountAccount1Before", c => c.Double(nullable: false));
            DropColumn("dbo.Transactions", "PeriodicPayment");
            DropColumn("dbo.Accounts", "AccountLock");
            RenameIndex(table: "dbo.Transactions", name: "IX_AccountTo_Id", newName: "IX_Account2_Id");
            RenameIndex(table: "dbo.Transactions", name: "IX_AccountFrom_Id", newName: "IX_Account1_Id");
            RenameColumn(table: "dbo.Transactions", name: "AccountTo_Id", newName: "Account2_Id");
            RenameColumn(table: "dbo.Transactions", name: "AccountFrom_Id", newName: "Account1_Id");
        }
    }
}
