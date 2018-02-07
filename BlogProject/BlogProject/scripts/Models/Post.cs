using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogProject.Models
{
    public class Post
    {   [Required]
        public int Id { get; set; }
        [StringLength(50)]
        public string Title { get; set; }
        [StringLength(200)]
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
    }
}