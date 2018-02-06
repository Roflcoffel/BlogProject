namespace BlogProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImageUrls : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Blogs", "ImageUrl", c => c.String());
            AddColumn("dbo.Posts", "ImageUrl", c => c.String());
            AddColumn("dbo.Users", "ImageUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "ImageUrl");
            DropColumn("dbo.Posts", "ImageUrl");
            DropColumn("dbo.Blogs", "ImageUrl");
        }
    }
}
