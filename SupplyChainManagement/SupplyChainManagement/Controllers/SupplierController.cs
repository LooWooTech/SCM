using loowootech.SCM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SupplyChainManagement.Controllers
{
    public class SupplierController : ControllerBase
    {
        //
        // GET: /Supplier/

        public ActionResult Index()
        {
            var list = Core.EnterpriseManager.Get(Business.Supplier);
            ViewBag.list = list;
            ViewBag.Dictionary = Core.ContactManager.Get();
            ViewBag.Business = Business.Supplier;
            return View(list);
        }

        [HttpPost]
        public ActionResult Add(Enterprise enterprise)
        {
            if (ModelState.IsValid)
            {
                var Index = Core.EnterpriseManager.Add(enterprise);
            }
            
            return RedirectToAction("Index");
        }

    }
}
