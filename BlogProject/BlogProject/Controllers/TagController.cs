using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BlogProject.Models;

namespace BlogProject.Controllers
{
    public class TagController : Controller
    {
        private BlogContext db = new BlogContext();

        // GET: Tag
        public ActionResult Index()
        {
            User user = (User)Session["User"];
            
            if (user != null)
            {
                User dbUser = db.Users.Find(user.id);
                if (user.Role.Id == 1)
                {
                    return View(db.Tags.ToList());
                }
            }
            //return HttpNotFound();
            TempData["Error"] = "You have to be admin to see this page";
            return RedirectToAction("UnAuthorizedAccess", "Browse");

        }

        //// GET: Tag/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Tag tag = db.Tags.Find(id);
        //    if (tag == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tag);
        //}

        // GET: Tag/Create
        public ActionResult Create()
        {
            User user = (User)Session["User"];

            if (user != null)
            {

                return View();

            }
            //return HttpNotFound();
            TempData["Error"] = "you have to be logged in to create a tag";
            return RedirectToAction("UnAuthorizedAccess", "Browse");

        }

        // POST: Tag/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Tag tag)
        {
            if (ModelState.IsValid)
            {
                db.Tags.Add(tag);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tag);
        }

        // GET: Tag/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tag tag = db.Tags.Find(id);
            if (tag == null)
            {
                return HttpNotFound();
            }

            User user = (User)Session["User"];

            if (user != null)
            {
                User dbUser = db.Users.Find(user.id);
                if (user.Role.Id == 1 )
                {
                    return View(tag);

                }
            }
            //return HttpNotFound();
            //return View(tag);
            TempData["Error"] = "You have to be admin to see this page";
            return RedirectToAction("UnAuthorizedAccess", "Browse");
        }

        // POST: Tag/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Tag tag)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tag).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tag);
        }

        // GET: Tag/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tag tag = db.Tags.Find(id);
            if (tag == null)
            {
                return HttpNotFound();
            }
            User user = (User)Session["User"];

            if (user != null)
            {
                User dbUser = db.Users.Find(user.id);
                if (user.Role.Id == 1)
                {
                    return View(tag);

                }
            }
            //return HttpNotFound();
            //return View(tag);
            TempData["Error"] = "You have to be admin to see this page";
            return RedirectToAction("UnAuthorizedAccess", "Browse");
        }

        // POST: Tag/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tag tag = db.Tags.Find(id);
            db.Tags.Remove(tag);
            List<PostTag> postTagList = db.PostTags.Where(t => t.Tag.Id == tag.Id).ToList();
            db.PostTags.RemoveRange(postTagList);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpPost]
        public JsonResult CreateTag(FormCollection fc, string[] txtValue)
        {
            

            List<string> value = new List<string>();
            for (int i = 0; i < txtValue.Length; i++)
            {
                var tagName = txtValue[i].ToLower();

                if (db.Tags.SingleOrDefault(t => t.Name.ToLower() == tagName) == null)
                {
                    value.Add(tagName);
                    db.Tags.Add(new Tag { Name = tagName });
                }
            }

            db.SaveChanges();

            List<int> tagIds = new List<int>();
            foreach (var item in value)
            {
                var tag = db.Tags.SingleOrDefault(t => t.Name == item);

                tagIds.Add(tag.Id);

            }

            var result = new { Success = "True", Message = "Nya Tags", nyaTags = value, nyaIds = tagIds };
            return Json(result, JsonRequestBehavior.AllowGet);

        }
    }
}
