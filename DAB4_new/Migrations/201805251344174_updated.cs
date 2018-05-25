namespace DAB4_new.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProsumerInfoes", "DifferencekW", c => c.Int(nullable: false));
            AlterColumn("dbo.ProsumerInfoes", "Type", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ProsumerInfoes", "Type", c => c.String(nullable: false));
            DropColumn("dbo.ProsumerInfoes", "DifferencekW");
        }
    }
}
