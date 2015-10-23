using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoowooTech.SCM.Web
{
    [AttributeUsage(AttributeTargets.All, Inherited = true)]
    public class UserAuthorizeAttribute : System.Web.Mvc.AuthorizeAttribute
    {
        public UserAuthorizeAttribute()
        {
            Enabled = true;
        }

        public bool Enabled { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return httpContext.User.Identity.IsAuthenticated;
        }


        protected override void HandleUnauthorizedRequest(System.Web.Mvc.AuthorizationContext filterContext)
        {
            if (Enabled)
            {
                var returnUrl = filterContext.HttpContext.Request.Url.AbsoluteUri;
                var loginUrl = System.Web.Security.FormsAuthentication.LoginUrl;
                filterContext.HttpContext.Response.Redirect(loginUrl + "?returnUrl=" + HttpUtility.UrlEncode(returnUrl));
            }
        }
    }
}