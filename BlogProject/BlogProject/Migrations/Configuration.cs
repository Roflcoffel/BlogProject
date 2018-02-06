namespace BlogProject.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BlogProject.Models.BlogContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(BlogContext db)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            //Even when though these use AddOrUpdate() duplicates are created!
            //User user1 = new User { Firstname = "Mikael", Lastname = "Svensson", Email = "mikael-svensson@hotmail.com", Password = "1", HashCode = "1", ImageUrl = "~/Content/images/user_default.jpg", RoleId = 2 };
            //User user2 = new User { Firstname = "Lars", Lastname = "Johansson", Email = "lars-johansson@gmail.com", Password = "1", HashCode = "1", ImageUrl = "~/Content/images/user_default.jpg", RoleId = 2 };

            //db.Users.AddOrUpdate(x => x.id, user1, user2);

            //Blog blog1 = new Blog { Body = "This is a blog about seeds", Title = "Seed Blog", Created = new DateTime(2014, 5, 4), User = user1, ImageUrl = "~/Content/images/3blog_logo.jpg" };
            //Blog blog2 = new Blog { Body = "a blog about MVC framework", Title = "MVC Framework", Created = new DateTime(2015, 5, 4), User = user2, ImageUrl = "~/Content/images/3blog_logo.jpg" };

            //db.Blogs.AddOrUpdate(x => x.Id, blog1, blog2);

            //Post post1 = new Post { Body = "text about seeds", Title = "Seeds", Blog = blog1, Created = new DateTime(2017, 5, 4), LastChanged = new DateTime(2017, 5, 4), ImageUrl= "~/Content/images/dasdsad.jpg" };
            //Post post2 = new Post { Body = "text about MVC", Title = "MVC", Blog = blog2, Created = new DateTime(2017, 6, 4), LastChanged = new DateTime(2017, 6, 4), ImageUrl= "~/Content/images/dasdsad.jpg" };

            //db.Posts.AddOrUpdate(x => x.Id, post1, post2);

            //Comment comment1 = new Comment { Body = "seeds är fina", Post = post1, Created = new DateTime(2015, 5, 4), user = user1 };
            //Comment comment2 = new Comment { Body = "seed comment", Post = post1, Created = new DateTime(2014, 5, 5), user = user2 };

            //db.Comments.AddOrUpdate(x => x.Id, comment1, comment2);

            //Tag tag1 = new Tag { Name = "Seed" };
            //Tag tag2 = new Tag { Name = "Odling" };
            //Tag tag3 = new Tag { Name = "C#" };
            //Tag tag4 = new Tag { Name = "MVC" };

            //db.Tags.AddOrUpdate(x => x.Id, tag1, tag2, tag3, tag4);

            //PostTag postTag1 = new PostTag { Post = post1, Tag = tag1 };
            //PostTag postTag2 = new PostTag { Post = post1, Tag = tag2 };
            //PostTag postTag3 = new PostTag { Post = post2, Tag = tag3 };
            //PostTag postTag4 = new PostTag { Post = post2, Tag = tag4 };

            //db.PostTags.AddOrUpdate(x => x.Id, postTag1, postTag2, postTag3, postTag4);
        }
    }
}
