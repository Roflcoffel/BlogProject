namespace BlogProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserRole : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Roles", "user_id", "dbo.Users");
            DropIndex("dbo.Roles", new[] { "user_id" });
            AddColumn("dbo.Users", "Role_Id", c => c.Int());
            CreateIndex("dbo.Users", "Role_Id");
            AddForeignKey("dbo.Users", "Role_Id", "dbo.Roles", "Id");
            DropColumn("dbo.Roles", "user_id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Roles", "user_id", c => c.Int());
            DropForeignKey("dbo.Users", "Role_Id", "dbo.Roles");
            DropIndex("dbo.Users", new[] { "Role_Id" });
            DropColumn("dbo.Users", "Role_Id");
            CreateIndex("dbo.Roles", "user_id");
            AddForeignKey("dbo.Roles", "user_id", "dbo.Users", "id");
        }
    }
}
