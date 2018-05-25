namespace DAB4_new.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Traderinfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TraderInfoes", "sellerId", c => c.Int(nullable: false));
            AddColumn("dbo.TraderInfoes", "buyerId", c => c.Int(nullable: false));
            AddColumn("dbo.TraderInfoes", "amount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TraderInfoes", "amount");
            DropColumn("dbo.TraderInfoes", "buyerId");
            DropColumn("dbo.TraderInfoes", "sellerId");
        }
    }
}
