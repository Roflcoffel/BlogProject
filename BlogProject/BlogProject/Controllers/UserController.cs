using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BlogProject.Models;
using System.IO;
using System.Data.Entity.Validation;

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
        public ActionResult Create(User user, HttpPostedFileBase file)
        {
            //if (ModelState.IsValid)
            //{
            //    db.Users.Add(user);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            if (user == null)
            {
                TempData["ErrorMessage"] = "Add User is not valid";
                return RedirectToAction("Index");
            }

            string mappningstringtophoto = "~/Content/images/";

            if (file == null)
            {
                mappningstringtophoto = mappningstringtophoto + "user_default.jpg";
            }
            else
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/images/"), fileName);
                mappningstringtophoto = mappningstringtophoto + fileName;
                file.SaveAs(path);
            }
            try
            {

                var dbUser = user.GetByEmail(user.Email, db);

                if (dbUser == null)
                {

                    //Create a hashkey
                    var HashKey = Encrypt.GeneratePassword(10);

                    //Create a encrypted password with the hashkey
                    var password = Encrypt.EncodePassword(user.Password, HashKey);

                    user.Password = password;
                    user.HashCode = HashKey;

                    user.ImageUrl = mappningstringtophoto;
                    user.RoleId = 2;

                    db.Users.Add(user);
                    db.SaveChanges();

                    TempData["Message"] = "User Created";

                    return RedirectToAction("Index", "Profile");
                }

                ViewBag.ErrorMessage = "Email already in use!";

                return RedirectToAction("Index");

            }
            catch (DbEntityValidationException e)
            {
                ViewBag.ErrorMessage = "Database and models do not match";
                return RedirectToAction("Index");
            }

            
            //return View(user);
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
                User dbUser = db.Users.Find(id);
                if (user.Role.Id == 1)
                {
                    return View(dbUser);

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
        public ActionResult Edit(User user)
        {
            string filename = "default_image.jpg";
            string path = "~/Content/images/";
            string fullPath = path + filename;
            string ImageFail = "Image did not update";

            try
            {
                var dbUser = db.Users.Find(user.id);

                //Change Default File to Current;
                if (dbUser.ImageUrl != null)
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
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = "Error uploading file";
                return RedirectToAction("Index");
            }
            //if (ModelState.IsValid)
            //{
            //    db.Entry(user).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            //return View(user);
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
                User dbUser = db.Users.Find(id);
                if (user.Role.Id == 1)
                {
                    return View(dbUser);

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
