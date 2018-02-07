using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogProject.Models
{
    public class PostTagVM
    {
        public Post Post{ get; set; }
        public List<Tag> Tags { get; set; }

        public PostTagVM()
        {
            Post = new Post();
            Tags = new List<Tag>();
        }
    }
}