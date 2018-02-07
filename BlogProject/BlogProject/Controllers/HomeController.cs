using BlogProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogProject.Models;

namespace BlogProject.Controllers
{
	
	
	[HandleError]
	[CustomErrorHandler]
	public class HomeController : Controller
	{
		private BlogContext db = new BlogContext();
	
		// GET: Home
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult Error()
		{
			return View();
		}
		public ActionResult ContactUs()
		{
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		
		public ActionResult ContactUs(ContactUs contactUs)
		{
			//if (ModelState.IsValid)
			//{
			User user = db.Users.Where(x => x.Email == contactUs.Email).FirstOrDefault();

			if (user != null)
			{ 
				ViewBag.MessageSend = "your Question sent!";
				return View();
			}
			else
			{
				ViewBag.MessageSendTop1 = "This email doesn't exist!";
				return View();
			}
			//}
			
			
		}
}
}