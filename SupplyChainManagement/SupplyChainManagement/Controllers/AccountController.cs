using LoowooTech.SCM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoowooTech.SCM.Web.Controllers
{
    [UserAuthorize(Enabled = false)]
    [UserRole(Role = UserRole.Everyone)]
    public class AccountController : ControllerBase
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            var user = Core.UserManager.GetModel(username, password);
            HttpContext.SaveAuth(user);
            return JsonSuccess(new { role = user.Role });
        }

        public ActionResult Logout()
        {
            HttpContext.ClearAuth();
            return RedirectToAction("Login");
        }
    }
}
