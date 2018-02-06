using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogProject.Models {
    public class CustomErrorHandler : HandleErrorAttribute {
        public override void OnException(ExceptionContext exceptionContext)
        {
            if (!exceptionContext.ExceptionHandled)
            {
                string controllerName = (string)exceptionContext.RouteData.Values["controller"];
                string actionName = (string)exceptionContext.RouteData.Values["action"];
                var model = new HandleErrorInfo(exceptionContext.Exception, controllerName, actionName);

                exceptionContext.Result = new ViewResult
                {
                    ViewName = "Error",
                    ViewData = new ViewDataDictionary<HandleErrorInfo>(model),
                    TempData = exceptionContext.Controller.TempData
                };
            }
        }
    }
}