using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BlogProject.Models;

namespace BlogProject.Models
{
    public class Search
    {
        public List<Blog> BlogsFound  { get; set; }
        public List<Post> PostsFound { get; set; }
        public  string FindString { get; set; }
        public string FindResult()
        {
            return BlogsFound.Count().ToString() + " blogs ," + PostsFound.Count().ToString() + "posts found matching : "+ FindString;
        }
         
        
    }
}