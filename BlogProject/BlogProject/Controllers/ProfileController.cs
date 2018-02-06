using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogProject.Models;
using System.IO;

namespace BlogProject.Controllers
{
    [HandleError]
    [CustomErrorHandler]
    public class ProfileController : Controller
    {
        BlogContext db = new BlogContext();
        
        //Profile Page
        public ActionResult Index()
        {
            ViewBag.ErrorMessage = TempData["ErrorMessage"];
            ViewBag.Message = TempData["Message"];

            User user = (User)Session["User"];
            
            if(user != null)
            {
                User dbUser = db.Users.Find(user.id);
                return View(dbUser);
            }
            else
            {
                TempData["Message"] = "Log in to view profile page"; 
                return RedirectToAction("Index", "Account");
            }
            
        }

        //Edit Profile; Ajax
        [HttpPost]
        public ActionResult Index(User user)
        {
            string filename = "default_image.jpg";
            string path = "~/Content/images/";
            string fullPath = path + filename;
            string ImageFail = "Image did not update";

            try
            {
                var dbUser = db.Users.Find(user.id);

                //Change Default File to Current;
                if(dbUser.ImageUrl != null)
                {
                    filename = Path.GetFileName(dbUser.ImageUrl);
                }

                if (Request.Files.Count > 0)
                {
                    HttpPostedFileBase file = Request.Files[0];

                    if (file != null && file.ContentLength > 0)
                    {
                        if (file.ContentType == "image/jpeg" || file.ContentType == "image/png")
                        {
                            filename = Path.GetFileName(file.FileName);
                            fullPath = Path.Combine(Server.MapPath(path), filename);

                            file.SaveAs(fullPath);
                            ImageFail = "";
                        }
                        else
                        {
                            //File Format Not Supported
                        }
                    }
                }
                else
                {
                    //File Not Found;
                }

                dbUser.ImageUrl = path + filename;
                dbUser.Firstname = user.Firstname;
                dbUser.Lastname = user.Lastname;
                dbUser.Email = user.Email;

                db.SaveChanges();

                TempData["Message"] = "User Updated, " + ImageFail;
                return RedirectToAction("Index", "Profile");
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = "Error uploading file";
                return RedirectToAction("Index", "Profile");
            }

        }

        public ActionResult MyBlogs()
        {
            User user = (User)Session["User"];

            if (user != null)
            {
                var userBlogs = db.Blogs.Where(x => x.User.id == user.id).ToList();
                return View(userBlogs);
            }

            return RedirectToAction("Index","Account");
        }

        public ActionResult MyPosts(int id)
        {
            User user = (User)Session["User"];
            if (user != null)
            {
                var userPosts = db.Posts.Where(x => (x.Blog.Id == id  && x.Blog.User.id == user.id)).ToList();
                ViewBag.ParentBlog = db.Blogs.Find(id).Title;

                return View(userPosts);
                    
            }
            return RedirectToAction("Index", "Account");
        }

        public ActionResult LogOff()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}