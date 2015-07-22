using loowootech.SCM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SupplyChainManagement.Controllers
{
    public class EnterpriseController : ControllerBase
    {
        //
        // GET: /Enterprise/

        public ActionResult Index(Business business)
        {
            var list = Core.EnterpriseManager.Get(business);
            ViewBag.Business = business;
            return View(list);
        }

        [HttpPost]
        public ActionResult Add(Enterprise enterprise)
        {
            if (ModelState.IsValid)
            {
                var index = Core.EnterpriseManager.Add(enterprise);
            }
            return RedirectToAction("Index", new { business = enterprise.Business });
        }

        [ChildActionOnly]
        public ActionResult Contacts(int ID)
        {
            ViewBag.DICT = Core.ContactManager.GetAddressList(ID);
            ViewBag.ID = ID;
            return PartialView("Contacts");
        }

        [ChildActionOnly]
        public ActionResult AddAddressList(int ID)
        {
            ViewBag.List = Core.ContactManager.GetNames(ID);
            return PartialView("AddAddressList");
        }

    }
}
