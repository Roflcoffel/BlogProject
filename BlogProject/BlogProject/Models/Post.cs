using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace BlogProject.Models
{
    public class Post
    {
        [Required]
        public int Id { get; set; }

        [StringLength(50)]
        public string Title { get; set; }

        [StringLength(500000)]
        [AllowHtml]
        public string Body { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Created { get; set; }

        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime LastChanged { get; set; }
        
        public int Views { get; set; }
        public virtual Blog Blog { get; set; }
        public virtual ICollection<PostTag> PostTags { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }

        public Post GetFromDB(BlogContext db)
        {
            return db.Posts.Find(Id);
        }

        public string RemoveHTML()
        {
            return WebUtility.HtmlDecode(
                Regex.Replace(Body, "<[^>]*(>|$)", string.Empty)
            );
        }

        public List<Tag> GetTags()
        {
            var tags = PostTags.Select(x => x.Tag).Distinct().ToList();
            return tags;
        }

        public string PostTagString()
        {
            string PostTag = "";
            List<Tag> tags = GetTags();
            foreach (var item in tags)
            {
                PostTag += item.Name + ",";
            }

            return PostTag;
        }
    }
}