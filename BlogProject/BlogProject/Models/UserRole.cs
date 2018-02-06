using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogProject.Models
{
    public class UserRole
    {
        [Required]
        public int Id { get; set; }
        
        public virtual User  User { get; set; }
        public virtual Role Role { get; set; }
    }
}