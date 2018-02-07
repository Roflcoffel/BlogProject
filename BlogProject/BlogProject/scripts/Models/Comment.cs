using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogProject.Models
{
    public class Comment
    {
        [Required]
        public int Id { get; set; }
        [StringLength(250)]
        public string Body { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime Created { get; set; }

        public virtual Post Post{get; set;}
        public virtual User user { get; set; }
    }
}