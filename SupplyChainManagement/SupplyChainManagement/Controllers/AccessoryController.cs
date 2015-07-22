using loowootech.SCM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SupplyChainManagement.Controllers
{
    public class AccessoryController : ControllerBase
    {
        //
        // GET: /Accessory/

        public ActionResult Index()
        {
            ViewBag.List = Core.ComponentsManager.Get();
            return View();
        }

        [HttpPost]
        public ActionResult Add(Components components)
        {
            var Index = Core.ComponentsManager.Add(components);
            return RedirectToAction("Index");
        }

    }
}
