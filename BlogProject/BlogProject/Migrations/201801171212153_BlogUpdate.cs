namespace BlogProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BlogUpdate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Blogs", "Body", c => c.String(maxLength: 800));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Blogs", "Body", c => c.String(maxLength: 200));
        }
    }
}
