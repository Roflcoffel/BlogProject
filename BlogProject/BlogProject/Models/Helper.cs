using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;

namespace BlogProject.Models {
    public static class Helper {
        public static string ShortenInputText(string input)
        {
            return input.Substring(0, (input.Length > 40 ? 40 : input.Length)) + "...";
        }

        public static string ShortenInputText(string input, int length)
        {
            return input.Substring(0, (input.Length > length ? length : input.Length)) + "...";
        }
        
        public static bool isUserLoggedIn()
        {
            HttpContext context = HttpContext.Current;
            return (User)context.Session["User"] != null ? true : false; 
        }

        public static bool isUserLoggedIn(User user)
        {
            HttpContext context = HttpContext.Current;
            return (User)context.Session["User"] != null ? (((User)context.Session["User"]).id == user.id ? true : false) : false;
        }

        public static User GetUserFromDatabase(BlogContext db)
        {
            HttpContext context = HttpContext.Current;
            return db.Users.Find(((User)context.Session["User"]).id);            
        }

        public static bool isUploadedFileValid()
        {
            HttpContext context = HttpContext.Current;

            if (context.Request.Files.Count > 0)
            {
                HttpPostedFile file = context.Request.Files[0];

                if (file != null && file.ContentLength > 0)
                {
                    if (file.ContentType == "image/jpeg" || file.ContentType == "image/png")
                    {
                        return true;
                    }
                    else
                    {
                        throw new FileLoadException();
                        //File Format Not Supported
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
                //File Not Found;
            }
        }
    }
}