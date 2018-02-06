namespace BlogProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stringsize : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Posts", "Body", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Posts", "Body", c => c.String(maxLength: 1000));
        }
    }
}
