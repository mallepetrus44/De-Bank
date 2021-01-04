namespace De_Bank.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init3 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Transactions", "TransactionType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Transactions", "TransactionType", c => c.Boolean(nullable: false));
        }
    }
}
