using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogProject.Models {
    public class LoginRegisterVM {

        [Required(ErrorMessage = "Please fill in a password")]
        [StringLength(50)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string LoginPassword { get; set; }

        [Required(ErrorMessage = "Please fill in an email")]
        [StringLength(50)]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string LoginEmail { get; set; }

        [Required(ErrorMessage = "Please fill in a password")]
        [StringLength(50)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string RegisterPassword { get; set; }

        [Required(ErrorMessage = "Please fill in an email")]
        [StringLength(50)]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string RegisterEmail { get; set; }

        [Required(ErrorMessage = "Please enter your firstname")]
        [StringLength(50)]
        [Display(Name = "Firstname")]
        public string RegisterFirstname { get; set; }

        [Required(ErrorMessage = "Please enter your lastname")]
        [StringLength(50)]
        [Display(Name = "Lastname")]
        public string RegisterLastname { get; set; }

        [StringLength(200)]
        [Display(Name = "Url")]
        public string RegisterImgUrl { get; set; }

        public User LoginToUser()
        {
            return new User { Password = LoginPassword, Email = LoginEmail };
        }

        public User RegisterToUser()
        {
            return new User { Password = RegisterPassword, Email = RegisterEmail, Firstname = RegisterFirstname, Lastname = RegisterLastname, ImageUrl = RegisterImgUrl };
        }
    }
}