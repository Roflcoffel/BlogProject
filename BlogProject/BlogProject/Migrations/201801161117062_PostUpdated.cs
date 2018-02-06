namespace BlogProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PostUpdated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "LastChanged", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "LastChanged");
        }
    }
}
