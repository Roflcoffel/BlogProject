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
    public class UserController : Controller
    {
        private BlogContext db = new BlogContext();

        // GET: User
        public ActionResult Index()
        {
            User user = (User)Session["User"];
            if (user != null)
            {
                User dbUser = db.Users.Find(user.id);
                if (user.Role.Id == 1)
                {
                    return View(db.Users.ToList());
                }
            }

            //return HttpNotFound();
            TempData["Error"] = "You have to be admin to see this page";
            return RedirectToAction("UnAuthorizedAccess", "Browse");

        }

        //// GET: User/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    User user = db.Users.Find(id);
        //    if (user == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(user);
        //}

        // GET: User/Create
        public ActionResult Create()
        {
            User user = (User)Session["User"];

            if (user != null)
            {

                return View();

            }
            //return HttpNotFound();
            TempData["Error"] = "You have to be admin to see this page";
            return RedirectToAction("UnAuthorizedAccess", "Browse");
        }

        // POST: User/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Firstname,Lastname,Email,Password,ImageUrl,HashCode")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: User/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ////User user = db.Users.Find(id);
            //if (user == null)
            //{
            //    return HttpNotFound();
            //}
            User user = (User)Session["User"];

            if (user != null)
            {
                User dbUser = db.Users.Find(user.id);
                if (user.Role.Id == 1)
                {
                    return View(user);

                }
            }
            //return HttpNotFound();
            TempData["Error"] = "You have to be admin to see this page";
            return RedirectToAction("UnAuthorizedAccess", "Browse");
            //return View(user);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Firstname,Lastname,Email,Password,ImageUrl,HashCode")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: User/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //User user = db.Users.Find(id);
            //if (user == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(user);
            User user = (User)Session["User"];

            if (user != null)
            {
                User dbUser = db.Users.Find(user.id);
                if (user.Role.Id == 1)
                {
                    return View(user);

                }
            }
            //return HttpNotFound();
            TempData["Error"] = "You have to be admin to see this page";
            return RedirectToAction("UnAuthorizedAccess", "Browse");
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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
    }
}
