using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogProject.Models
{
    public class User {
        //DB Fields
        [Required]
        public int id { get; set; }

        [StringLength(50)]
        [Required]
        public string Firstname { get; set; }

        [StringLength(50)]
        [Required]
        public string Lastname { get; set; }

        [Required]
        [StringLength(50)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [StringLength(100)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }

        [Required]
        public int RoleId { get; set; }

        [Required]
        [StringLength(30)] 
        public string HashCode { get; set; }

        public virtual ICollection<Blog> Blogs { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual Role Role { get; set; }

        //DB Access Helper Methods
        public User GetByEmail(string email, BlogContext db)
        {
            return db.Users.Where(u => u.Email == email).FirstOrDefault();
        }

        public User CheckPassword(string password, BlogContext db)
        {
            return db.Users.Where(u => u.Password.Equals(password)).FirstOrDefault();
        }

        public User GetFromDB(BlogContext db)
        {
            return db.Users.Find(id);
        }
    }
}