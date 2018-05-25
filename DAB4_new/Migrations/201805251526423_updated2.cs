namespace DAB4_new.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updated2 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.ProsumerInfoes");
            AlterColumn("dbo.ProsumerInfoes", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.ProsumerInfoes", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.ProsumerInfoes");
            AlterColumn("dbo.ProsumerInfoes", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.ProsumerInfoes", "Id");
        }
    }
}
