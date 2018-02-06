using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BlogProject.Models;
using System.Data.Entity.Validation;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace BlogProject.Controllers
{
	[HandleError]
	[CustomErrorHandler]
	public class BlogController : Controller
	{
		BlogContext db = new BlogContext();

		//GET: Blog
		public ActionResult Index()
		{
			User user = (User)Session["User"];
			
			if (user != null)
			{
				User dbUser = db.Users.Find(user.id);
				if (user.Role.Id == 1)
				{
					return View(db.Blogs.ToList());
				}
			}
			
			//return HttpNotFound();
			TempData["Error"] = "You have to be admin to see this page";
			return RedirectToAction("UnAuthorizedAccess", "Browse");

		}

		// GET: Blog/Details/5
		//public ActionResult Details(int? id)
		//{
		//    if (id == null)
		//    {
		//        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
		//    }
		//    Blog blog = db.Blogs.Find(id);
		//    if (blog == null)
		//    {
		//        return HttpNotFound();
		//    }
		//    return View(blog);
		//}

		// GET: Blog/Create
		public ActionResult Create()
		{
			User user = (User)Session["User"];

			if (user != null)
			{
				
				return View();
				
			}
			//return HttpNotFound();
			TempData["Error"] = "you have to be logged in to create a blog";

			return RedirectToAction("UnAuthorizedAccess", "Browse");

		}

		// POST: Blog/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(Blog blog, HttpPostedFileBase file)
		{
			User user = (User)Session["User"];

			string filename = "blog.png";         
			string path = "~/Content/images/";
			string fullPath = path + filename;
			string ImageFail = "Image did not upload";

			try
			{ 
				if(user != null)
				{
					User dbUser = db.Users.Find(user.id);
					

					if (Request.Files.Count > 0)
					{
						

						if (file != null && file.ContentLength > 0)
						{
							if (file.ContentType == "image/jpeg" || file.ContentType == "image/png")
							{
								filename = dbUser.id + Path.GetFileName(file.FileName);
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

					

					if (ModelState.IsValid)
					{
						blog.ImageUrl = path + filename;
						blog.User = dbUser;
						blog.Created = DateTime.Today;
						db.Blogs.Add(blog);
						db.SaveChanges();
						TempData["Message"] = "Blog Created, " + ImageFail;

						return RedirectToAction("MyBlogs","Profile");
					}
				}
			}
			catch (InvalidOperationException e)
			{
				TempData["ErrorMessage"] = "Error uploading file";
				ViewBag.ErrorMessage = "Validation Error: " +e.StackTrace + "\n\n" + e.InnerException;
				return View();
			}
			
			return View(blog);
		}

		// GET: Blog/Edit/5
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Blog blog = db.Blogs.Find(id);
			if (blog == null)
			{
				return HttpNotFound();
			}

			User user = (User)Session["User"];

			if (user != null)
			{
				User dbUser = db.Users.Find(user.id);
				if (user.Role.Id == 1 || blog.User.id == dbUser.id)
				{
					return View(blog);
					
				}
			}
			//return HttpNotFound();
			TempData["Error"] = "you have to be logged in to edit your blog";
			return RedirectToAction("UnAuthorizedAccess", "Browse");
			//return View(blog);

		}

		// POST: Blog/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit( Blog blog, HttpPostedFileBase file)
		{
			string filename = "blog.png";
			
			string path = "~/Content/images/";
			string fullPath = path + filename;
			string ImageFail = "Image did not upload";
			User user = (User)Session["User"];
			try
			{
				if (user != null )
				{
					User dbUser = db.Users.Find(user.id);
					

					if (Request.Files.Count > 0)
					{
						

						if (file != null && file.ContentLength > 0)
						{
							if (file.ContentType == "image/jpeg" || file.ContentType == "image/png")
							{
								filename = dbUser.id + Path.GetFileName(file.FileName);
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


					//&& blog.User.id == dbUser.id
					if (ModelState.IsValid )
					{
						blog.ImageUrl = path + filename;
						
						db.Entry(blog).State = EntityState.Modified;
						
						db.SaveChanges();
						TempData["Message"] = "Blog Created, " + ImageFail;
						
						return RedirectToAction("MyBlogs", "Profile");
					}
				}
			}
			catch (InvalidOperationException e)
			{
				TempData["ErrorMessage"] = "Error uploading file";
				ViewBag.ErrorMessage = "Validation Error: " + e.StackTrace + "\n\n" + e.InnerException;
				return View();
				
			}
			
			return View(blog);
			
		}

		//GET: Blog/Delete/5
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Blog blog = db.Blogs.Find(id);
			if (blog == null)
			{
				return HttpNotFound();
			}

			User user = (User)Session["User"];

			if (user != null)
			{
				User dbUser = db.Users.Find(user.id);
				if (user.Role.Id == 1 || blog.User.id == dbUser.id)
				{
					return View(blog);

				}
			}
			//return HttpNotFound();
			//return View(blog);
			TempData["Error"] = "you have to be logged in to delete your blog";
			return RedirectToAction("UnAuthorizedAccess", "Browse");

		}

		// POST: Blog/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			Blog blog = db.Blogs.Find(id);
			List<Post> posts = blog.Posts.ToList();
			List<Comment> comments = blog.Posts.SelectMany(x => x.Comments).ToList();

			foreach (var post in posts)
			{
				var ptag = post.PostTags.ToList();
				foreach (var item in ptag )
				{
					db.PostTags.Remove(item);
				}

				foreach(var item in comments)
				{
					db.Comments.Remove(item);
				}

				db.Posts.Remove(post);
			}
			
			db.Blogs.Remove(blog);
			db.SaveChanges();
			
			return RedirectToAction("MyBlogs", "Profile");
		}

		public ActionResult ViewBlog(int Id)
		{
			var blog = db.Blogs.Find(Id);

			//Reverse the order of posts, newest dispays at the top.
			blog.Posts = blog.Posts.OrderByDescending(p => p.Id).ToList();

			return View("ViewBlog",blog);
		}
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}
	}
}
