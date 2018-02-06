namespace BlogProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userRequirements : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "Firstname", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Users", "Lastname", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Lastname", c => c.String(maxLength: 50));
            AlterColumn("dbo.Users", "Firstname", c => c.String(maxLength: 50));
        }
    }
}
