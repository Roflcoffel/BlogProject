namespace BlogProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new_role : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserRoles", "Role_Id", "dbo.Roles");
            DropForeignKey("dbo.UserRoles", "User_id", "dbo.Users");
            DropIndex("dbo.UserRoles", new[] { "Role_Id" });
            DropIndex("dbo.UserRoles", new[] { "User_id" });
            DropTable("dbo.UserRoles");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Role_Id = c.Int(),
                        User_id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.UserRoles", "User_id");
            CreateIndex("dbo.UserRoles", "Role_Id");
            AddForeignKey("dbo.UserRoles", "User_id", "dbo.Users", "id");
            AddForeignKey("dbo.UserRoles", "Role_Id", "dbo.Roles", "Id");
        }
    }
}
