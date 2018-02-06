namespace BlogProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bodysize : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Blogs", "Body", c => c.String(maxLength: 200));
            AlterColumn("dbo.Posts", "Body", c => c.String(maxLength: 1000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Posts", "Body", c => c.String(maxLength: 200));
            AlterColumn("dbo.Blogs", "Body", c => c.String(maxLength: 800));
        }
    }
}
