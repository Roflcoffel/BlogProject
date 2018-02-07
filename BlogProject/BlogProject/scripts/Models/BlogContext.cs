using BlogProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BlogProject.Models
{
    public class BlogContext : DbContext
    {
        public BlogContext() : base("ServerConnection")
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostTag> PostTags { get; set; }
        public DbSet<Tag> Tags { get; set; }

        //var dbContext = new BlogContext();
        //var userStore = new UserStore<ApplicationDbUser>(dbContext);
        //var userManager = new UserManager<ApplicationDbUser>(userStore);

        //ApplicationDbUser user = new ApplicationDbUser()
        //{

        //}



    }
}