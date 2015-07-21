using loowootech.SCM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SupplyChainManagement.Controllers
{
    public class DealerController : ControllerBase
    {
        //
        // GET: /Dealer/

        public ActionResult Index()
        {
            ViewBag.list = Core.EnterpriseManager.Get(Business.Seller);
            return View();
        }

    }
}
