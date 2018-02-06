namespace BlogProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Inital : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Blogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 50),
                        Body = c.String(maxLength: 200),
                        Created = c.DateTime(nullable: false),
                        DbUser_id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.DbUser_id)
                .Index(t => t.DbUser_id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Firstname = c.String(maxLength: 50),
                        Lastname = c.String(maxLength: 50),
                        Email = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false, maxLength: 100),
                        HashCode = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Body = c.String(maxLength: 250),
                        Created = c.DateTime(nullable: false),
                        Post_Id = c.Int(),
                        User_id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Posts", t => t.Post_Id)
                .ForeignKey("dbo.Users", t => t.User_id)
                .Index(t => t.Post_Id)
                .Index(t => t.User_id);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 50),
                        Body = c.String(maxLength: 200),
                        Created = c.DateTime(nullable: false),
                        Views = c.Int(nullable: false),
                        Blog_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Blogs", t => t.Blog_Id)
                .Index(t => t.Blog_Id);
            
            CreateTable(
                "dbo.PostTags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Post_Id = c.Int(),
                        Tag_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Posts", t => t.Post_Id)
                .ForeignKey("dbo.Tags", t => t.Tag_Id)
                .Index(t => t.Post_Id)
                .Index(t => t.Tag_Id);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "User_id", "dbo.Users");
            DropForeignKey("dbo.Comments", "Post_Id", "dbo.Posts");
            DropForeignKey("dbo.PostTags", "Tag_Id", "dbo.Tags");
            DropForeignKey("dbo.PostTags", "Post_Id", "dbo.Posts");
            DropForeignKey("dbo.Posts", "Blog_Id", "dbo.Blogs");
            DropForeignKey("dbo.Blogs", "DbUser_id", "dbo.Users");
            DropIndex("dbo.PostTags", new[] { "Tag_Id" });
            DropIndex("dbo.PostTags", new[] { "Post_Id" });
            DropIndex("dbo.Posts", new[] { "Blog_Id" });
            DropIndex("dbo.Comments", new[] { "User_id" });
            DropIndex("dbo.Comments", new[] { "Post_Id" });
            DropIndex("dbo.Blogs", new[] { "DbUser_id" });
            DropTable("dbo.Tags");
            DropTable("dbo.PostTags");
            DropTable("dbo.Posts");
            DropTable("dbo.Comments");
            DropTable("dbo.Users");
            DropTable("dbo.Blogs");
        }
    }
}
