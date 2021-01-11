namespace De_Bank.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init7 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Transactions", "Account_Id", "dbo.Accounts");
            DropIndex("dbo.Transactions", new[] { "Account_Id" });
            AddColumn("dbo.AccountHolders", "FirstName", c => c.String());
            AddColumn("dbo.AccountHolders", "MiddleName", c => c.String());
            AddColumn("dbo.AccountHolders", "LastName", c => c.String());
            DropColumn("dbo.AccountHolders", "AccountHolderName");
            DropColumn("dbo.Transactions", "Account_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Transactions", "Account_Id", c => c.Int());
            AddColumn("dbo.AccountHolders", "AccountHolderName", c => c.String());
            DropColumn("dbo.AccountHolders", "LastName");
            DropColumn("dbo.AccountHolders", "MiddleName");
            DropColumn("dbo.AccountHolders", "FirstName");
            CreateIndex("dbo.Transactions", "Account_Id");
            AddForeignKey("dbo.Transactions", "Account_Id", "dbo.Accounts", "Id");
        }
    }
}
