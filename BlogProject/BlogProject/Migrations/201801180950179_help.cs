namespace BlogProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class help : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Blogs", name: "DbUser_id", newName: "User_id");
            RenameIndex(table: "dbo.Blogs", name: "IX_DbUser_id", newName: "IX_User_id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Blogs", name: "IX_User_id", newName: "IX_DbUser_id");
            RenameColumn(table: "dbo.Blogs", name: "User_id", newName: "DbUser_id");
        }
    }
}
