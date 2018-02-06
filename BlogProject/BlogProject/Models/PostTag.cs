using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogProject.Models
{
    public class PostTag
    {
        [Required]
        public int Id { get; set; }
        public virtual Post Post { get; set; }
        public virtual Tag Tag { get; set; }
    }
}