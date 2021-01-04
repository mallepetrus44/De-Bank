namespace De_Bank.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Banks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AccountHolders", "Bank_Id", c => c.Int());
            AddColumn("dbo.Accounts", "Bank_Id", c => c.Int());
            AddColumn("dbo.Transactions", "Bank_Id", c => c.Int());
            CreateIndex("dbo.AccountHolders", "Bank_Id");
            CreateIndex("dbo.Accounts", "Bank_Id");
            CreateIndex("dbo.Transactions", "Bank_Id");
            AddForeignKey("dbo.AccountHolders", "Bank_Id", "dbo.Banks", "Id");
            AddForeignKey("dbo.Accounts", "Bank_Id", "dbo.Banks", "Id");
            AddForeignKey("dbo.Transactions", "Bank_Id", "dbo.Banks", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transactions", "Bank_Id", "dbo.Banks");
            DropForeignKey("dbo.Accounts", "Bank_Id", "dbo.Banks");
            DropForeignKey("dbo.AccountHolders", "Bank_Id", "dbo.Banks");
            DropIndex("dbo.Transactions", new[] { "Bank_Id" });
            DropIndex("dbo.Accounts", new[] { "Bank_Id" });
            DropIndex("dbo.AccountHolders", new[] { "Bank_Id" });
            DropColumn("dbo.Transactions", "Bank_Id");
            DropColumn("dbo.Accounts", "Bank_Id");
            DropColumn("dbo.AccountHolders", "Bank_Id");
            DropTable("dbo.Banks");
        }
    }
}
