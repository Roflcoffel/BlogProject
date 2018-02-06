using BlogProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BlogProject.Controllers
{
    [HandleError]
    [CustomErrorHandler]
    public class PostController : Controller
    {
        BlogContext db = new BlogContext();

        // GET: Post
        public ActionResult Index()
        {
            User user = (User)Session["User"];
            if (user != null)
            {
                User dbUser = db.Users.Find(user.id);
                if (user.Role.Id == 1)
                {
                    return View(db.Posts.ToList());
                }
            }

            TempData["Error"] = "You have to be admin to see this page";
            return RedirectToAction("UnAuthorizedAccess", "Browse");

        }

        public ActionResult Create(int id)
        {
            User user = (User)Session["User"];

            if (user != null)
            {
                User dbUser = db.Users.Find(user.id);
                Blog blog = db.Blogs.Find(id);

                if(blog != null)
                {
                    if (blog.User.id == dbUser.id)
                    {
                        PostTagVM postTag = new PostTagVM();

                        postTag.Post.Blog = db.Blogs.Find(id);
                        postTag.Tags = db.Tags.ToList();

                        return View(postTag);
                    }
                    else
                    {
                        TempData["Error"] = "you are allowed to create a post in your blog only";
                        return RedirectToAction("UnAuthorizedAccess", "Browse");
                    }
                }
                else
                {
                    TempData["Error"] = "the blog does not exist";
                    return RedirectToAction("UnAuthorizedAccess", "Browse");
                }
            }
            
            TempData["Error"] = "you have to be logged in to create a post in your blog";
            return RedirectToAction("UnAuthorizedAccess", "Browse");
        }

        [HttpPost]
        [ValidateInput(true)]
        public ActionResult Create(PostTagVM postTag, HttpPostedFileBase file, int id)
        {
            string mappningstringtophoto = "~/Content/images/";

            if (file == null)
            {
                mappningstringtophoto = mappningstringtophoto + "post_default.jpg";
            }
            else
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/images/"), fileName);
                mappningstringtophoto = mappningstringtophoto + fileName;
                file.SaveAs(path);
            }

            postTag.Post.ImageUrl = mappningstringtophoto;
            postTag.Post.Created = DateTime.Today;
            postTag.Post.LastChanged = DateTime.Today;

            postTag.Post.Blog = db.Blogs.Find(postTag.Post.Blog.Id);
            postTag.Post.Comments = new List<Comment>();

            List<Tag> checkedTags = postTag.Tags.Where(x => x.Check).ToList();

            foreach (var item in checkedTags)
            {
                var tempTag = db.Tags.Find(item.Id);
                db.PostTags.Add(new PostTag { Tag = tempTag, Post = postTag.Post });
            }

            db.Posts.Add(postTag.Post);
            db.SaveChanges();

            return View("ViewBlog", postTag.Post.Blog);
        }

        [HttpGet]
        public ActionResult Edit(int postId)
        {
            
            var post = db.Posts.Find(postId);
            if (post == null)
            {
                return HttpNotFound();
            }

            User user = (User)Session["User"];

            if (user != null)
            {
                User dbUser = db.Users.Find(user.id);
                if (user.Role.Id == 1 || post.Blog.User.id == dbUser.id)
                {
                    PostTagVM obj = new PostTagVM();
                    obj.Post = post;
                    obj.Tags = db.Tags.ToList();

                    return View(obj);

                }
            }
            
            TempData["Error"] = "you have to be logged in to edit your post";
            return RedirectToAction("UnAuthorizedAccess", "Browse");
            
        }

        [HttpPost]
        public ActionResult Edit(PostTagVM postTag, HttpPostedFileBase file)
        {
            string mappningstringtophoto = "~/Content/images/";
            Post dbPost = db.Posts.Find(postTag.Post.Id);

            if (file != null)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/images/"), fileName);
                mappningstringtophoto = mappningstringtophoto + fileName;
                file.SaveAs(path);
                postTag.Post.ImageUrl = mappningstringtophoto;
                dbPost.ImageUrl = postTag.Post.ImageUrl;
            }
             
            dbPost.LastChanged = DateTime.Today;
            dbPost.Title = postTag.Post.Title;
            dbPost.Body = postTag.Post.Body;

            List<Tag> checkedTags = postTag.Tags.Where(x => x.Check).ToList();

            foreach (var item in checkedTags)
            {
                var tempTag = db.Tags.Find(item.Id);
                db.PostTags.Add(new PostTag { Tag = tempTag, Post = dbPost });
            }

            var id = dbPost.Blog.Id;

            db.SaveChanges();

            return RedirectToAction("MyPosts","Profile", new {id = id });
        }

        [HttpGet]
        public ActionResult Delete(int postId)
        {
            
            var post = db.Posts.Find(postId);
            if (post == null)
            {
                return HttpNotFound();
            }

            User user = (User)Session["User"];

            if (user != null)
            {
                User dbUser = db.Users.Find(user.id);
                if (user.Role.Id == 1 || post.Blog.User.id == dbUser.id)
                {
                    return View(post);

                }
            }
            
            TempData["Error"] = "you have to be logged in to delete your post";
            return RedirectToAction("UnAuthorizedAccess", "Browse");

            //var post = db.Posts.Find(postId);
            //return View(post);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int postId)
        {

            Post post = db.Posts.Find(postId);
            List<PostTag> postTag = post.PostTags.ToList();
            List<Comment> comments = post.Comments.ToList(); 

            foreach (var item in postTag)
            {
                db.PostTags.Remove(db.PostTags.Find(item.Id));
            }

            foreach (var item in comments)
            {
                db.Comments.Remove(item);
            }

            var id = post.Blog.Id;
            
            db.Posts.Remove(post);
            db.SaveChanges();
            return RedirectToAction("MyPosts", "Profile", new { id = id });

        }


        // POST: Post/Comment
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Comment(int id, string commentbody)
        {
            Comment comment = new Comment();
            User user = (User)Session["User"];
            
            if (user != null)
            {
                User dbUser = db.Users.Find(user.id);
                comment.user = dbUser;
            }

            
            comment.Body = commentbody;
            comment.Created = DateTime.Now;
            
            
            Post post = db.Posts.Find(id);

            comment.Post = post;
            if (ModelState.IsValid)
            {
                
                post.Comments.Add(comment);
                db.Entry(post).State = EntityState.Modified;

                db.SaveChanges();
                ModelState.Clear();
                return View("Article", post);

            }
            
            return View("Article", post);

        }

        public ActionResult DeleteComment(int id)
        {
            User user = (User)Session["User"];

            if(user == null)
            {
                TempData["Message"] = "You are not logged in";
                return RedirectToAction("Index", "Account");
            }

            //Check Permission to delete;
            
            var comment = db.Comments.Find(id);
            if (comment != null)
            {
                if (user.id == comment.user.id)
                {
                    var post = comment.Post;
                    db.Comments.Remove(comment);
                    db.SaveChanges();
                    return RedirectToAction("Article", new { id = post.Id });
                }
                TempData["Error"] = "You do not have permission to delete this comment";
                return RedirectToAction("UnAuthorizedAccess", "Browse");
            }

            return new HttpNotFoundResult();
        }

        public ActionResult Article(int id)
        {
            Post post = db.Posts.Find(id);

            if(post != null)
            {
                post.Views += 1;
                db.SaveChanges();
                return View(post);
            }

            TempData["Error"] = "Article not found";
            return RedirectToAction("UnAutorizedAccess", "Browse");
        }
    }
}