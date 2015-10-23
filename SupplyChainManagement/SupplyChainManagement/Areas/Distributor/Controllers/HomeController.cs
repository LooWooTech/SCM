using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoowooTech.SCM.Web.Areas.Distributor.Controllers
{
    public class HomeController : DistributorControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }

    }
}
