﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BlogProject.Models
{
    public class Tag
    {   [Required]
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; }

        [NotMapped]
        public bool Check { get; set; }

        public virtual ICollection<PostTag> PostTags { get; set; }
    }
}