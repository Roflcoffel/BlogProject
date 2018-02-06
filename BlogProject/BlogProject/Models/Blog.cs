using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using BlogProject.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogProject.Models
{
    public class Blog
    {   [Required]
        public int Id { get; set; }
        [StringLength(50)]
        public string Title { get; set; }

        [Display(Name = "Description")]
        [StringLength(200)]
        public string Body { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Created { get; set; }

        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Post> Posts { get; set; }

        public int BlogViews()
        {
            int views=Posts.Sum(x => x.Views);
            return views;
        }

        public List<Tag> BlogTags()
        {
            var postTag = Posts.SelectMany(x => x.PostTags).ToList();
            var tags = postTag.Select(x => x.Tag).Distinct().ToList();
            return tags;
        }

        public string BlogTagString()
        {
            string blogTag = "";
            List<Tag> tags = BlogTags();
            foreach (var item in tags)
            {
                blogTag += item.Name + ",";
            }

            return blogTag;
        }

        public List<IGrouping<int,Post>> Sort(){
            var OrderedPosts = Posts.OrderByDescending(p => p.Created).ToList();
            var GroupedPosts = OrderedPosts.GroupBy(x => x.Created.Year ).ToList();
            return GroupedPosts;
        }
    }
}