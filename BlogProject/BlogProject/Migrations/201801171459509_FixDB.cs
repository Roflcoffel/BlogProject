namespace BlogProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixDB : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Comments", new[] { "User_id" });
            CreateIndex("dbo.Comments", "user_id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Comments", new[] { "user_id" });
            CreateIndex("dbo.Comments", "User_id");
        }
    }
}
