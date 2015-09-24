using LoowooTech.SCM.Manager;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoowooTech.SCM.Web.Controllers
{
    public class ControllerBase : AsyncController   
    {
        protected ManagerCore Core = new ManagerCore();
        private const string JsonContentType = "text/html";

        protected ActionResult GetActionResult(object data)
        {
            return Content(JsonConvert.SerializeObject(data), JsonContentType);
        }

        protected ActionResult JsonContent(object data)
        {
            return GetActionResult(data);
        }
        protected ActionResult JsonSuccess(object data = null, string message = null)
        {
            return GetActionResult(new { reslut = 1, data, message });
        }


        protected ActionResult JsonFail(string message = null)
        {
            HttpContext.Response.StatusCode = 500;
            return GetActionResult(new { result = 0, message });
        }



        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewBag.Controller = RouteData.Values["Controller"];
            ViewBag.Action = RouteData.Values["action"];
            base.OnActionExecuting(filterContext);
        }

        private Exception GetException(Exception ex)
        {
            var innerEx = ex.InnerException;
            if (innerEx != null)
            {
                return GetException(innerEx);
            }
            return ex;
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled) return;
            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.StatusCode = 500;
            ViewBag.Exception = GetException(filterContext.Exception);
            filterContext.Result = View("Error");
        }
    }
}
