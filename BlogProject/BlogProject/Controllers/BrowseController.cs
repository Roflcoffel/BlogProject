using BlogProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogProject.Controllers
{
    [HandleError]
    [CustomErrorHandler]
    public class BrowseController : Controller
    {
        BlogContext db = new BlogContext();
        //Tag Search View
        public ActionResult Index()
        {
            ViewBag.ErrorMessage = TempData["ErrorMessage"];

           var query = db.Tags.OrderByDescending(z=>z.PostTags.Count).ToList();
            ViewBag.ViewCount = query.Select(t => t.PostTags.Sum(pt => pt.Post.Views));

            return View(query);
        }

        public ActionResult BlogByViews()
        {
            var query = db.Blogs.OrderByDescending(b => b.Posts.Sum(p => p.Views)).ToList();

            return View(query);
        }

        public ActionResult PostByTags(int Id)
        {
            try
            {
                var query = db.PostTags.Where(x => x.Tag.Id == Id).Select(x => x.Post).ToList();
                ViewBag.Tag = db.Tags.Find(Id);

                return View(query);

            } catch(Exception e)
            {
                TempData["ErrorMessage"] = "Id was not found";
                return RedirectToAction("Index", "Browse");
            }
        }

        public ActionResult Search(string searchString)
        {
            Search searchResult = new Search();

            searchResult.FindString = searchString;

            searchResult.BlogsFound = db.Blogs.Where(b => b.Title.Contains(searchString) ||
            b.Body.Contains(searchString) || b.Posts.SelectMany(p => p.PostTags.Select(pt => pt.Tag.Name)).Contains(searchString)).Distinct().ToList();

            searchResult.PostsFound = db.Posts.Where(p => p.Title.Contains(searchString) ||
            p.Body.Contains(searchString) || p.PostTags.Select(pt => pt.Tag.Name).Contains(searchString)).Distinct().ToList();

            return View(searchResult);
        }

        public ActionResult UnAuthorizedAccess()
        {
            ViewBag.Error = TempData["Error"];
            return View("UnAuthorizedAccess");
        }
    }
}