using BlogProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace BlogProject.Controllers
{
    [HandleError]
    [CustomErrorHandler]
    public class AccountController : Controller {
        BlogContext db = new BlogContext();

        //Login and Register page
        public ActionResult Index()
        {
            ViewBag.Message = TempData["Message"];
            ViewBag.ErrorMessage = TempData["ErrorMessage"];

            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginRegisterVM vm)
        {
            User user = vm.LoginToUser();

            try
            {
                if (user != null)
                {
                    //Email must be unique for this to work.
                    var dbUser = user.GetByEmail(user.Email, db);

                    //Get the Encrypted Password
                    string encodedPassword = Encrypt.EncodePassword(user.Password, dbUser.HashCode);

                    //Check if the encrypted input matches what is stored in
                    var userExist = user.CheckPassword(encodedPassword, db);

                    if (userExist != null)
                    {
                        //Start Session
                        Session["User"] = dbUser;
                        TempData["Message"] = "Login Successful";

                        return RedirectToAction("Index", "Profile");
                    }

                    TempData["ErrorMessage"] = "Invallid User Name or Password";
                    return RedirectToAction("Index");
                }

            
                TempData["ErrorMessage"] = "Invallid User Name or Password";
                return RedirectToAction("Index");

            }
            catch
            {
                TempData["ErrorMessage"] = "Email does not exist";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Registration(LoginRegisterVM vm, HttpPostedFileBase file)
        {
            
            User user = vm.RegisterToUser();

            if(user == null)
            {
                TempData["ErrorMessage"] = "Registration form not valid";
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

                    TempData["Message"] = "Account Created";

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
            
            
        }
    }
}